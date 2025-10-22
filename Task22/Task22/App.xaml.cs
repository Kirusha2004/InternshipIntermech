using System.Windows;
using Application = System.Windows.Application;

namespace Task22;

public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
