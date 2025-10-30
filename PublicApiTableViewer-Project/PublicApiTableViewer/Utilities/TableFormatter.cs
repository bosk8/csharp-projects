using PublicApiTableViewer.Models;

namespace PublicApiTableViewer.Utilities;

/// <summary>
/// Utility class for formatting data into console tables
/// </summary>
public static class TableFormatter
{
    /// <summary>
    /// Formats a list of users into a console table
    /// </summary>
    /// <param name="users">List of users to display</param>
    public static void DisplayUsersTable(List<User> users)
    {
        if (users == null || !users.Any())
        {
            Console.WriteLine("No users to display.");
            return;
        }

        // Define column widths
        const int idWidth = 4;
        const int nameWidth = 20;
        const int usernameWidth = 15;
        const int emailWidth = 25;
        const int cityWidth = 15;
        const int companyWidth = 20;

        // Print header
        PrintSeparatorLine(idWidth, nameWidth, usernameWidth, emailWidth, cityWidth, companyWidth);
        PrintTableRow("ID", "Name", "Username", "Email", "City", "Company",
                     idWidth, nameWidth, usernameWidth, emailWidth, cityWidth, companyWidth);
        PrintSeparatorLine(idWidth, nameWidth, usernameWidth, emailWidth, cityWidth, companyWidth);

        // Print user data
        foreach (var user in users)
        {
            PrintTableRow(
                user.Id.ToString(),
                TruncateString(user.Name, nameWidth),
                TruncateString(user.Username, usernameWidth),
                TruncateString(user.Email, emailWidth),
                TruncateString(user.Address.City, cityWidth),
                TruncateString(user.Company.Name, companyWidth),
                idWidth, nameWidth, usernameWidth, emailWidth, cityWidth, companyWidth
            );
        }

        PrintSeparatorLine(idWidth, nameWidth, usernameWidth, emailWidth, cityWidth, companyWidth);
        Console.WriteLine($"\nTotal users displayed: {users.Count}");
    }

    /// <summary>
    /// Formats a single user into a detailed console display
    /// </summary>
    /// <param name="user">User to display</param>
    public static void DisplayUserDetails(User user)
    {
        if (user == null)
        {
            Console.WriteLine("No user data to display.");
            return;
        }

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine($"USER DETAILS - ID: {user.Id}");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine($"Name:        {user.Name}");
        Console.WriteLine($"Username:    {user.Username}");
        Console.WriteLine($"Email:       {user.Email}");
        Console.WriteLine($"Phone:       {user.Phone}");
        Console.WriteLine($"Website:     {user.Website}");

        Console.WriteLine("\nAddress:");
        Console.WriteLine($"  Street:    {user.Address.Street}");
        Console.WriteLine($"  Suite:     {user.Address.Suite}");
        Console.WriteLine($"  City:      {user.Address.City}");
        Console.WriteLine($"  Zipcode:   {user.Address.Zipcode}");
        Console.WriteLine($"  Location:  {user.Address.Geo.Latitude}, {user.Address.Geo.Longitude}");

        Console.WriteLine("\nCompany:");
        Console.WriteLine($"  Name:      {user.Company.Name}");
        Console.WriteLine($"  Catchphrase: {user.Company.CatchPhrase}");
        Console.WriteLine($"  Business:  {user.Company.BusinessStrategy}");

        Console.WriteLine(new string('=', 60));
    }

    /// <summary>
    /// Prints a separator line for the table
    /// </summary>
    private static void PrintSeparatorLine(params int[] widths)
    {
        Console.Write("+");
        foreach (var width in widths)
        {
            Console.Write(new string('-', width + 2) + "+");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Prints a table row with proper alignment
    /// </summary>
    private static void PrintTableRow(string id, string name, string username, string email, string city, string company,
                                    int idWidth, int nameWidth, int usernameWidth, int emailWidth, int cityWidth, int companyWidth)
    {
        Console.Write($"| {id.PadRight(idWidth)} |");
        Console.Write($" {name.PadRight(nameWidth)} |");
        Console.Write($" {username.PadRight(usernameWidth)} |");
        Console.Write($" {email.PadRight(emailWidth)} |");
        Console.Write($" {city.PadRight(cityWidth)} |");
        Console.Write($" {company.PadRight(companyWidth)} |");
        Console.WriteLine();
    }

    /// <summary>
    /// Truncates a string to fit within the specified width
    /// </summary>
    /// <param name="text">Text to truncate</param>
    /// <param name="maxWidth">Maximum width</param>
    /// <returns>Truncated string</returns>
    private static string TruncateString(string text, int maxWidth)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        if (text.Length <= maxWidth)
        {
            return text;
        }

        return text.Substring(0, maxWidth - 3) + "...";
    }

    /// <summary>
    /// Displays a simple loading animation
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="duration">Duration in milliseconds</param>
    public static async Task ShowLoadingAnimation(string message, int duration = 2000)
    {
        var spinner = new[] { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
        var endTime = DateTime.Now.AddMilliseconds(duration);

        Console.Write($"{message} ");

        while (DateTime.Now < endTime)
        {
            foreach (var frame in spinner)
            {
                Console.Write($"\r{message} {frame}");
                await Task.Delay(100);

                if (DateTime.Now >= endTime)
                {
                    break;
                }
            }
        }

        Console.WriteLine($"\r{message} ✅");
    }
}
