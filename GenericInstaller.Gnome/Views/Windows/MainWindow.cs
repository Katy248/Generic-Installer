using Adw;
using GenericInstaller.Gnome.Extensions;
using Gtk;
using ApplicationWindow = Adw.ApplicationWindow;
using HeaderBar = Adw.HeaderBar;
using MessageDialog = Adw.MessageDialog;

namespace GenericInstaller.Gnome.Views.Windows;

public static class MainWindow
{
    public static ApplicationWindow New()
    {
        var window = new ApplicationWindow
    {
        DefaultHeight = 800
    };
    
    var openFileButton = new Button
    {
        // IconName = "system-file-manager-symbolic",
        Child = Box.New(Orientation.Horizontal, 12)
            .AppendChild(Label.New("Open file"))
            .AppendChild(Image.NewFromIconName("folder-open-symbolic"))
            .WithHalign(Align.Center),
        Hexpand = true,
        CssClasses = ["suggested-action", "pill"],
    };
    openFileButton.OnClicked += async (sender, eventArgs) =>
    {
        var dialog = FileDialog.New();
        dialog.DefaultFilter = FileFilter.New();
        dialog.DefaultFilter.AddPattern("*.toml");
        var file = await dialog.OpenAsync(window);
    };

    var openUrlButton = new Button
    {
        Label = "Open url",
        CssClasses = ["pill"],
        Child = Box.New(Orientation.Horizontal, 12)
            .AppendChild(Label.New("Open url"))
            .AppendChild(Image.NewFromIconName("link-symbolic"))
            .WithHalign(Align.Center),
    };
    openUrlButton.OnClicked += (sender, eventArgs) =>
    {
        var dialog = MessageDialog
            .New(window, "Enter remote file url", "Url here:")
            .Width(500);
        
        dialog.SetExtraChild(Entry.New().WithPlaceholder("https://domain.example/file"));
        
        dialog.AddResponse("open", "Open");
        dialog.SetResponseAppearance("open", ResponseAppearance.Suggested);
        dialog.AddResponse("cancel", "Cancel");
        dialog.SetResponseAppearance("cancel", ResponseAppearance.Destructive);
        dialog.DefaultResponse = "cancel";
        dialog.Show();
    };

    var box = Box.New(Orientation.Vertical, 32)
        .WithValign(Align.Center)
        .AppendChild(Label.New("Generic Installer").WithCss("title-1"))
        /*.AppendChild(
            Button.New()
                .WithCss("card")
                .Child(
                    Box.New(Orientation.Vertical, 12)
                        .WithMargin(12)
                        .AppendChild(Label.New("\"Levels\" - The Named")
                            .WithMarkup()
                            .WithCss("title-3"))
                        .AppendChild(Label.New("I put together in a factory\nMy waste is premeditated, and my behavior is predictable\nBut the materials from which I am made are not as predictable\nI find mind self out of alignment\nDrifting further from the safe place\nI seethe structure beneath the chaos\nThe world I live in was constructed\nIf worlds can be constructed why not make my own")
                            .WithMarkup().WithHalign(Align.Center)
                            .WithCss(""))))*/
        .AppendChild(Components.RecentFiles.New())
        .AppendChild(openFileButton)
        .AppendChild(openUrlButton);

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
}