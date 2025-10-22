using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FileMonitorService;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
    public ProjectInstaller()
    {
        ServiceProcessInstaller processInstaller = new ServiceProcessInstaller
        {
            Account = ServiceAccount.LocalSystem
        };

        ServiceInstaller serviceInstaller = new ServiceInstaller
        {
            ServiceName = "FileMonitorService",
            DisplayName = "File Monitor Service",
            Description = "Monitors file deletions and logs them to a file",
            StartType = ServiceStartMode.Automatic
        };

        Installers.Add(processInstaller);
        Installers.Add(serviceInstaller);
    }
}
