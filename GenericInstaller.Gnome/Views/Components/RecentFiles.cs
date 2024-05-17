using System.Globalization;
using Adw;
using Gtk;
using GenericInstaller.Gnome.Extensions;

namespace GenericInstaller.Gnome.Views.Components;

public class RecentFiles : Box
{
    private string[] _recentFiles = ["file1", "recent file2"];
    protected override void Initialize()
    {
        SetOrientation(Orientation.Vertical);
        SetSpacing(12);

        if (_recentFiles.Length == 0)
            return;
        
        var fileListBox = ListBox.New()
            .WithCss("boxed-list");

        foreach (var file in _recentFiles)
        {
            var row = new ActionRow
            {
                Title = file,
                Subtitle = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                IconName = "folder-symbolic",
            };
            fileListBox.Append(row);
        }

        this.AppendChild(
                Label.New("Open recent files:")
                    .WithCss("title-2"))
            .AppendChild(fileListBox);
    }
}