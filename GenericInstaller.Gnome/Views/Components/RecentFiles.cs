using System.Globalization;
using Adw;
using Gtk;
using GenericInstaller.Gnome.Extensions;

namespace GenericInstaller.Gnome.Views.Components;

public static class RecentFiles
{
    public static Box New()
    {
        var files = new[] { "file1", "file2" };

        var box = Box.New(Orientation.Vertical, 32);
        if (files.Length == 0) return box;

        var fileListBox = ListBox.New()
            .WithCss("boxed-list");

        foreach (var file in files)
        {
            var row = new ActionRow
            {
                Title = file,
                Subtitle = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                IconName = "folder-symbolic",
            };
            fileListBox.Append(row);
        }

        return box.AppendChild(
                Label.New("Open recent files:")
                    .WithCss("title-2"))
            .AppendChild(fileListBox);
    }
}