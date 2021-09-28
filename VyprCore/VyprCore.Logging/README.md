# VyprCore.Logging

## Setup Up Logging In A .NET 5 Project

* This project uses NLog - see NLog https://github.com/NLog for more information.
* Add a project reference to the `VyprCore.Logging` project.
* Add `VyprCore.Logging` to the service collection container by using `services.AddVyprLogging()` in Startup.cs
* Configure the app to use the Logging service by adding `webBuilder.ConfigureVyprLogging()` in Program.cs, this also takes in an options/config model.
* This service can then be injected into your classes via the IVyprLoggingService interface. 
* This service can then be used to resolve the logging type/instance, and commit log entries.