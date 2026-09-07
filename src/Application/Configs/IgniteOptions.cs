namespace Ignite.Application.Configs;

public class IgniteOptions
{
    public const string SectionName = "Ignite";

    public string AdminEmail { get; set; } = "admin@ignitechurch.local";
    public string AdminPassword { get; set; } = "IgniteAdmin!23";
    public string ChurchName { get; set; } = "Ignite Church";
}
