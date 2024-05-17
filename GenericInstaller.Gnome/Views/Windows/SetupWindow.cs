using Adw;
using GenericInstaller.Gnome.Extensions;
using Gtk;
using ApplicationWindow = Adw.ApplicationWindow;
using HeaderBar = Adw.HeaderBar;
using MessageDialog = Adw.MessageDialog;

namespace GenericInstaller.Gnome.Views.Windows;

public class SetupWindow : ApplicationWindow
{
    private readonly FileInfo _file;

    public SetupWindow(FileInfo file) : base()
    {
        _file = file;
    }

    protected override void Initialize()
    {
        DefaultWidth = 600;
        
        var header = Label.New("Maker").WithCss("title-1");

        var list = ListBox.New().WithCss("boxed-list");

        foreach (var i in Enumerable.Range(1, 22))
        {
            Widget w = new ActionRow
            {
                Title = "Dummy ActionRow"
            };

            switch (i % 6)
            {
                case 0:
                    var row = new EntryRow
                    {
                        Title = "Item " + i
                    };
                    w = row;
                    break;
                case 1:
                    var spin = new SpinRow
                    {
                        Title = "Spin " + i,
                        Subtitle = "Some subtitle",
                        IconName = "",
                        Adjustment = Adjustment.New(i + 1, i, (i + 2) * 2, 1, 1, 1)
                    };
                    w = spin;
                    break;
                case 2:
                    var sw = new SwitchRow
                    {
                        Title = "Switch " + i,
                        Subtitle = "Some switch for example"
                    };
                    w = sw;
                    break;
                case 3:
                    var exp = new ExpanderRow
                    {
                        Title = "Expander " + i,
                        Subtitle = "Some hidden settings",
                    };
                    exp.AddRow(new ActionRow
                    {
                        Title = "Nested item " + i + ".1",
                        Subtitle = "So cool!"
                    });
                    exp.AddRow(new SwitchRow
                    {
                        Title = "Nested switch " + i + ".2",
                        Subtitle = "Cool hidden switch",
                        Activatable = true
                    });
                    w = exp;
                    break;
                case 4:
                    var dropdown = new ComboRow
                    {
                        Title = "Combo row " + i,
                        Subtitle = "Choose one element from dropdown list"
                    };
                    dropdown.Model = StringList.New(["Default", "First option", "Second otion"]);
                    w = dropdown;
                    break;
                case 5:
                    var password = new PasswordEntryRow
                    {
                        Title = "Some secret here " + i
                    };
                    w = password;
                    break;
            }

            list.Append(w);
        }


        var submitButton = new Button
        {
            CssClasses = ["suggested-action", "pill"],
            Label = "Submit"
        };

        var resetButton = new Button
        {
            Label = "Reset",
            CssClasses = ["pill"]
        };
        resetButton.OnClicked += (sender, args) =>
        {
            var dialog = MessageDialog.New(this, "Reset parameters", "Are you shure? This action cannot be undo")
                .AppendResponse("Cancel", isDefault: true)
                .AppendResponse("Reset", ResponseAppearance.Destructive);
            
            dialog.Show();
        };

        var box = Box.New(Orientation.Vertical, 12)
            .AppendChild(header)
            .AppendChild(list)
            .AppendChild(submitButton)
            .AppendChild(resetButton)
            .WithMargin(32);

        var clamp = new Clamp
        {
            Child = box
        };

        var scroll = new ScrolledWindow
        {
            Child = clamp
        };

        var headerBar = new HeaderBar
        {
            TitleWidget = WindowTitle.New("Installer", "File 'makefile'")
        };

        var view = new ToolbarView
        {
            CssClasses = ["flat"],
            Content = scroll
        };
        view.AddTopBar(headerBar);

        SetContent(view);
    }
}