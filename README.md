# Unity-Build-System

This is a sample implementation a build system geared towards automating the creation of games with the Unity Editor.

## Overview

This application comes with 3 distinct features:

- Cloning a repository using Git as a SCM to a local directory on a host machine.

- Building the game and generating logs for the selected build target.

- Publishing the artifacts and the logs of the built game at the provided location given by the user.

## How to use the application

### Pre-requisites

- You will need to have [.NET 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) installed on your machine.

- You will need [Git](https://git-scm.com/downloads) installed on your machine.

- You will need the [Unity Editor](https://unity.com/download) installed on your machine.

- Clone the repository on your machine.

- Use either the dotnet CLI or a .NET IDE allowing you to build the project's binaries.

### Learning how to use the CLI

To learn about specific commands and their arguments, run the EXE with ```--help```.

## Design, tradeoffs and considerations
See the [design document](docs/designDocument.md) file for more details.