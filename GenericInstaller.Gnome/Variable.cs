namespace GenericInstaller.Gnome;

public class Variable
{
    public string ShellName { get; set; }
    public string Name { get; set; }
    public VariableType Type { get; set; } = VariableTypes.Text;
}