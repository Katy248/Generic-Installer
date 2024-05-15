namespace GenericInstaller.Gnome;

public class Parser
{
    private readonly FileInfo _file;

    public Parser(FileInfo file)
    {
        _file = file;
    }
    public IEnumerable<Variable> Parse()
    {
        var dummy = new Variable[] { };
        return dummy;
    }
}