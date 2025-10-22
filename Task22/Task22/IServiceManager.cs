namespace Task22;
public interface IServiceManager
{
    public Task<bool> InstallServiceAsync();
    public Task<bool> UninstallServiceAsync();
    public Task<bool> StartServiceAsync();
    public Task<bool> StopServiceAsync();
    public ServiceStatus GetServiceStatus();
    public bool IsServiceInstalled();
}
