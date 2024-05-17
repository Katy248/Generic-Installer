using Gtk;

namespace GenericInstaller.Gnome.Extensions;

public static class LabelExtensions
{
    public static TLabel WithMarkup<TLabel>(this TLabel l) where TLabel : Label
    {
        // l.UseMarkup = true;
        l.SetUseMarkup(true);
        return l;
    }
}