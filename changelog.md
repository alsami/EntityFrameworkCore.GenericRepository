# [4.0.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/4.0.0) (2019-09-23)

## Features

* Same as previous pre-release!

## Breaking changes

* Same as previous pre-release! You need to run on .NET-Core 3 now!

# [4.0.0-rc.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/4.0.0-rc.0) (2019-09-18)

## Breaking changes

* All the Entity-Framework libraries have been update to `.NET-Standard 2.1`. If you rely on any of those within a `.NET-Standard 2.0` library, this won't install!

## Features

* `.NET Core 3` and `.NET-Standard 2.1` support!

# [3.0.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/3.0.0) (2019-05-17)

## Breaking changes

The redundant abstraction of `DbContext` has been removed. You now need to derive directly from `DbContext`.

Before 3.0.0:

```csharp
public class MyDbContext : GenericRepositoryContext 
{
  // more here
}
```

After:

```csharp
public class MyDbContext : DbContext 
{
  // more here
}
```

Please check out the updated samples and docs.

## Features

This release adds a bunch of new APIs

```csharp
void AddMany(IEnumerable<TEntity> entities); // add many entities sync

Task AddManyAsync(IEnumerable<TEntity> entities); // add many entities async

Task<IEnumerable<TType>> GetAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc) where TType : class; // load all into a projection sync

Task<IEnumerable<TType>> GetAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc) where TType : class; // load all into a projection async

IEnumerable<TType> GetAll<TType>(Expression<Func<TEntity, TType>> projectToFunc, 
    params Expression<Func<TEntity, object>>[] includes) where TType : class; // load all into a projection with includes sync
            
IEnumerable<TType> GetAll<TType>(Expression<Func<TEntity, TType>> projectToFunc,
    params Expression<Func<TEntity, object>>[] includes) where TType : class; // load all into a projection with includes async
    
IEnumerable<TType> FindAll<TType>(Expression<Func<TEntity, TType>> projectToFunc, 
    Expression<Func<TEntity, bool>> predicate) where TType : class; // find all and load result into a projection sync
    
IEnumerable<TType> FindAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc, 
    Expression<Func<TEntity, bool>> predicate) where TType : class; // find all and load result into a projection async

IEnumerable<TType> FindAll<TType>(Expression<Func<TEntity, TType>> projectToFunc, Expression<Func<TEntity, bool>> predicate, 
    params Expression<Func<TEntity, object>>[] includes) where TType : class; // find all with includes and load result into a projection sync
    
IEnumerable<TType> FindAllAsync<TType>(Expression<Func<TEntity, TType>> projectToFunc, Expression<Func<TEntity, bool>> predicate, 
    params Expression<Func<TEntity, object>>[] includes) where TType : class; // find all with includes and load result into a projection async 
```

For more information please have a look at the updated samples and tests.

# [2.2.4](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.2.4) (2019-04-18)

## Chore

* Update ef-core dependencies to 2.2.4

## Chore

* Add project-icon
* Update copyright

# [2.2.1](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.2.1) (2019-02-03)

## Chore

* Update ef-core dependencies to 2.2.1
* Update test and sample projects to .NET-Core 2.2

# [2.2.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.2.0) (2018-12-11)

## Chore

* Update ef-core dependencies to 2.2.0

# [2.1.1](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.1.1) (2018-08-27)

## Chore

* Update ef-core dependencies to 2.1.2

# [2.1.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.1.0) (2018-08-11)

## Features

* Update interface and add functionality to check if matching elements are available 

# [2.0.1](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.0.1) (2018-07-28)


## Chore

* Update dependencies

## Features

* Remove entityframework-dependency from abstractions project

# [2.0.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.0.0) (2018-07-26)

## Breaking changes

* Move the dbcontext abstractions to implementation package, so abstractions do not know of implementation details

Before version 2.0.0:
```c#
using EntityFrameworkCore.GenericRepository.Abstractions;
```

With version 2.0.0:
```c#
using EntityFrameworkCore.GenericRepository;
```

# [1.3.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/1.3.0) (2018-07-18)

## Features

* Add paging-repository implementation
* Add base implementation for usage in both repositories 

# [1.2.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/1.2.0) (2018-07-16)

## Features

* Add most common basetypes containing only the primary key of an entity (string, long, int and guid)

# [1.0.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/1.0.0) (2018-07-14)

## Features

* Reading all entities of a specific type async and sync
* Reading all entities of a specific type async and sync including it's children
* Searching all entities of a specific type async and sync with a given predicate including it's children
* Searching for an entity of a specific type async and sync with a given id
* Searching for an entity of a specific type async and sync with a given id and including it's children
* Searching for an entity of a specific type async and sync using a predicate
* Searching for an entity of a specific type async and sync using a predicate and including it's children
* Counting all entities of a specific type async and sync
* Creat a query for a given type so you can add more dynamic code within your code
* Adding an entity of a specific type async and sync
* Updating an entity of a specific type async and sync
* Saving changes async and sync