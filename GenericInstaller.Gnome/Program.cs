using GenericInstaller.Gnome;

var app = Application.WithSetupWindow("ru.katy248.installer", new FileInfo("/some/some.txt"));

return app.Run(args);