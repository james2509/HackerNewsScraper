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

## Packages
* CommandLineParser (https://github.com/commandlineparser/commandline) - Library to make interpretting command line arguments a bit easier. Using this meant I did not have to write code to parse the args collection.
* NewtonsoftJson (https://www.newtonsoft.com/json) - Well known library for working with Json data
* XUnit (https://xunit.net/) - Unit test framework. I have used this a lot over the last few years so tend to stick with it.   
* FluentAssertions (https://fluentassertions.com/) - Unit test assertion framework. I like to use FluentAssertions as I feel it makes it easier to read the test code. 
* mockhttp (https://github.com/richardszalay/mockhttp) - allows easy mocking of the HttpMessageHandler that can be passed in to the HttpClient to effectively mock calls to external APIs. 


## Docker
To build and run the docker container you will first need to publish the HackerNewsScraper application. After that you will need to build a docker image (the Dockerfile is checked in to source) and then you can run it. The following commands need to be run in the root of the application.
```
dotnet publish HackerNewsScraper -c Release

docker build . -t hackernewsscraper -f Dockerfile

docker run -it hackernewsscraper --posts 12
```
