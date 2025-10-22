using System.Diagnostics;
using System.ServiceProcess;

namespace Task22;

public class ServiceManager : IServiceManager
{
    private const string ServiceName = "FileMonitorService";
    private readonly string _serviceExecutablePath;

    public ServiceManager(string serviceExecutablePath)
    {
        _serviceExecutablePath = serviceExecutablePath ?? throw new ArgumentNullException(nameof(serviceExecutablePath));
    }

    public async Task<bool> InstallServiceAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                if (IsServiceInstalled())
                {
                    return true;
                }

                using Process process = new Process();
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = $"create \"{ServiceName}\" binPath= \"{_serviceExecutablePath}\" start= auto";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                _ = process.Start();
                process.WaitForExit();

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                throw new ServiceOperationException("Failed to install service", ex);
            }
        });
    }

    public async Task<bool> UninstallServiceAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                if (!IsServiceInstalled())
                {
                    return true;
                }

                using Process process = new Process();
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = $"delete \"{ServiceName}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                _ = process.Start();
                process.WaitForExit();

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                throw new ServiceOperationException("Failed to uninstall service", ex);
            }
        });
    }

    public async Task<bool> StartServiceAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                using ServiceController controller = new ServiceController(ServiceName);
                if (controller.Status != ServiceControllerStatus.Running)
                {
                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running,
                        TimeSpan.FromSeconds(30));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceOperationException("Failed to start service", ex);
            }
        });
    }

    public async Task<bool> StopServiceAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                using ServiceController controller = new ServiceController(ServiceName);
                if (controller.Status != ServiceControllerStatus.Stopped)
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped,
                        TimeSpan.FromSeconds(30));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceOperationException("Failed to stop service", ex);
            }
        });
    }

    public ServiceStatus GetServiceStatus()
    {
        try
        {
            if (!IsServiceInstalled())
            {
                return ServiceStatus.NotInstalled;
            }

            using ServiceController controller = new ServiceController(ServiceName);
            return (ServiceStatus)controller.Status;
        }
        catch
        {
            return ServiceStatus.NotInstalled;
        }
    }

    public bool IsServiceInstalled()
    {
        return ServiceController.GetServices()
            .Any(service => service.ServiceName == ServiceName);
    }
}
