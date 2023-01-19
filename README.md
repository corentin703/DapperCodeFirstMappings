# DapperCodeFirstMappings

A much simple .NET Standard 2.0 library that add code first POCOs' properties to database columns mapping capabilities to Dapper.

## Compatibility
- .NET Framework >= 4.6.1
- .NET (Core) >= 2.0

## How to use ?

### Define a model

```c#
[DapperEntity]
public class UserModel
{
    [DapperColumn("usr_id")]
    public int Id { get; set; }

    [DapperColumn("usr_email")]
    public string Email { get; set; } = string.Empty;

    [DapperColumn("usr_pwd")]
    public string Password { get; set; } = string.Empty;

    [DapperColumn("usr_alias")]
    public string? Alias { get; set; }
}
```

Above columns names are matching with the SQL statement (SQLite's flavor) bellow :

```sql
CREATE TABLE users (
    usr_id INT,
    usr_email VARCHAR(255),               
    usr_pwd VARCHAR(255),
    usr_alias VARCHAR(255)
)
```

### Register mapping

#### Load from one type
```c#
DapperEntitiesMappingUtils.LoadMappingFromEntityType(typeof(UserModel));
```

#### Load all from one assembly
```c#
DapperEntitiesMappingUtils.LoadMappingsFromAssembly(typeof(UserModel).Assembly)
```
