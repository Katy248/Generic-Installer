using System.Diagnostics;
using System.Text.Json;
using Adw;
using GenericInstaller.Gnome.Extensions;
using GenericInstaller.Gnome.Extensions.Buttons;
using Gtk;
using ApplicationWindow = Adw.ApplicationWindow;
using HeaderBar = Adw.HeaderBar;

namespace GenericInstaller.Gnome.Views.Views;

public class DockerListView
{
    private List<DockerProcess>? _processes;

    public DockerListView()
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/usr/local/bin/docker",
                Arguments = "ps -a --format json -s",
                RedirectStandardOutput = true,
                // UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Exited += (sender, args) =>
        {
            _processes = [];
            while (!process.StandardOutput.EndOfStream)
            {
                _processes.Add(JsonSerializer.Deserialize<DockerProcess>(process.StandardOutput.ReadLine()));
            }
        };

        process.OutputDataReceived += (sender, args) => { Console.WriteLine(args); };
        process.Start();
        // process.BeginOutputReadLine();

        process.WaitForExit();
        while (!process.HasExited)
        {
        }

        var output = "[" + process.StandardOutput.ReadToEnd() + "]";
        Console.WriteLine(output);
    }

    public Widget GetView(ApplicationWindow parent)
    {
        while (_processes is null)
        {
        }

        var btn = Button.New()
            .WithCss("pill", "suggested-action")
            .Child(
                ButtonContent.New().Label("Press me").Icon("input-mouse-symbolic"));
        
        var box = Box.New(Orientation.Vertical, 32)
            .WithValign(Align.Center)
            .AppendChild(Label.New("Docker processes").WithCss("title-1"))
            .AppendChild(ProcessesList())
            .AppendChild(btn);

        var clamp = Clamp.New().WithMarginX(32);
        clamp.SetChild(box);

        var headerBar = HeaderBar.New();
        headerBar.TitleWidget = WindowTitle.New("Docker processes", "docker ps -a -s");

        var view = new ToolbarView
        {
            CssClasses = ["flat"],
            Content = clamp,
        };
        view.AddTopBar(headerBar);


        return view;
    }

    private Gtk.ListBox ProcessesList()
    {
        var box = ListBox.New().WithCss("boxed-list");

        foreach (var process in _processes)
        {
            var row = ExpanderRow.New()
                    .WithTitle(process.Names)
                    .AppendRow(
                        ActionRow.New().WithTitle("Command").WithSubtitle(process.Command, true).WithCss("property"))
                    .AppendRow(
                        ActionRow.New().WithTitle("Image").WithSubtitle(process.Image, true).WithCss("property"))
                    .AppendRow(
                        ActionRow.New().WithTitle("Ports").WithSubtitle(process.Ports, true).WithCss("property"))
                    .AppendRow(
                        ActionRow.New().WithTitle("Size").WithSubtitle(process.Size, true).WithCss("property"))
                    .AppendRow(
                        ActionRow.New().WithTitle("Created").WithSubtitle(process.CreatedAt, true).WithCss("property"))
                    .AppendRow(
                        ActionRow.New().WithTitle("Labels").WithSubtitle(process.Labels, true).WithCss("property"))
                ;
            if (process.State == DockerProcessStates.Running)
            {
                row.AppendRow(
                    ActionRow.New().WithTitle("Stop").Activatable().WithCss("destructive-action"));
            }else if (process.State == DockerProcessStates.Created || process.State == DockerProcessStates.Exited)
            {
                row.AppendRow(
                    ActionRow.New().WithTitle("Run").Activatable().WithCss("suggested-action"));
            }
            
            row.Subtitle = process.Status + "/" + process.State;
            row.Activatable = false;
            box.Append(row);
        }

        return box;
    }
}

public record DockerProcess(
    string Command,
    string CreatedAt,
    string ID,
    string Image,
    string Labels,
    string LocalVolumes,
    string Mounts,
    string Names,
    string Networks,
    string Ports,
    string RunningFor,
    string Size,
    string State,
    string Status);

public static class DockerProcessStates
{
    public const string Created = "created";
    public const string Exited = "exited";
    public const string Running = "running";
}