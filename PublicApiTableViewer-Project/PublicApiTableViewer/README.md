# Public API Table Viewer

A C# console application that demonstrates REST API consumption skills, JSON deserialization capabilities, and DevOps awareness through GitHub Actions integration.

## Overview

This project showcases practical skills in modern C# development patterns while providing a clean, documented example for career advancement. The application consumes a free public REST API, retrieves data, and displays it in a formatted table.

## Features

- **REST API Integration**: Uses HttpClient with proper async/await patterns
- **JSON Deserialization**: Leverages System.Text.Json for data processing
- **Table Formatting**: Displays data in clean, formatted console tables
- **Error Handling**: Graceful handling of API failures and network issues
- **CI/CD Pipeline**: GitHub Actions workflow for automated builds

## Technology Stack

- **Framework**: .NET 9.0
- **Key Libraries**: System.Net.Http, System.Text.Json
- **CI/CD**: GitHub Actions
- **API**: JSONPlaceholder (free public API)

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Git
- GitHub account (for CI/CD features)

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd PublicApiTableViewer
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the application:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

## Usage

### Display All Users
Run the application without arguments to fetch and display all users:
```bash
dotnet run
```

### Display Specific User
Run the application with a user ID to display detailed information for a specific user:
```bash
dotnet run 1
```

The application will automatically fetch data from the JSONPlaceholder API and display it in a formatted table. No additional configuration is required for basic usage.

### Example Output

**All Users Table:**
```
🚀 Public API Table Viewer
============================

📋 Displaying all users

🔄 Fetching users from API...
✅ Successfully fetched 10 users

📊 Users Table:
+------+----------------------+-----------------+---------------------------+-----------------+----------------------+
| ID   | Name                 | Username        | Email                     | City            | Company              |
+------+----------------------+-----------------+---------------------------+-----------------+----------------------+
| 1    | Leanne Graham        | Bret            | Sincere@april.biz         | Gwenborough     | Romaguera-Crona      |
| 2    | Ervin Howell         | Antonette       | Shanna@melissa.tv         | Wisokyburgh     | Deckow-Crist         |
...
+------+----------------------+-----------------+---------------------------+-----------------+----------------------+

Total users displayed: 10
```

**Specific User Details:**
```
🚀 Public API Table Viewer
============================

📋 Displaying user with ID: 1

🔄 Fetching user 1 from API...
✅ Successfully fetched user: Leanne Graham

============================================================
USER DETAILS - ID: 1
============================================================
Name:        Leanne Graham
Username:    Bret
Email:       Sincere@april.biz
Phone:       1-770-736-8031 x56442
Website:     hildegard.org

Address:
  Street:    Kulas Light
  Suite:     Apt. 556
  City:      Gwenborough
  Zipcode:   92998-3874
  Location:  -37.3159, 81.1496

Company:
  Name:      Romaguera-Crona
  Catchphrase: Multi-layered client-server neural-net
  Business:  harness real-time e-markets
============================================================
```

## Project Structure

```
PublicApiTableViewer/
├── Models/           # Data models for JSON deserialization
├── Services/         # API consumer and business logic
├── Utilities/        # Table formatting and helper utilities
├── Program.cs        # Main application entry point
└── PublicApiTableViewer.csproj
```

## Available Commands

### Build Commands
```bash
# Restore NuGet packages
dotnet restore

# Build the project (Debug configuration)
dotnet build

# Build with Release configuration
dotnet build --configuration Release

# Clean build artifacts
dotnet clean
```

### Code Quality Commands
```bash
# Format code automatically
dotnet format

# Verify code formatting without making changes
dotnet format --verify-no-changes

# Format with detailed output
dotnet format --verbosity diagnostic
```

### Running the Application
```bash
# Run in Debug mode
dotnet run

# Run in Release mode
dotnet run --configuration Release

# Run with arguments (user ID)
dotnet run 1

# Run without building first
dotnet run --no-build
```

### Code Analysis
```bash
# Build with all warnings
dotnet build --configuration Release

# Run code analysis (if analyzers are configured)
dotnet build --configuration Release /p:RunAnalyzersDuringBuild=true
```

## Testing

This project currently does not include unit tests. To add testing support:

1. Create a test project:
```bash
dotnet new xunit -n PublicApiTableViewer.Tests
dotnet add PublicApiTableViewer.Tests reference PublicApiTableViewer.csproj
```

2. Run tests:
```bash
dotnet test
```

### Code Quality Checks

The project uses the following automated quality checks:

- **Code Formatting**: Enforced via `.editorconfig` and `dotnet format`
- **Build Validation**: Automated via GitHub Actions CI/CD pipeline
- **Static Analysis**: Built-in .NET analyzers through the SDK
- **Project Structure**: Validated in CI/CD workflow

To verify code quality locally:

```bash
# Check formatting
dotnet format --verify-no-changes

# Build with all checks
dotnet build --configuration Release
```

## Contributing

Contributions are welcome! This project follows standard C# conventions and includes automated build validation through GitHub Actions.

### Contribution Guidelines

1. **Code Style**: Follow the `.editorconfig` configuration file
2. **Formatting**: Run `dotnet format` before committing
3. **Build**: Ensure `dotnet build` succeeds with no warnings
4. **Documentation**: Update README.md for significant changes
5. **Pull Requests**: 
   - Create a feature branch
   - Ensure all CI checks pass
   - Provide a clear description of changes

### Development Workflow

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature-name`)
3. Make your changes
4. Format code: `dotnet format`
5. Build and verify: `dotnet build --configuration Release`
6. Commit changes: `git commit -m "Add feature: description"`
7. Push to branch: `git push origin feature/your-feature-name`
8. Create a Pull Request

## API Source

This application uses the [JSONPlaceholder API](https://jsonplaceholder.typicode.com/) for demonstration purposes. JSONPlaceholder is a free online REST API that provides fake data for testing and prototyping.

- **API Documentation**: https://jsonplaceholder.typicode.com/
- **Users Endpoint**: https://jsonplaceholder.typicode.com/users
- **License**: Free to use for testing and development

## License

MIT License - see LICENSE file for details.
