#Warning: This project has been replaced by [https://github.com/SSWConsulting/SSW.DataOnion2](SSW DataOnion 2)

#SSW.DataOnion

.NET data access layer generator. Includes repositories, interfaces, unit of work and all required generators.

For more information, please visit [http://www.sswdataonion.com](http://www.sswdataonion.com)

## The Packages

One core consideration for implementation under the onion architecture was the ability to place all components in separate projects / assemblies. To allow this, SSW Data Onion is split across multiple Nuget packages. You can install these packages to separate projects (recommended) or you can install to one test project.

### [SSW.Data.Interfaces](https://www.nuget.org/packages/SSW.Data.Interfaces/)

The core IRepository and IUnitOfWork interfaces. This is a very small package but it allows the interfaces to be added to a client project (ie the web layer) without the implementation packages (such as SSW.Data.EF) and without pulling in Entity Framework.

### [SSW.Data.EF](https://www.nuget.org/packages/SSW.Data.EF/)

This is SSW’s core Entity Framework package. This package has Entity Framework as a direct dependency.

### [SSW.Data.Entities](https://www.nuget.org/packages/SSW.Data.Entities/)

Provides some common Entity interfaces.

### [SSW.Data.DbContext.Generator](https://www.nuget.org/packages/SSW.Data.DbContext.Generator/)

Builds upon SSW.Data.EF to provide tt templates that generate a DbContext class

### [SSW.Data.RepositoryInterfaces.Generator](https://www.nuget.org/packages/SSW.Data.RepositoryInterfaces.Generator/)

Builds upon SSW.Data.EF to provide tt templates that generate repository interfaces

### [SSW.Data.Repositories.Generator](https://www.nuget.org/packages/SSW.Data.Repositories.Generator/)

Builds upon SSW.Data.EF to provide tt templates that generate repository implementations.

## Installing from Nuget

Each of the above packages can be installed from Nuget. 

PM> Install-Package SSW.Data.Repositories.Generator

## Code Generator Configuration

Each code generator package is configured by a .ttinclude file.

### DbContext.Generator

**BaseEntityClass**
Set this if all your entity classes inherit from a common base class. Otherwise leave this empty.

**BaseEntityClassDll**
Path to the assembly that contains BaseEntityClass - relative to your project's bin folder. Leave empty if BaseEntityCLass is not used

**DomainModelProjectDll**
Path to assembly that contains your domain model classes. Relative to your project's bin folder.

**DomainModelProjectNamespace**
Namespace containing your domain classes.

**DbContextName**
Name of the DBContext class to generate. When the DbContext.Generator package is installed, A partial dbContext class is also installed. This should be renamed to match the name provided here. Any further DbContext customization should be added to this non-generated file.

**DataProjectNamespace**
Namespace of the generated DbContext. Again, the details of the installed dbContext file should be changed to match.

**DomainTypeFilter**
Edit this Func&lt;&gt; to customize which domain classes to include when generating.

### RepositoryInterfaces.Generator

**BaseEntityClass**
Set this if all your entity classes inherit from a common base class. Otherwise leave this empty.

**BaseEntityClassDll**
Path to the assembly that contains BaseEntityClass - relative to your project's bin folder. Leave empty if BaseEntityCLass is not used

**DomainModelProjectDll**
Path to assembly that contains your domain model classes. Relative to your project's bin folder.

**DomainModelProjectNamespace**
Namespace containing your domain classes.

**RepositoryInterfacesProjectNamespace**
Namespace for generated repository interfaces

**DomainTypeFilter**
Edit this Func&lt;&gt; to customize which domain classes to include when generating.

### Repositories.Generator

**BaseEntityClass**
Set this if all your entity classes inherit from a common base class. Otherwise leave this empty.

**BaseEntityClassDll**
Path to the assembly that contains BaseEntityClass - relative to your project's bin folder. Leave empty if BaseEntityCLass is not used.

**BaseRepositoryName**
The base repository that all generated repositories inherit. Set to SSW.Data.EF.BaseRepository by default. Change this is you want to insert your own custom base repository.

**DomainModelProjectDll**
Path to assembly that contains your domain model classes. Relative to your project's bin folder.

**DomainModelProjectNamespace**
Namespace containing your domain classes.

**RepositoryInterfacesProjectNamespace**
Namespace for repository interfaces

**RepositoriesProjectNamespace**
Namespace for generated repository classes

**DomainTypeFilter**
Edit this Func&lt;&gt; to customize which domain classes to include when generating.

## Dependency Injection Configuration

SSW Data Onion is intended for use with an IOC / Dependency Injection container such as Autofac or Structure Map. Each class receives its dependencies via constructor parameters. Behavior at each level can be changed by providing your own implementation of the corresponding interface.

### IDbInitializer

An implementation of the standard System.Data.Entity.IDatabaseInitializer&lt;T&gt; interface from Entity Framework. Typically this is where we configure Entity Framework code-first migrations.

### Context Factory

An Implementation of the standard System.Data.Entity.Infrastructure.IDbContextFactory&lt;T&gt; interface.
The role of the ContectFactory in this design is to create new instances of your DbContext. We provide a default, generic implementation that makes the DbInitializer an explicit dependency.
http://msdn.microsoft.com/en-us/library/hh506876(v=vs.113).aspx

### Context Manager

Where Context Factory creates new DbContext instances, Context manager builds on this to manage the dbContext lifecycle. We provide a ContextManager implementation that is disposable: when the context manager is disposed, the underlying DbContext is disposed.
So we can control the lifecycle of our DbContexts by configuring the lifecycle of the Context Manager.

### Repository Implementation.

The repository implementations as generated by SSW.Data.Repositories.Generator simply depend upon the context manager.

### Unit of work pattern

The unit of work is designed to combine actions across multiple repositories into a single unit of work.
The implementation provided by SSW Data Onion supports multiple databases / DbContexts by depending upon a collection of context managers.

### Your Class

When it comes to using Data Onion, your code just needs a dependency on one or more repositories and a unit of work.
