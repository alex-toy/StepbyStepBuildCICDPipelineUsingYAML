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
set ASPNETCORE_URLS="https://localhost:5000;http://localhost:5000"
dotnet C:\source\AzureDevops\StepbyStepBuildCICDPipelineUsingYAML\HelloWorldApp\HelloWorldApp\bin\Release\net6.0\HelloWorldApp.dll
```

- publish
```
dotnet publish HelloWorldApp.csproj --configuration Release --output drop
```

- local test (use bash shell)
```
dotnet drop\HelloWorldApp.dll
```

## Configure Frontend
```
cd frontend
ng build --configuration production

cd wwwroot
rm main.js ; rm polyfills.js ; rm runtime.js ; rm styles.css ; cp -r ../frontend/dist/frontend/* .
mv main.*.js main.js ; mv polyfills.*.js polyfills.js ; mv runtime.*.js runtime.js ; mv styles.*.css styles.css
```

- inside *wwwroot*, clean *index.html*

## Azure build Pipeline

- choose empty job and inherit from pipelines
<img src="/pictures/build.png" title="build pipeline"  width="900">

- restore
<img src="/pictures/build1.png" title="build pipeline"  width="900">

- build
<img src="/pictures/build2.png" title="build pipeline"  width="900">

- publish
<img src="/pictures/build3.png" title="build pipeline"  width="900">
<img src="/pictures/build31.png" title="build pipeline"  width="900">

- publish build artifact
<img src="/pictures/build4.png" title="build pipeline"  width="900">

## App Service

- choose 
<img src="/pictures/webapp.png" title="web app"  width="900">

## Book Library
We are going to be in a code-first configuration.

- install packages
```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Relational
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

## Web App Configuration
```
Server=tcp:<your-server-name>.database.windows.net,1433;Initial Catalog=<your-database-name>;Persist Security Info=False;User ID=<your-user-id>;Password=<your-password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```
