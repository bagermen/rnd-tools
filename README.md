# RND Tools

## Overview
RND Tools is a command-line application designed to streamline various development tasks for project https://github.com/bagermen/rnd-images.

## Getting Started

### Prerequisites
- .NET SDK
- Visual Studio or any other C# compatible IDE

### Building the Project
To build the project, navigate to the root directory and run the following command:
```sh
dotnet build
```

### Running the Application
To run the command-line application, navigate to the `RND.Tools.CmdLine` directory and execute:
```sh
dotnet run
```

## Configuration
Use `RNDTOOLS_CmdLine__AssemblyPath` environment variable to provide `--assembly-path` option. The option takes precedence over he environment variable.

## Dependencies
Project is expecting to have installed:
- PostgreSQL Tools

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

## License
This project is licensed under the MIT License.
