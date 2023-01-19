using DapperCodeFirstMappings.Attributes;

namespace DapperCodeFirstMappings.Tests.Models;

[DapperEntity]
public class ThrowErrorModel
{
    [DapperColumn("")]
    public string ErrorProperty { get; set; } = string.Empty;
}