using Adw;
using GenericInstaller.Gnome.Views.Views;

namespace GenericInstaller.Gnome.Views.Windows;

public class MainWindow : ApplicationWindow
{
    protected override void Initialize()
    {
        // var setup = new SetupView(this)
        DefaultHeight = 800;
        DefaultWidth = 600;
    }
}