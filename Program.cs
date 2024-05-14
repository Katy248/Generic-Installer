using Adw;

var application = Application.New("ru.katy248.installer", Gio.ApplicationFlags.FlagsNone);

var mainWindow = GetMainWindow();

application.OnActivate += (Gio.Application sender, EventArgs e) =>
{
    application.AddWindow(mainWindow);
    mainWindow.Present();
};


application.RunWithSynchronizationContext(args);

Gtk.Window GetMainWindow()
{
    var header = Gtk.Label.New("Maker");
    header.AddCssClass("title-1");

    var list = Gtk.ListBox.New();
    list.AddCssClass("boxed-list");

    foreach (var i in Enumerable.Range(0, 12))
    {
        Gtk.Widget w = Adw.ActionRow.New();
        
        switch (i % 3) {
            case 0:
            var row = Adw.EntryRow.New();
            row.Title = "Item " + i;
            w = row;
            break;   
            case 1:
            var spin = Adw.SpinRow.New();
            spin.Title = "Spin " + i;
            spin.SetSubtitle("Some subtitle"); 
            ((Adw.SpinRow)spin).SetAdjustment(Gtk.Adjustment.New(i, i, i * 2, 1, 1, 1));
            w = spin;
            break;
            case 2:
            var sw = Adw.SwitchRow.New();
            sw.Title = "Switch " + i;
            w = sw;
            break;
        }
        if (w is not null)
            list.Append(w);
    }

    var submitButton = Gtk.Button.New();
    submitButton.AddCssClass("suggested-action");
    submitButton.AddCssClass("pill");
    submitButton.Label = "Submit";

    var box = Gtk.Box.New(Gtk.Orientation.Vertical, 32);
    box.Append(header);
    box.Append(list);
    box.Append(submitButton);
    box.MarginBottom = 32;
    box.MarginTop = 32;
    box.MarginStart = 32;
    box.MarginEnd = 32;
    
    var clamp = Adw.Clamp.New();
    clamp.SetChild(box);

    var page = Adw.NavigationPage.New(clamp, "Some");
    var view = Adw.NavigationView.New();
    view.Add(page);

    var window = Gtk.Window.New();
    window.SetChild(view);

    return window;
}
