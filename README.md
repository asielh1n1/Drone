# Drone
Service via REST API that allows clients to communicate with the drones (i.e. **dispatch controller**). 
## Install 
* Install Runtime [.NET Core 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Download the project from the repository
* Run at the root of the project
```
dotnet run --project ./DroneApi/DroneApi.csproj
```
* Open the browser 
```
http://localhost:8080/swagger
```
* To run unit tests
```
dotnet test
```