# Welcome to Pokedex

This app providing some pokemon API to get pokemon basic details and fun translation of description by passing name of pokemon.

-----------------------------------
-----------------------------------

# Build and run project

## Requirements

The project requires [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).

## Compatible IDEs

- Visual Studio 2019

## Useful commands

From the terminal/shell/command line tool, use the following commands to build, test and run the API.

### Build the project - 

```console
$ dotnet build
```

```Run this command in Pokedex project folder where we have Pokedex c# project file```


### Run the tests

````Visual Studio 2019 - Test Explorer or select project and Run Tests
````

```console
$ dotnet test Pokedex.Test
```

### Run the application

Run the application which will be listening on port `5000`.

```console
$ dotnet run Pokedex
```

```Run this command in Pokedex project folder where we have Pokedex c# project file```

-----------------------------------
-----------------------------------

# Avaialble API with payload
Use postman or IE browser to test API

### First Http GET Api to get Pokemon basic Information
Url - http://localhost:5000/pokemon/mewtwo 
```where mewtwo is Pokemon Name```

### Second Http GET Api to get Pokemon basic Information with Fun Translation
Url - http://localhost:5000/pokemon/translated/mewtwo
```where mewtwo is Pokemon Name and there are 2 fun translation present - Yoda and Shakespeare based on conditions```

-----------------------------------
-----------------------------------

# Build and Run with Docker - WSL Docker with Ubuntu or Linux

For Docker Build use and run sh file - docker_build.sh
& For Run use and execute file - docker_run.sh

```Run above commands in main App folder where we have solution file```

```Go to Path and run these files with linux CLI or copy command from these files and directly run on CLI.```

```URL will remain same in case of docker also```

-----------------------------------
-----------------------------------

# Production Settings needs to be done 

In appsettings.json file - Need to update property - It can be done with docker file or by Jenkins or other CI-CD
Current "ApplicationEnvironment": "Local" --> to --> "ApplicationEnvironment": "Production"

and Update URL for production API in this file

# Tasks in pipline for further production API

1) Security implementation(Authorization and Token Based Authentication).
2) Caching implementation
3) Versioning of API.
4) CORS.