using System.ServiceProcess;

ServiceBase[] servicesToRun = [new FileMonitorService.FileMonitorService()];
ServiceBase.Run(servicesToRun);
