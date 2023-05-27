# [![](https://github.com/adamfisher/WaveApps.GraphQLBuilder/blob/main/WaveApps.GraphQLBuilder/wave_logo.png?raw=true)](https://www.waveapps.com/) WaveApps.GraphQLBuilder

[![Nuget](https://img.shields.io/nuget/dt/WaveApps.GraphQLBuilder?color=blue&label=nuget&style=plastic)](https://www.nuget.org/packages/WaveApps.GraphQLBuilder)
[![GitHub](https://img.shields.io/github/license/adamfisher/WaveApps.GraphQLBuilder?style=plastic)](https://github.com/adamfisher/WaveApps.GraphQLBuilder/blob/main/LICENSE)

A C# GraphQL query builder for [waveapps.com](https://www.waveapps.com/). Use this library to build strongly-typed queries in C# that can then be used with a GraphQL client to send commands to the Wave GraphQL API. This client was code generated against the schema definition and maps 100% of the API operations and models.

## Resources
- [Wave Developer Portal](https://developer.waveapps.com/hc/en-us/categories/360001114072-Documentation)
- [Wave API Playground (GraphQL Explorer)](https://developer.waveapps.com/hc/en-us/articles/360018937431-API-Playground)
- [Wave GraphQL API Reference](https://developer.waveapps.com/hc/en-us/articles/360019968212-API-Reference)

## Examples

For more query examples, see the [query examples](https://developer.waveapps.com/hc/en-us/sections/360006441372-Examples) in the Wave developer portal.

### List Businesses

A query constructed like this:

```c#
var query = new WaveQueryBuilder()
  .WithBusinesses(
      new BusinessConnectionQueryBuilder()
          .WithEdges(new BusinessEdgeQueryBuilder()
              .WithNode(new BusinessQueryBuilder()
                  .WithId()
                  .WithName())))
  .Build();
```

Will yield the following string from the `.Build()` method:

```graphql
query {
  businesses {
    edges {
      node {
        id
        name
      }
    }
  }
}
```

### Get User

Get the currently logged in user:

```c#
var query = new WaveQueryBuilder()
  .WithUser(new UserQueryBuilder()
      .WithId()
      .WithFirstName()
      .WithLastName()
      .WithDefaultEmail()
      .WithCreatedAt()
      .WithCreatedAt()
      .WithModifiedAt()
  ).Build();
```

Will yield the following string from the `.Build()` method:

```graphql
query {
  user {
    id
    firstName
    lastName
    defaultEmail
    createdAt
    modifiedAt
  }
}
```
