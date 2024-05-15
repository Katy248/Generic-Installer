using System.Globalization;
using Adw;
using Gio;
using Gtk;
using Application = Adw.Application;
using ApplicationWindow = Adw.ApplicationWindow;
using DateTime = System.DateTime;
using HeaderBar = Adw.HeaderBar;

var application = Application.New("ru.katy248.installer", ApplicationFlags.FlagsNone);

application.OnActivate += (sender, e) =>
{
    var window = args.Length <= 0 ? GetMainWindow() : GetSelectFileWindow();
    application.AddWindow(window);
    window.Present();
};

return application.RunWithSynchronizationContext(args);

ApplicationWindow GetSelectFileWindow()
{
    var window = new ApplicationWindow();

    var openFileButton = new Button();
    openFileButton.Label = "Open file";
    openFileButton.AddCssClass("suggested-action");
    openFileButton.AddCssClass("pill");

    openFileButton.OnClicked += async (sender, eventArgs) =>
    {
        var dialog = FileDialog.New();
        var file = await dialog.OpenAsync(window);
    };

    var openUrlButton = new Button();
    openUrlButton.Label = "Open url";
    openUrlButton.AddCssClass("pill");

    var box = Box.New(Orientation.Vertical, 32);
    box.Valign = Align.Center;
    box.Append(GetRecentFilesBox());
    box.Append(openFileButton);
    box.Append(openUrlButton);

    var clamp = Clamp.New();
    clamp.MarginStart = 32;
    clamp.MarginEnd = 32;
    clamp.SetChild(box);

    var headerBar = HeaderBar.New();
    headerBar.TitleWidget = WindowTitle.New("Open file", "Selecting local or remote file");

    var view = ToolbarView.New();
    view.AddCssClass("flat");
    view.AddTopBar(headerBar);
    view.Content = clamp;


    window.Content = view;
    window.Modal = true;
    window.DefaultHeight = 800;
    window.DefaultWidth = 600;

    return window;
}

Box GetRecentFilesBox()
{
    var files = new[] { "file1", "file2" };

    var box = Box.New(Orientation.Vertical, 32);
    if (files.Length == 0) return box;

    var header = Label.New("Open recent files:");
    header.AddCssClass("title-2");

    var fileListBox = ListBox.New();
    fileListBox.AddCssClass("boxed-list");

    foreach (var file in files)
    {
        var row = new ActionRow();
        row.Title = file;
        row.Subtitle = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        fileListBox.Append(row);
    }


    box.Append(header);
    box.Append(fileListBox);

    return box;
}

ApplicationWindow GetMainWindow()
{
    var header = Label.New("Maker");
    header.AddCssClass("title-1");

    var list = ListBox.New();
    list.AddCssClass("boxed-list");

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
                    Subtitle = "Some hidden settings"
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


    var submitButton = Button.New();
    submitButton.AddCssClass("suggested-action");
    submitButton.AddCssClass("pill");
    submitButton.Label = "Submit";

    var box = Box.New(Orientation.Vertical, 12);
    box.Append(header);
    box.Append(list);
    box.Append(submitButton);
    box.MarginBottom = 32;
    box.MarginTop = 32;
    box.MarginStart = 32;
    box.MarginEnd = 32;

    var clamp = Clamp.New();
    clamp.SetChild(box);

    var scroll = new ScrolledWindow();
    scroll.SetChild(clamp);

    var headerBar = HeaderBar.New();
    headerBar.TitleWidget = WindowTitle.New("Installer", "File 'makefile'");

    var view = ToolbarView.New();
    view.AddCssClass("flat");
    view.AddTopBar(headerBar);
    view.SetContent(scroll);

    var window = new ApplicationWindow();
    window.SetContent(view);
    window.DefaultWidth = 600;


    return window;
}