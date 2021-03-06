# Pull Requests Viewer

This project aims developers, who struggle to manage the PR's from several repos. When we work as a team, it's normal to stack some PR's, and sometimes we lost track of it. The Pull Requests Viewer allows a holistic view over the *open* PR's for the selected repos.

[![Build status](https://ci.appveyor.com/api/projects/status/1is7xrovhnau8l0u?svg=true)](https://ci.appveyor.com/project/joaoasrosa/pullrequests-viewer) [![Build Status](https://travis-ci.org/joaoasrosa/pullrequests-viewer.svg?branch=master)](https://travis-ci.org/joaoasrosa/pullrequests-viewer)

## Getting Started

These instructions will get you a copy of the project up and run on your local machine.

### Prerequisites

Before running the project, you will need:

* [.NET Core 2.0 SDK][dotnet-sdk]
* [Docker][docker]

### Installing as container (Docker)

1. Open the terminal/command line
2. Execute the command `docker run -d -p 8000:80 joaoasrosa/pullrequestsviewer`
3. Open the browser and type `http://localhost:8000`


### Installing as application

1. Download the latest version from [here][latest-release]
2. Unzip the downloaded file to a directory
3. Open the terminal/command line and navigate to the directory
4. Execute the command `dotnet PullRequestsViewer.WebApp.dll`
5. Open the browser and type `http://localhost:5000`

Alternatively, you can deploy the application on a web server.

## Built With

* [.NET Core 2.0][dotnet] - The Framework
* [SQLite][sqlite] - Data Persistence
* [NuGet][nuget] - Dependency Management
* [Cake][cake] - Cross Platform Build Automation System
* [AppVeyor][appveyor] - Continuous Integration & Delivery Service
* [TravisCI][travisci] - Continuous Integration Platform for GitHub
* [Docker][docker] - Container Platform

## Contributing

Please read [CONTRIBUTING.md][contributing] for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer][semver] for versioning. For the versions available, see the [tags on this repository][tags].

## Authors

* **João Rosa** - *Initial work* - [joaoasrosa][joaoasrosa]

See also the list of [contributors][contributors] who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE][license] file for details

[dotnet-sdk]: https://www.microsoft.com/net/core/preview
[docker]: https://www.docker.com/
[latest-release]: https://github.com/joaoasrosa/pullrequests-viewer/releases
[dotnet]: https://www.microsoft.com/net/
[sqlite]: https://www.sqlite.org/
[nuget]: https://www.nuget.org/
[cake]: http://cakebuild.net/
[appveyor]: https://www.appveyor.com/
[travisci]: https://travis-ci.org/
[contributing]: https://github.com/joaoasrosa/pullrequests-viewer/blob/master/CONTRIBUTING.md
[semver]: http://semver.org/
[tags]: https://github.com/joaoasrosa/pullrequests-viewer/tags
[joaoasrosa]: https://github.com/joaoasrosa
[contributors]: https://github.com/joaoasrosa/pullrequests-viewer/contributors
[license]: LICENSE
