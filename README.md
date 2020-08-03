# EntityPlus
An Entity Framework extension library that aims to provide the API for easier entity manipulation.

So far, it comprises facilities for:
* the retrieval of entity metadata, such as primary keys, navigation properties and their relationship types, from DbContext.
* the automatic updating of all related entities in the underlying context by simply passing in models with the desired values. The navigation property resolution and updating is done automatically, in a generic way, without the need to specify an udpate action for each navigation property.

TODOs:
* Write some unit tests when I have time (define a test Db context), possibly using in-memory EF.
* Migrate to Entity Framework Core.

Abandonded. No more interest in Entity Framework.
