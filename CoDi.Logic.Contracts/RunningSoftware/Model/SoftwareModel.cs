namespace CoDi.Logic.Contracts.RunningSoftware.Model;

public class SoftwareModel
{
    public required string Name { get; set; }

    public string Metadata { get; set; } = string.Empty;
}