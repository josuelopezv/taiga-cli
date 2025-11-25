namespace Taiga.Cli.Configuration;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class SubCommandAttribute(string Name) : Attribute
{
    public string Name { get; set; } = Name;
    public string? Description { get; set; }
}