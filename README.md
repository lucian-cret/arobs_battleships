# Arobs Battleships

A simple Battleships game written in ASP.NET Core MVC and jQuery.  
xUnit was used for unit testing.

### Prerequisites

You will need to download and install .NET Core 2.2 or later.

### Installing

Download a copy or clone the repo.

### Running the application

Head to the location of the source .csproj file using the shell of your choice and execute the following command
```
dotnet run
```
Once you see this message
```
Now listening on: http://[::]:5000
```
you can open a browser and go to http://localhost:5000.  
Choose the grid settings and start shooting.

### Good to know
* There are 2 types of ships that can be placed on the grid:
    - Battleships which take up 5 squares each
    - Destroyers which take up 4 squares each
* If the size of the grid is smaller than double the number of the squares occupied by the ships, then a warning will be shown and you cannot start a new game.
