using PublicApiTableViewer.Services;
using PublicApiTableViewer.Utilities;

namespace PublicApiTableViewer;

/// <summary>
/// Main application entry point for the Public API Table Viewer
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🚀 Public API Table Viewer");
        Console.WriteLine("============================\n");

        // Parse command line arguments
        var userId = ParseCommandLineArguments(args);

        // Setup dependency injection
        using var httpClient = new HttpClient();
        var apiService = new ApiService(httpClient);

        try
        {
            if (userId.HasValue)
            {
                // Fetch and display specific user
                await DisplaySpecificUser(apiService, userId.Value);
            }
            else
            {
                // Fetch and display all users
                await DisplayAllUsers(apiService);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Application error: {ex.Message}");
            Environment.Exit(1);
        }

        Console.WriteLine("\n👋 Thank you for using Public API Table Viewer!");

        // Only wait for key press if running in interactive mode
        if (Console.IsInputRedirected == false)
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Parses command line arguments to extract user ID
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>User ID if specified, null otherwise</returns>
    private static int? ParseCommandLineArguments(string[] args)
    {
        if (args.Length > 0 && int.TryParse(args[0], out int userId))
        {
            Console.WriteLine($"📋 Displaying user with ID: {userId}\n");
            return userId;
        }

        Console.WriteLine("📋 Displaying all users\n");
        return null;
    }

    /// <summary>
    /// Displays all users in a table format
    /// </summary>
    /// <param name="apiService">API service instance</param>
    private static async Task DisplayAllUsers(ApiService apiService)
    {
        var users = await apiService.GetUsersAsync();

        if (users.Any())
        {
            Console.WriteLine("\n📊 Users Table:");
            TableFormatter.DisplayUsersTable(users);
        }
        else
        {
            Console.WriteLine("❌ No users were retrieved. Please check your internet connection and try again.");
        }
    }

    /// <summary>
    /// Displays a specific user's details
    /// </summary>
    /// <param name="apiService">API service instance</param>
    /// <param name="userId">User ID to fetch</param>
    private static async Task DisplaySpecificUser(ApiService apiService, int userId)
    {
        var user = await apiService.GetUserByIdAsync(userId);

        if (user != null)
        {
            TableFormatter.DisplayUserDetails(user);
        }
        else
        {
            Console.WriteLine($"❌ User with ID {userId} was not found or could not be retrieved.");
        }
    }
}
