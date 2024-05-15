using System.Globalization;
using Adw;
using Gio;
using Gtk;
using Application = Adw.Application;
using ApplicationWindow = Adw.ApplicationWindow;
using DateTime = System.DateTime;
using HeaderBar = Adw.HeaderBar;
using GenericInstaller.Gnome;

var application = Application.New("ru.katy248.installer", ApplicationFlags.FlagsNone);

application.OnActivate += (sender, e) =>
{
    var window = args.Length > 0 ? GetMainWindow() : GetSelectFileWindow();
    application.AddWindow(window);
    window.Present();
};

return application.RunWithSynchronizationContext(args);

ApplicationWindow GetSelectFileWindow()
{
    var window = new ApplicationWindow
    {
        DefaultHeight = 800
    };

    var openFileBtnBox = Box.New(Orientation.Horizontal, 12);
    openFileBtnBox.Halign = Align.Center;
    openFileBtnBox.Append(Label.New("Open file"));
    openFileBtnBox.Append(Image.NewFromIconName("folder-open-symbolic"));
    
    var openFileButton = new Button
    {
        // IconName = "system-file-manager-symbolic",
        Child = Box.New(Orientation.Horizontal, 12)
            .WithChild(Label.New("Open file"))
            .WithChild(Image.NewFromIconName("folder-open-symbolic"))
            .WithHalign(Align.Center),
        Hexpand = true,
        CssClasses = ["suggested-action", "pill"],
    };
    openFileButton.OnClicked += async (sender, eventArgs) =>
    {
        var dialog = FileDialog.New();
        var file = await dialog.OpenAsync(window);
    };

    var openUrlButton = new Button
    {
        Label = "Open url",
        CssClasses = ["pill"],
        Child = Box.New(Orientation.Horizontal, 12)
            .WithChild(Label.New("Open url"))
            .WithChild(Image.NewFromIconName("link-symbolic"))
            .WithHalign(Align.Center),
    };

    var box = Box.New(Orientation.Vertical, 32)
        .WithValign(Align.Center)
        .WithChild(GetRecentFilesBox())
        .WithChild(openFileButton)
        .WithChild(openUrlButton);

    var clamp = Clamp.New().WithMarginX(32);
    clamp.SetChild(box);

    var headerBar = HeaderBar.New();
    headerBar.TitleWidget = WindowTitle.New("Open file", "Selecting local or remote file");

    var view = new ToolbarView
    {
        CssClasses = ["flat"],
        Content = clamp,
    };
    view.AddTopBar(headerBar);


    window.Content = view;
    window.Modal = true;
    window.DefaultWidth = 600;

    return window;
}

Box GetRecentFilesBox()
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

    return box.WithChild(
            Label.New("Open recent files:")
            .WithCss("title-2"))
        .WithChild(fileListBox);
}

ApplicationWindow GetMainWindow()
{
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

    var box = Box.New(Orientation.Vertical, 12)
        .WithChild(header)
        .WithChild(list)
        .WithChild(submitButton)
        .WithChild(resetButton)
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

    var window = new ApplicationWindow
    {
        DefaultWidth = 600
    };
    window.SetContent(view);


    return window;
}