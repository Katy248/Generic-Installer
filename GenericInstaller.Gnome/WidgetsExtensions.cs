using Gtk;

namespace GenericInstaller.Gnome;

public static class WidgetsExtensions
{
    public static TBox WithChild<TBox>(this TBox box, Widget widget) where TBox : Gtk.Box
    {
        box.Append(widget);
        return box;
    }

    public static TWidget WithCss<TWidget>(this TWidget w, params string[] cssClasses) where TWidget : Widget
    {
        foreach (var css in cssClasses)
        {
            w.AddCssClass(css);
        }

        return w;
    }

    public static TWidget WithHalign<TWidget>(this TWidget w, Align align) where TWidget: Widget
    {
        w.Halign = align;
        return w;
    }public static TWidget WithValign<TWidget>(this TWidget w, Align align) where TWidget: Widget
    {
        w.Valign = align;
        return w;
    }
    public static TWidget WithMargin<TWidget>(this TWidget widget, int margin) where TWidget: Widget
    {
        widget.WithMarginY(margin);
        widget.WithMarginX(margin);
        return widget;
    }
    public static TWidget WithMarginX<TWidget>(this TWidget widget, int margin) where TWidget: Widget
    {
        widget.MarginStart = margin;
        widget.MarginEnd = margin;
        return widget;
    }public static TWidget WithMarginY<TWidget>(this TWidget widget, int margin) where TWidget: Widget
    {
        widget.MarginTop = margin;
        widget.MarginEnd = margin;
        return widget;
    }
}