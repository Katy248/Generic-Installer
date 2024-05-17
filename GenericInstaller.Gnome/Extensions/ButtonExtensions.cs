using Gtk;

namespace GenericInstaller.Gnome.Extensions;

public static class ButtonExtensions
{
    public static TButton Child<TButton>(this TButton w, Widget child) where TButton: Button
    {
        w.SetChild(child);
        return w;
    }
}