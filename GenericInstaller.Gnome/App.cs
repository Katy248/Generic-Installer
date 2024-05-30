using GenericInstaller.Gnome.Views.Views;
using GenericInstaller.Gnome.Views.Windows;
using Gio;
using ApplicationWindow = Adw.ApplicationWindow;
using FileInfo = System.IO.FileInfo;

namespace GenericInstaller.Gnome;

public class App
{
    private readonly Adw.Application _application;
    private App(string appId)
    {
        _application = Adw.Application.New(appId, ApplicationFlags.DefaultFlags);
    }

    private App SetMainWindow(ApplicationWindow window)
    {
        _application.OnActivate += (sender, args) =>
        {
            _application.AddWindow(window);
            window.Present();
        };

        return this;
    }

    public static App MainWindow(string appId)
    {
        var app = new App(appId);
        var window = new MainWindow();
        var view = new DockerListView();
        window.SetContent(view.GetView(window));
        app.SetMainWindow(window);

        return app;
    }
    /*public static App WithSetupWindow(string appId, FileInfo file)
    {
        var app = new App(appId);

        app.SetMainWindow(new SetupWindow(file));
        
        return app;
    }*/

    public int Run(string[] args)
    {
        return _application.RunWithSynchronizationContext(args);
    }
}