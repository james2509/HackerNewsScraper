# HackerNewsScraper

## Prerequisites
This application is build using .Net Core 3.0 so the .Net Core 3.0 SDK needs to be installed.

[https://dotnet.microsoft.com/download/dotnet-core/3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)

Docker will need to be installed if you would like to run the app in the Docker container.

[https://docs.docker.com/docker-for-windows/install/](https://docs.docker.com/docker-for-windows/install/)

## Running the application
First clone the source code from GIT.

Restore the NuGet packages. Run the following command in the root of the project.

```dotnet restore```

Publish a stand-alone executable for the project. Run the following command in the root of the project. You may need to change the OS (Win-x64) if you are using a different OS.

``` dotnet publish hackernewsscraper -r win-x64 -c Release --self-contained ```

You should be able to run the HackerNewsScraper.exe from \bin\Release\netcoreapp3.0\win-x64 folder in the command line. You need to use --post switch with a number between 1 and 100 to determine the number of posts to output.

```.\HackerNewsScraper.exe --posts 42 ```
