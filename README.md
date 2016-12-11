# EntityPlus
An Entity Framework extension library that aims to provide tools for easier entity manipulation.
This project is a result of restructurization of the https://github.com/Andrius-Dobrotinas/RecordLabel project.

So far, it comprises facilities for:
* retrieving entity metadata, such as primary keys, navigation properties and their relationship types, from DbContext.
* automatically updating all related entities in the underlying context by simply passing in models with the desired values. All navigation property resolution and updating is done automatically, in a generic way, without the need to specify udpate action for each navigation property.

TODOs:
* Write some unit tests when I have time (define a test Db context) possibly using EF InMemory.
* Migrate to Entity Framework Core.
