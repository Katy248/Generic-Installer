using Gtk;

namespace GenericInstaller.Gnome.Extensions;

public static class BoxExtensions
{
    public static TBox AppendChild<TBox>(this TBox box, Widget widget) where TBox : Box
    {
        box.Append(widget);
        return box;
    }
}