# [2.1.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.0.1) (2018-08-11)

* Update interface and add functionality to check if matching elements are available 


# [2.0.0](https://www.nuget.org/packages/CleanCodeLabs.EntityFrameworkCore.GenericRepository/2.0.1) (2018-07-28)

* Update dependencies
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
