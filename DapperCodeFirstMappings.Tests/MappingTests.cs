using Dapper;
using DapperCodeFirstMappings.Exceptions;
using DapperCodeFirstMappings.Tests.Models;
using Microsoft.Data.Sqlite;

namespace DapperCodeFirstMappings.Tests;

public class MappingTests
{
    private static readonly string ConnectionString = new SqliteConnectionStringBuilder()
    {
        Mode = SqliteOpenMode.Memory,
        Cache = SqliteCacheMode.Shared,
    }.ToString();

    private readonly List<UserModel> _users = new()
    {
        new UserModel()
        {
            Id = 1,
            Email = "test@email.com",
            Password = "UltraSecurePa$$word!",
        },
        new UserModel()
        {
            Id = 2,
            Email = "test2@email.com",
            Password = "PopcornCaramel<3",
        },
        new UserModel()
        {
            Id = 3,
            Email = "alias@email.com",
            Password = "I<3Gollum",
            Alias = "Sméagol",
        },
    };

    private SqliteConnection BuildDb()
    {
        SqliteConnection db = new SqliteConnection(ConnectionString);
        db.Open();

        db.Execute(@"
            CREATE TABLE users (
                usr_id INT,
                usr_email VARCHAR(255),               
                usr_pwd VARCHAR(255),
                usr_alias VARCHAR(255)
            )
        ");

        foreach (UserModel user in _users)
        {
            db.Execute(@$"
                INSERT INTO users (usr_id, usr_email, usr_pwd, usr_alias)
                VALUES (
                    @{nameof(UserModel.Id)},
                    @{nameof(UserModel.Email)},
                    @{nameof(UserModel.Password)},
                    @{nameof(UserModel.Alias)}
                )
            ", user);
        }

        return db;
    }

    [Fact]
    public void MapUser()
    {
        DapperEntitiesMappingUtils.LoadMappingFromEntityType(typeof(UserModel));

        using SqliteConnection db = BuildDb();
        List<UserModel> users = 
            db.Query<UserModel>("SELECT * FROM users ORDER BY usr_id")
                .ToList();

        Assert.Equal(_users.Count, users.Count);

        for (int userIdx = 0; userIdx < _users.Count; userIdx++)
        {
            Assert.Equal(_users[userIdx].Id, users[userIdx].Id);
            Assert.Equal(_users[userIdx].Email, users[userIdx].Email);
            Assert.Equal(_users[userIdx].Password, users[userIdx].Password);
            Assert.Equal(_users[userIdx].Alias, users[userIdx].Alias);
        }
    }

    [Fact]
    public void InvalidMapping()
    {
        Assert.Throws<DapperColumnEmptyException>(() =>
        {
            DapperEntitiesMappingUtils.LoadMappingFromEntityType(typeof(ThrowErrorModel));
        });
    }
}