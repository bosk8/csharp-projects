# Public API Table Viewer

A full-stack application for viewing and interacting with public API data. Consists of a C# .NET console application and a modern web interface.

## Project Overview

This project demonstrates:
- **REST API Integration** using HttpClient with async/await patterns
- **JSON Deserialization** with System.Text.Json
- **Modern Web UI** with vanilla JavaScript and hash-based routing
- **Table Formatting** with clean, readable output
- **Error Handling** and graceful failure management

## Project Structure

```
.
├── PublicApiTableViewer/          # C# .NET 9.0 Console Application
│   ├── Models/                    # Data models for JSON deserialization
│   ├── Services/                  # API consumer and business logic
│   ├── Utilities/                 # Table formatting utilities
│   └── Program.cs                 # Main entry point
│
├── PublicApiTableViewer.Web/      # Web Interface
│   ├── index.html                 # Main HTML file
│   ├── app.js                     # JavaScript application logic
│   ├── styles.css                 # Bosk8-styled CSS
│   └── README.md                  # Web app documentation
│
└── README.md                      # This file
```

## Features

### Console Application (`PublicApiTableViewer`)
- Display all users in a formatted table
- Display specific user details by ID
- Command-line interface
- Comprehensive error handling

### Web Application (`PublicApiTableViewer.Web`)
- **Users List View**: Browse all users in a sortable, searchable table
- **User Detail View**: View complete user information
- **Search Functionality**: Real-time filtering by name, username, or email
- **Quick Navigation**: Jump to specific users by ID
- **Export Features**: Download data as CSV or copy as JSON
- **Table Sorting**: Click column headers to sort ascending/descending
- **Responsive Design**: Clean, modern UI with Bosk8 styling
- **Hash-based Routing**: Navigate between views without page reloads

## Technology Stack

### Backend (Console)
- **Framework**: .NET 9.0
- **Key Libraries**: 
  - System.Net.Http (API consumption)
  - System.Text.Json (JSON deserialization)

### Frontend (Web)
- **Vanilla JavaScript**: No frameworks, pure ES6+
- **Hash-based Routing**: Single-page application navigation
- **CSS**: Custom Bosk8 design system
- **API**: JSONPlaceholder REST API

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later (for console app)
- Modern web browser (for web app)
- Git

### Running the Console Application

1. Navigate to the console application directory:
```bash
cd PublicApiTableViewer
```

2. Restore and build:
```bash
dotnet restore
dotnet build
```

3. Run the application:
```bash
# Display all users
dotnet run

# Display specific user
dotnet run 1
```

For more details, see [PublicApiTableViewer/README.md](./PublicApiTableViewer/README.md)

### Running the Web Application

1. Navigate to the web application directory:
```bash
cd PublicApiTableViewer.Web
```

2. Start a local web server:
```bash
# Using Python 3
python -m http.server 5500

# Using Node.js (http-server)
npx http-server -p 5500

# Using PHP
php -S localhost:5500
```

3. Open in browser:
```
http://localhost:5500/
```

4. Navigate to users:
```
http://localhost:5500/#/users
```

The web application features:
- Real-time search filtering
- Column sorting (click headers)
- Export to CSV
- Copy JSON to clipboard
- Direct navigation via "GO TO ID"
- User detail pages with full information

For more details, see [PublicApiTableViewer.Web/README.md](./PublicApiTableViewer.Web/README.md)

## Web Application Features in Detail

### Search & Filter
- Type in the search box to filter users in real-time
- Searches across: ID, Name, Username, Email
- Stats update automatically to show filtered results

### Table Sorting
- Click any column header to sort
- Click again to reverse sort order
- Works with all columns: ID, Name, Username, Email, City, Company

### Export Options
- **EXPORT CSV**: Downloads current view as `users.csv`
- **COPY JSON**: Copies formatted JSON to clipboard

### Navigation
- **GO TO ID**: Enter user ID (1-10) and click GO to jump directly to user details
- **VIEW Links**: Click any "VIEW" button to see full user details
- **Breadcrumbs**: Navigate back using breadcrumb links
- **BACK Button**: Return to users list from detail page

### User Detail View
Shows complete user information:
- Basic Info: Name, Username, Email, Phone, Website
- Address: Street, Suite, City, Zipcode
- Company: Name, Catchphrase, Business description

## API Source

This application uses the [JSONPlaceholder API](https://jsonplaceholder.typicode.com/):
- **Endpoint**: `https://jsonplaceholder.typicode.com/users`
- **Documentation**: https://jsonplaceholder.typicode.com/
- **License**: Free for testing and development

## Development

### Building the Console App

```bash
cd PublicApiTableViewer
dotnet build --configuration Release
```

### Code Quality

```bash
# Format code
dotnet format

# Verify formatting
dotnet format --verify-no-changes
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Ensure code is formatted: `dotnet format`
5. Commit your changes (`git commit -m 'Add amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

### Contribution Guidelines
- Follow existing code style
- Update documentation for significant changes
- Test both console and web applications
- Ensure builds succeed without warnings

## License

MIT License - see LICENSE file in PublicApiTableViewer directory for details.

## Author

Built with modern C# and vanilla JavaScript, demonstrating practical API consumption and web development skills.

