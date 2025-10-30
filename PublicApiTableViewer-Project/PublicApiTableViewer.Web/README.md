# PublicApiTableViewer.Web

A modern web interface for viewing and interacting with user data from JSONPlaceholder API. Built with vanilla JavaScript and Bosk8-styled CSS.

## Features

### Users List View (`#/users`)
- **Table Display**: View all users in a sortable table
- **Real-time Search**: Filter by name, username, email, or ID as you type
- **Column Sorting**: Click any column header to sort ascending/descending
- **Quick Stats**: See totals by city and company
- **Export Options**: 
  - Export filtered results to CSV
  - Copy filtered results as JSON to clipboard
- **Quick Navigation**: Jump directly to any user by ID

### User Detail View (`#/users/:id`)
- **Complete Information**: View full user details including:
  - Basic info (name, username, email, phone, website)
  -内心深处 Address (street, suite, city, zipcode)
  - Company information (name, catchphrase, business)
- **Navigation**: Use BACK button or breadcrumb to return to users list

### Controls
- **REFRESH**: Reload data from API
- **EXPORT CSV**: Download current view as CSV file
- **COPY JSON**: Copy current view as formatted JSON to clipboard
- **GO TO ID**: Enter user ID (1-10) and click GO for direct navigation
- **SEARCH**: Real-time filtering (filters table as you type)
- **HELP**: Toggle help panel with usage instructions

## Running

### Option 1: Simple HTTP Server

Open `index.html` directly in a browser, or use a local server:

```bash
# Python 3
python -m http.server 5500

# Node.js (http-server package)
npx http-server -p 5500

# PHP
php -S localhost:5500
```

Then navigate to:
```
http://localhost:5500/
http://localhost:5500/#/users
```

### Option 2: Live Server (VS Code Extension)

Use the Live Server extension in VS Code to serve the files automatically.

## Routes

The application uses hash-based navigation (single-page application):

- `#/users` - Users list view (default)
- `#/users/:id` - User detail view (e.g., `#/users/1`)

## Usage Examples

### Searching
1. Type in the SEARCH box (e.g., "Graham")
2. Table filters automatically
3. Stats update to show filtered results

### Sorting
1. Click any column header (e.g., "Name")
2. Click again to reverse sort order
3. Sort persists while filtering

### Exporting
1. **CSV**: Click "EXPORT CSV" - downloads `users.csv`
2. **JSON**: Click "COPY JSON" - copies to clipboard (button shows "COPIED" feedback)

### Navigation
- **Direct**: Enter ID in "GO TO ID" box and click GO
- **From Table**: Click "VIEW" link on any user row
- **Back**: Click "BACK" link or "USERS" breadcrumb

## Technical Details

### Architecture
- **Vanilla JavaScript**: No frameworks or dependencies
- **Hash-based Routing**: Client-side routing without page reloads
- **API Integration**: Fetches from JSONPlaceholder API
-ائه **Data Caching**: Caches API responses to minimize requests
- **Real-time Updates**: Immediate UI updates on user interactions

### Browser Compatibility
- Modern browsers with ES6+ support
- Requires Fetch API support
- Clipboard API for JSON copy feature

### API Endpoints Used
- BLACK `GET https://jsonplaceholder.typicode.com/users` - All users
- `GET https://jsonplaceholder.typicode.com/users/:id` - Single user

## File Structure

```
PublicApiTableViewer.Web/
├── index.html          # Main HTML structure
├── app.js             # Application logic and routing
├── styles.css         # Bosk8-styled CSS
└── README.md          # This file
```

## Styling

The application uses the Bosk8 design system:
- Consistent color scheme and typography
- Clean, minimal interface
- Responsive table layout
- Accessible form controls
