using Adw;
using Gtk;

namespace GenericInstaller.Gnome.Extensions;

public static class ButtonExtensions
{
    public static TButton Child<TButton>(this TButton w, Widget child) where TButton: Button
    {
        w.SetChild(child);
        return w;
    }
    public static TButton Label<TButton>(this TButton w, string label) where TButton: Button
    {
        w.SetLabel(label);
        return w;
    }   
}