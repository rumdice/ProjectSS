
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
        new MenuItem { Text = "Gallery", Path = "/gallery", Icon = "photo" },
        new MenuItem { Text = "Image", Path = "/image", Icon = "photo" },
        new MenuItem { Text = "Upload", Path = "/upload", Icon = "photo" },
        new MenuItem { Text = "Item", Path = "/item", Icon = "photo" },
        new MenuItem { Text = "Temp", Path = "/temp", Icon = "photo" },
        new MenuItem { Text = "Test", Path = "/test", Icon = "photo" },
        new MenuItem { Text = "About", Path = "/about", Icon = "info" }
    };
}

