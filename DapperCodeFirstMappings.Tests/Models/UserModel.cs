using DapperCodeFirstMappings.Attributes;

namespace DapperCodeFirstMappings.Tests.Models;

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