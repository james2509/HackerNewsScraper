# HackerNewsScraper

## Prerequisites
This application is built using .Net Core 3.0 so the .Net Core 3.0 SDK needs to be installed.

[https://dotnet.microsoft.com/download/dotnet-core/3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)

Docker will need to be installed if you would like to run the app in the Docker container.

[https://docs.docker.com/docker-for-windows/install/](https://docs.docker.com/docker-for-windows/install/)

If you are running Windows you may need the Windows SubSystem for Linux

[https://docs.microsoft.com/en-us/windows/wsl/install-win10](https://docs.microsoft.com/en-us/windows/wsl/install-win10)

## Running the application
### Build and run locally
First clone the source code from GIT.

Restore the NuGet packages. Run the following command in the root of the project.

```dotnet restore```

Publish a stand-alone executable for the project. Run the following command in the root of the project. You may need to change the OS if you are using a different OS.

### Windows 10
``` dotnet publish hackernewsscraper -r win-x64 -c Release --self-contained ```

### Ubunutu
``` dotnet publish hackernewsscraper -r ubuntu.18.04-x64 -c Release --self-contained ```

### OSX (Untested)
``` dotnet publish hackernewsscraper -r osx.10.11-x64 -c Release --self-contained ```

You should be able to run the HackerNewsScraper.exe from \bin\Release\netcoreapp3.0\win-x64 folder in the command line. Execute and alter the command appropriately for the OS that the applicatio will run on. 
You need to use --post switch with a number between 1 and 100 to determine the number of posts to output.

```.\HackerNewsScraper.exe --posts 42 ```

### Pull and run from Docker
Docker must be installed locally
A Docker Hub account is required. You can create one [here](https://hub.docker.com)

First login to Docker Hub

```docker login```

Pull the image from Docker Hub

```docker pull james2509/hackernewsscraper```

Run the application from the image

```docker pull james2509/hackernewsscraper```

## Packages
* CommandLineParser (https://github.com/commandlineparser/commandline) - Library to make interpretting command line arguments a bit easier. Using this meant I did not have to write code to parse the args collection.
* NewtonsoftJson (https://www.newtonsoft.com/json) - Well known library for working with Json data
* XUnit (https://xunit.net/) - Unit test framework. I have used this a lot over the last few years so tend to stick with it.   
* FluentAssertions (https://fluentassertions.com/) - Unit test assertion framework. I like to use FluentAssertions as I feel it makes it easier to read the test code. 
* mockhttp (https://github.com/richardszalay/mockhttp) - allows easy mocking of the HttpMessageHandler that can be passed in to the HttpClient to effectively mock calls to external APIs. 


## Docker
### Build and Run Docker image
To build and run the docker image you will first need to publish the HackerNewsScraper application. After that you will need to build a docker image (the Dockerfile is checked in to source) and then you can run it. The following commands need to be run in the root of the application.
```
dotnet publish HackerNewsScraper -c Release

docker build . -t hackernewsscraper -f Dockerfile

docker run -it hackernewsscraper --posts 12
```

### Publish image to Docker Hub
To create and publish the container to Docker hub (you will need a Docker account and need to be logged in). Run the following commands.

Login to Docker

```docker login```

Get Image Id

```docker images```

Tag image

```docker tag {image id} {docker hub user id}/hackernewsscraper```

Push image to Docker Hub

```docker push {docker hub user id}/hackernewsscraper```

### Pull and Run image from Docker hub

Login to Docker

```docker login```

Pull image 

```docker pull {docker hub user id}/hackernewsscraper```

Run image

```docker run -it {docker hub user id}/hackernewsscraper --posts 12```


