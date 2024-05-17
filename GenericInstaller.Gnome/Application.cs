using System.Globalization;
using Adw;
using GenericInstaller.Gnome.Extensions;
using GenericInstaller.Gnome.Views.Windows;
using Gio;
using Gtk;
using ApplicationWindow = Adw.ApplicationWindow;
using FileInfo = System.IO.FileInfo;
using HeaderBar = Adw.HeaderBar;
using MessageDialog = Adw.MessageDialog;
using Task = System.Threading.Tasks.Task;

namespace GenericInstaller.Gnome;

public class Application
{
    private readonly Adw.Application _application;
    private Application(string appId)
    {
        _application = Adw.Application.New(appId, ApplicationFlags.DefaultFlags);
    }

    private Application SetMainWindow(ApplicationWindow window)
    {
        _application.OnActivate += (sender, args) =>
        {
            _application.AddWindow(window);
            window.Present();
        };

        return this;
    }

    public static Application MainWindow(string appId)
    {
        var app = new Application(appId);

        app.SetMainWindow(Views.Windows.MainWindow.New());

        return app;
    }
    public static Application WithSetupWindow(string appId, FileInfo file)
    {
        var app = new Application(appId);

        app.SetMainWindow(SetupWindow.New());
        
        return app;
    }

    public int Run(string[] args)
    {
        return _application.RunWithSynchronizationContext(args);
    }
}