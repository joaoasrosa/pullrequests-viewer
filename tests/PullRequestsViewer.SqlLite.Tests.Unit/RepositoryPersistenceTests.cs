using Microsoft.EntityFrameworkCore;
using Moq;
using PullRequestsViewer.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PullRequestsViewer.SqlLite.Tests.Unit
{
    public class RepositoryPersistenceTests
    {
        private readonly Mock<IPullRequestsViewerContext> _dbContextMock;
        private readonly RepositoryPersistence _sut;

        public RepositoryPersistenceTests()
        {
            var repositoryDbSet = GetQueryableMockDbSet(new List<Repository>());

            _dbContextMock = new Mock<IPullRequestsViewerContext>();
            _dbContextMock.SetupGet(x => x.Repositories).Returns(repositoryDbSet);

            _sut = new RepositoryPersistence(_dbContextMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Always_CallsDbContext()
        {
            await _sut.GetAllAsync();

            _dbContextMock.Verify(x => x.Repositories, Times.Once);
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
