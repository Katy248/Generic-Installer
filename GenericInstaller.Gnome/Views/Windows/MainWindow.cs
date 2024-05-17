using Adw;
using GenericInstaller.Gnome.Extensions;
using Gtk;
using ApplicationWindow = Adw.ApplicationWindow;
using HeaderBar = Adw.HeaderBar;
using MessageDialog = Adw.MessageDialog;

namespace GenericInstaller.Gnome.Views.Windows;

public class MainWindow : ApplicationWindow
{
    protected override void Initialize()
    {
        DefaultHeight = 800;

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
            dialog.Modal = true;
            var file = await dialog.OpenAsync(this);
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
                .New(this, "Enter remote file url", "Url here:")
                .Width(500)
                .AppendResponse("Open", ResponseAppearance.Suggested, true)
                .AppendResponse("Close", ResponseAppearance.Destructive);

            dialog.SetExtraChild(Entry.New().WithPlaceholder("https://domain.example/file"));
            
            dialog.Show();
        };
        var bntBox = Box.New(Orientation.Vertical, 12)
            .AppendChild(openFileButton)
            .AppendChild(openUrlButton);

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
            .AppendChild(new Components.RecentFiles())
            .AppendChild(bntBox);

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


        Content = view;
        DefaultWidth = 600;
    }
}