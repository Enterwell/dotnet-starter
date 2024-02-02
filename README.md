<h1 align="center">
  <a style="display: inline-block;" href="https://enterwell.net" target="_blank">
    <picture>
      <source media="(prefers-color-scheme: dark)" srcset="http://dev.enterwell.space/EW_logo_white.svg">
      <img width="128" height="128" alt="logo" src="http://dev.enterwell.space/EW_logo_black.svg">
    </picture>
  </a>
  <p>Enterwell .NET starter</p>
</h1>

<div align="center">
  <p>ASP.NET Core Web API using .NET 8 and PostgreSQL with Entity Framework Core following the principles of Clean Architecture.</p>
  <div>

  [![CI](https://github.com/Enterwell/dotnet-starter/actions/workflows/CI.yml/badge.svg)](https://github.com/Enterwell/dotnet-starter/actions/workflows/CI.yml)
  [![CodeQL](https://github.com/Enterwell/dotnet-starter/actions/workflows/codeql-anaysis.yml/badge.svg)](https://github.com/Enterwell/dotnet-starter/actions/workflows/codeql-anaysis.yml)
  [![GitHub issues](https://img.shields.io/github/issues/Enterwell/dotnet-starter?color=0088ff)](https://github.com/Enterwell/dotnet-starter/issues)
  [![GitHub contributors](https://img.shields.io/github/contributors/Enterwell/dotnet-starter)](https://github.com/Enterwell/dotnet-starter/graphs/contributors)
  [![GitHub pull requests](https://img.shields.io/github/issues-pr/Enterwell/dotnet-starter?color=0088ff)](https://github.com/Enterwell/dotnet-starter/pulls)

  </div>
  <div>
    <a href="https://dotnet.microsoft.com" target="_blank">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="40" />
    </a>
    <a href="https://learn.microsoft.com/en-us/dotnet/csharp/" target="_blank">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="40" />
    </a>
    <a href="https://www.postgresql.org/" target="_blank">
      <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-plain.svg" width="40" />
    </a>
    <a href="https://xunit.net/" target="_blank">
      <img src="https://avatars.githubusercontent.com/u/2092016" width="40" />
    </a>
    <a href="https://automapper.org/" target="_blank">
      <img src="https://avatars.githubusercontent.com/u/890883" width="40" />
    </a>
    <a href="https://github.com/moq/moq" target="_blank">
      <img src="https://avatars.githubusercontent.com/u/1434934" width="40" />
    </a>
  </div>
</div>

## 📖 Table of contents
+ 🚀 [Technologies](#-technologies)
+ 🛠️ [Prerequisites](#-prerequisites)
+ 🔰 [Getting started](#-getting-started)
+ 🏛️ [Project structure](#-project-structure)
+ 🔄 [Continous integration](#-continous-integration)
+ 📄 [Changelog manager](#-changelog-manager)
+ ☎️ [Support](#-support)
+ 🪪 [License](#-license)

## 🚀 Technologies
+ [ASP.NET Core 7](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0) - Cross-platform, high-performance, open source framework for building modern, cloud-enabled, Internet-connected applications
+ [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio) - membership system that allows login functionality
+ [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/) - lightweight, extensible, open source and cross-platform object-relational mapper (O/RM)
+ [AutoMapper](https://automapper.org/) - convention-based object-object mapper
+ [PostgreSQL](https://www.postgresql.org/) - powerful, open source object-relational database system
+ [Serilog](https://serilog.net/) - simple .NET logging with fully-structured events
+ [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/) and [Moq](https://github.com/moq)

## 🛠 Prerequisites
+ [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
+ [PostgreSQL](https://www.postgresql.org/) installed on your local machine
+ Code editor ([Visual Studio 2022](https://visualstudio.microsoft.com/vs/) recommended)

## 🔰 Getting started

### Downloading the starter
The easiest way to get started is to scaffold a copy of the repository by using [degit](https://github.com/Rich-Harris/degit).

If you don't already have it, you can easily install it by using the following command (assuming you have [Node.js](https://nodejs.org/en) installed)

```bash
npm install --global degit
```

Now you can download the repository without any hassle and unnecessary git history using the following command

```bash
degit https://github.com/Enterwell/dotnet-starter dotnet-starter
```

### Renaming the solution and projects

If you want to replace the placeholder name "Acme", you can easily do so using the script `StarterRename.ps1`

```bash
.\StarterRename.ps1 -newName TestApp
```

This will update all namespaces, usings, project names and solution file. No further action should be necessary on your part.

### Running the application
Set the `Acme.Interface.WebAPI` as the startup project, build and run the application.

Web API is available at [`https://localhost:7090`](https://localhost:7090) and the interactive Web API Swagger documentation at the [`https://localhost:7090/swagger`](https://localhost:7090/swagger).

#### PostgreSQL

By default, application will try to connect to PostgreSQL server running locally with the following configuration:
+ Port: `5432`
+ Username: `postgres`
+ Password: `password`

If you have your server configured differently, change the connection string in the `Acme.Interface.WebAPI/appsettings.Development.json` file.

#### Database initialization (initial migration)

If the application was started for the first time, database needs to be initialized by executing the migrations.

+ After starting the application go to the Web API Swagger documentation available at [`https://localhost:7090/swagger`](https://localhost:7090/swagger).
+ You can see two `ApplicationManagement` endpoints:
  + `/assert-migrations` - used to remotely check if the database is up-to-date with the migrations
  + `/migrate` - used to remotely run the migrations
+ Execute the `/migrate` endpoint using Swagger docs
+ You can check if the migrations were ran by asserting once again
+ After running the migrations, database is seeded with a single admin user:
  + username: `admin`
  + email: `admin@acme.com`
  + password: `pa$$w0rd`

#### Generating new migrations

Generate a new migration using Visual Studio Package Manager Console (from menu: Tools -> NuGet Package Manager -> Package Manager Console):

+ Verify that the `Acme.Infrastructure.EF.PostgreSql` is selected as a default project

```powershell
PM> Add-Migration <MIGRATION_NAME>
```

## 🏛 Project structure

### Core
Core encapsulate *enterprise wide* business rules or, in simpler terms, core entities that are the business objects of the application. They encapsulate the most general and high-level rules. They are the least likely to change when something external changes.
This contains all entities, enums, exceptions, interfaces, types and logic specific to the "domain" layer. This layer stands on its own and does not depend on any other layer or project.

### Application
Layer that encapsulates and implements all of the use cases of the system, or to put it simply, all application logic. It is dependent on the Core layer and has no dependencies on any other layer or project. This layer contains classes that are based on the interfaces defined within the Core layer.

### Infrastructure
Layer that contains classes for accessing external resources such as file systems, web services, SMTP, databases and so on. In this starter, we have `Infrastructure.EF.PostgreSql` layer that contains the Entity Framework logic for accessing PostgreSQL database. It is dependent on the Core layer and has no dependencies on any other layer or project. This layer also contains classes that are based on the interfaces defined within the Core layer.

### Interface
Layer that acts as a set of adapters that convert data from the format most convenient for the use case and entities to the format most convenient for some external resource like the web API or CLI. In this starter, we have `Interface.WebAPI` layer that contains the logic for mapping domain logic entities into DTOs used by the controllers for providing RESTful API to the web. This layer depends on both the Application and Infrastructure layers, however, the dependency on the Infrastructure layer is here only to support dependency injection.

So, to be frank, only *Program.cs* is referencing Infrastructure and for that reason, we previously had a separate layer called `Interface.WebAPI.Starter` that would be bootstrapping the application and its' DI container. But, to reduce the number of layers, we stopped with that practice.

### 🔄 Continous integration

We've set up a couple of basic CI pipelines for GitHub Actions. You should review them and correct your branch names, if needed.

There are actions for:

+ Continous integration (CI) - checkouts, restores packages, builds, runs unit tests and adds code coverage
+ Code quality analysis - runs code quality analysis using CodeQL
+ Release - bumps the release version and merges _changelog entries_ into `CHANGELOG.MD` and optionally adds the git tag (if configured)

## 📄 Changelog manager

We like to see a nice and up-to-date changelog between releases, but sometimes that's just too much work. To simplify this process, we've developed our own [Changelog Manager](https://github.com/Enterwell/ChangelogManager) which adheres to the [Keep a changelog](https://keepachangelog.com/en/1.0.0/#how) principles, along with a couple of integrations to help you create _changelog entries_:

+ [Visual Studio extension](https://marketplace.visualstudio.com/items?itemName=Enterwell.EnterwellChangelogVsix)
+ [CLI tool](https://github.com/Enterwell/ChangelogManager/blob/main/Enterwell.CI.Changelog.CLI/README.md#changelog-create-cli-tool)

In short, you create _changelog entries_ either by manually adding entries (files) into the `./changes` folder (if you _really_ want to do it that way...) or by using one of the above mentioned tools. As your PR's are merged into the `stage` branch (or your equivalent development branch) this folder will accumulate entries which will be bundled up into your `CHANGELOG.MD` when you create a release (ie. when you merge `stage` into `main`), and the folder will be purged. Rinse and repeat for your next release!

## ☎ Support
If you are having problems, please let us know by [raising a new issue](https://github.com/Enterwell/dotnet-starter/issues/new).

## 🪪 License
This project is licensed with the [MIT License](LICENSE).