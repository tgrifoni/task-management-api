# Task Management Api

## Introduction
This project is a .NET 9 API for a task management system with a specific set of requirements.
The system allows users to create, update, delete, and retrieve tasks.
It includes custom middleware that logs every API request, including method type and endpoint, into a file.
When a task with "High" priority is created or updated, an event is triggered that logs it separately as a critical update.
The project is secured using JWT authentication, designed in a very basic way for demonstration purposes only, and not production-ready. 

Unit and integration tests are implemented using xUnit and Moq.
The system uses SQLite for data persistence, creating/initialising a file when the API starts and deleting it upon application shutdown.
Additionally, the project utilizes AutoMapper, MediatR, Dapper, and OpenAPI/Swagger to enhance functionality.

## Prerequisites
Before setting up the project, ensure you have the following installed:
- **.NET 9 SDK**: You can download and install it from the [official .NET website](https://dotnet.microsoft.com/download/dotnet/9.0).

If you want to follow [Option 1](#option-1-cloning-the-repository), you'll need the following installed:
- **Git**: You need Git installed to clone the repository. You can download it from [here](https://git-scm.com/downloads).

## Setup Instructions
### Step 1: Installation
#### Option 1: Cloning the repository
If you prefer to clone the repository, follow these steps:

1. **Clone the repository**
    Open a terminal or command prompt and run the following command:
    ```bash
    git clone https://github.com/tgrifoni/task-management-api.git
    ```

2. **Navigate to the project directory**
    Once the repository is cloned, navigate to the project directory:
    ```bash
    cd task-management-api
    ```

### Option 2: Downloading the files directly
If you prefer to download the files directly, follow these steps:

1. **Download the files**
    Download the project files from the repository in a ZIP format and extract them to a directory on your local machine.

2. **Navigate to the project directory**
    Open a terminal or command prompt and navigate to the extracted project directory:
    ```bash
    cd path-to-extracted-directory/task-management-api
    ```
    Replace `path-to-extracted-directory` with the actual path where you extracted the files.

### Step 2: Install .NET SDK (if not already installed)
If you don't have the .NET 9 SDK installed, download and install it from the [official .NET website](https://dotnet.microsoft.com/download/dotnet/9.0). Follow the installation instructions provided on the website.

### Step 3: Build the project
Now, build the project to restore dependencies and compile the code.
Run the following command in the terminal:
```bash
dotnet build
```

## Running the project
### Using Command Line Interface (CLI)
To run the project using the CLI, execute the following command in the project directory:
```bash
dotnet run --project .\TaskManagement.Api\TaskManagement.Api.csproj
```
The API will start, and you should see output indicating that it's running on a specific URL, typically `http://localhost:5000` or similar.

### Using an Integrated Development Environment (IDE)
If you prefer using an IDE such as Visual Studio or Visual Studio Code, follow these steps:

1. **Open the project**: Open your preferred IDE and load the project by selecting the `TaskManagement.sln` solution.
2. **Build the solution**: Use the build option in your IDE to compile the project.
3. **Run the project**: Use the run/debug option in your IDE to start the project. The API will start running on a local server, typically accessible at `http://localhost:5000` or a similar URL.

### General guidelines
- You can also navigate to the Swagger page by adding `/swagger` to the end of the URL.
- To log in, you can use the `admin` username and `password` password to generate an access token. Any other combination will result in an `401 Unauthorized` response.
- The access token is set to expire in 120 seconds (2 minutes). You can change it by editing the amount of seconds in the `appsettings.json` file, under `Jwt__SecondsToExpire`.

## Troubleshooting
If you encounter any issues during setup or running the project, check the following:
- Ensure that you have the correct version of the .NET SDK installed.
- Verify that all dependencies are properly restored by running `dotnet restore`.
- Check the terminal or IDE output for any error messages and follow the suggestions provided.

For further assistance, feel free to open an issue in the GitHub repository or contact me.

## Conclusion
Thank you for taking the time to set up and run this project.
I hope this README has been helpful.
Happy coding !