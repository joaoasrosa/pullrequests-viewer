using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PullRequestsViewer.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.SqlLite.Tests.Unit
{
    public class RepositoryPersistenceTests
    {
        private readonly Mock<IPullRequestsViewerContext> _dbContextMock;
        private readonly RepositoryPersistence _sut;

        private readonly IReadOnlyList<Repository> _repositories;

        public RepositoryPersistenceTests()
        {
            var repositoryDbSet = GetQueryableMockDbSet(new List<Repository>());

            _dbContextMock = new Mock<IPullRequestsViewerContext>();
            _dbContextMock.SetupGet(x => x.Repositories).Returns(repositoryDbSet);

            _sut = new RepositoryPersistence(_dbContextMock.Object);

            _repositories = Common.Tests.Builders.Domain.RepositoryBuilder.GenerateValidRepositories();
        }

        [Fact]
        public async Task GetAllAsync_Always_CallsDbContext()
        {
            await _sut.GetAllAsync();

            _dbContextMock.Verify(x => x.Repositories, Times.Once);
        }

        [Fact]
        public async Task SaveAsync_Always_CallsRepositoriesFindAsync()
        {
            List<string> expected = new List<string>();

            _dbContextMock.Setup(x => x.Repositories.FindAsync(It.IsAny<object[]>()))
                .Callback((object[] keys) => expected.AddRange(keys.Select(x => x.ToString())))
                .ReturnsAsync((Repository)null);

            await _sut.SaveAsync(_repositories);

            expected.ShouldBeEquivalentTo(_repositories.Select(x => x.Name));
        }

        [Fact]
        public async Task SaveAsync_IfFindAsyncReturnsNull_CallsRepositoriesAddAsync()
        {
            _dbContextMock.Setup(x => x.Repositories.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Repository)null);

            await _sut.SaveAsync(_repositories);

            foreach (var repository in _repositories)
            {
                _dbContextMock.Verify(x => x.Repositories.AddAsync(repository, default(CancellationToken)), Times.Once);
            }
        }

        [Fact]
        public async Task SaveAsync_IfFindAsyncReturnsEntity_CallsRepositoriesUpdate()
        {
            _dbContextMock.Setup(x => x.Repositories.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(new Repository());

            await _sut.SaveAsync(_repositories);

            _dbContextMock.Verify(x => x.Repositories.Update(It.IsAny<Repository>()), Times.Exactly(_repositories.Count));
        }

        [Fact]
        public async Task SaveAsync_Always_CallsDbContextSaveChangesAsync()
        {
            await _sut.SaveAsync(_repositories);

            _dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
