using Gtk;

namespace GenericInstaller.Gnome.Extensions;

public static class EntryExtensions
{
    public static TEntry WithPlaceholder<TEntry>(this TEntry widget, string placeholder) where TEntry: Entry
    {
        widget.PlaceholderText = placeholder;
        return widget;
    }
}