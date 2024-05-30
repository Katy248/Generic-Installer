using Adw;

namespace GenericInstaller.Gnome.Extensions.Buttons;

public static class ButtonContentExtensions
{
    public static ButtonContent Label(this ButtonContent b, string label) 
    {
        b.SetLabel(label);
        return b;
    }
    public static ButtonContent Icon(this ButtonContent b, string icon)
    {
        b.SetIconName(icon);
        return b;
    }
}