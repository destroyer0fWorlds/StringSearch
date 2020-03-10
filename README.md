# StringSearch

### tl;dr
This is a utility designed to make it easier to perform RESTful API calls. It allows you to quickly parse filter and order by strings like this:
```
../api/users/search?filter=(FirstName[eq]John)[and](LastName[eq]Doe)&orderBy=LastName[asc],FirstName[asc]
```

### Documentation
[GitHub Wiki](https://github.com/destroyer0fWorlds/StringSearch/wiki)

### Nuget
**Parse a filter string

Browse: https://www.nuget.org/packages/StringSearch.Filter/

Download: `Install-Package StringSearch.Filter -Version 2.0.0`

**Parse an order by string

Browse: https://www.nuget.org/packages/StringSearch.OrderBy/

Download: `Install-Package StringSearch.OrderBy -Version 2.0.0`

**Convert the results to TypeSearch

Browse: https://www.nuget.org/packages/StringSearch.Converters.TypeSearch/

Download: `Install-Package StringSearch.Converters.TypeSearch -Version 2.0.0`

### License
[MIT](https://opensource.org/licenses/MIT) - happy coding!
