# Step by Step - Build CI and CD Pipeline using YAML

## Manual Deploy

- build
```
cd .\HelloWorldApp\HelloWorldApp\
dotnet restore HelloWorldApp.csproj
dotnet build HelloWorldApp.csproj --configuration Release
```

- local test
```
dotnet C:\source\AzureDevops\StepbyStepBuildCICDPipelineUsingYAML\HelloWorldApp\HelloWorldApp\bin\Release\net6.0\HelloWorldApp.dll
```

- publish
```
dotnet publish HelloWorldApp.csproj --configuration Release --output drop
```

- local test
```
dotnet drop\HelloWorldApp.dll
```
