namespace GenericInstaller.Gnome;

public class VariableValidator
{
    public string Name { get; set; }
    public VariableType[] AcceptedTypes { get; set; } = [];
}