
public class MenuItem
{
    public string Text { get; set; }
    public string Path { get; set; }
    public string Icon { get; set; }
}

public static class SideMenu
{
    public static List<MenuItem> menuItems = new List<MenuItem>
    {
        new MenuItem { Text = "Home", Path = "/", Icon = "home" },
        new MenuItem { Text = "Gallary", Path = "/gallary", Icon = "photo" },
        new MenuItem { Text = "Upload", Path = "/upload", Icon = "photo" },
        new MenuItem { Text = "Item", Path = "/item", Icon = "photo" },
        new MenuItem { Text = "Table", Path = "/table", Icon = "photo" },
        new MenuItem { Text = "Test", Path = "/test", Icon = "photo" },
        new MenuItem { Text = "About", Path = "/about", Icon = "info" }
    };
}

