using Tomlet.Attributes;

namespace GenericInstaller.Shared;

public class InstallConfiguration
{
    [TomlProperty("variables")]
    public IEnumerable<Variable> Variables { get; set; }
    [TomlProperty("make")]
    public MakeConfiguration Make { get; set; }
}

public class Variable
{
    [TomlProperty("name")]
    public string Name { get; set; }
    [TomlProperty("display")]
    public DisplayConfiguration Display { get; set; }
    [TomlProperty("type")]
    public VariableType Type { get; set; } = VariableType.String;
    [TomlProperty("allowed_values")]
    public IEnumerable<string> AllowedValues { get; set; }
}

public class MakeConfiguration
{
    [TomlProperty("file")]
    public string File { get; set; }
}

public class DisplayConfiguration()
{
    [TomlProperty("name")]
    public string Name { get; set; } 
    [TomlProperty("tooltip")]
    public string Tooltip { get; set; } 
    [TomlProperty("description")]
    public string Description { get; set; }
    [TomlProperty("style")]
    public DisplayStyle Style { get; set; } = DisplayStyle.None;
}

public enum DisplayStyle
{
    None,
    Dropdown,
    Password
}

public enum VariableType
{
    String,
    Bool,
    Number
}