using Adw;
using Gtk;

namespace GenericInstaller.Gnome.Extensions;

public static class PreferencesRowExtensions
{
    public static TRow WithTitle<TRow>(this TRow row, string title, bool selectable = false) where TRow : PreferencesRow
    {
        row.SetTitle(title);
        row.SetTitleSelectable(selectable);
        return row;
    }

    public static TRow WithSubtitle<TRow>(this TRow row, string subtitle, bool selectable) where TRow : ActionRow
    {
        row.SetSubtitle(subtitle);
        row.SetSubtitleSelectable(selectable);
        return row;
    }
    public static TRow Activatable<TRow>(this TRow row, bool activatable = true) where TRow : ListBoxRow
    {
        row.SetActivatable(activatable);
        return row;
    }

    public static TExpanderRow AppendRow<TExpanderRow>(this TExpanderRow expanderRow, PreferencesRow newRow)
        where TExpanderRow : ExpanderRow
    {
        expanderRow.AddRow(newRow);
        return expanderRow;
    }
}