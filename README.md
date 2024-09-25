# WorkSample

## Introduction

This solution contains a sample of my work. It is intended to give you an idea of my programming style and the structure I would use.


I employed [clean architecture](https://netsharpdev.com/images/posts/shape.png) to develop this solution. While it may be an overstatement for this small application, my goal was to demonstrate how I would approach structuring a larger project. 
The application enables basic CRUD (Create, Read, Update, Delete) operations on a 'Person' object with attributes for name and surname.

## Usage

If you want to run the application you need a SQL database. Follow these steps to get it to work:
1. Go to the `appsettings.json` in the project `WorkSample.Api` and put in your connection string
1. Install the [EF Core tools](https://learn.microsoft.com/en-us/ef/core/cli/)
1. Update the database using the EF core tools
1. Running it with Visual Studio:
	1. Set up `WorkSample.Api` and `WorkSample.Client` as startup projects
	1. Start the application
1. Running it using the console:
	1. Build the solution with `dotnet build`
	1. Start `WorkSample.Api.exe` in bin\Debug\net8.0 
	1. Start `WorkSample.Client.exe` in bin\Debug\net8.0-windows
1. Using [task](https://taskfile.dev):
	1. Run `task --list`

```shell
task: Available tasks for this project:
* build:              Building the project
* coverage:           Generating coverage report(s)
* dependencies:       Installing the dependencies
* run:                Running everything
* run-backend:        Running the backend
* run-frontend:       Running the frontend
* test:               Running the tests
```