using System.Text.Json;
using PublicApiTableViewer.Models;

namespace PublicApiTableViewer.Services;

/// <summary>
/// Service for consuming the JSONPlaceholder API
/// </summary>
public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private const string BaseUrl = "https://jsonplaceholder.typicode.com";

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Fetches all users from the API
    /// </summary>
    /// <returns>A list of users or empty list if failed</returns>
    public async Task<List<User>> GetUsersAsync()
    {
        try
        {
            Console.WriteLine("🔄 Fetching users from API...");

            var response = await _httpClient.GetAsync("/users");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<User>>(jsonContent, _jsonOptions);

                Console.WriteLine($"✅ Successfully fetched {users?.Count ?? 0} users");
                return users ?? new List<User>();
            }
            else
            {
                Console.WriteLine($"❌ API request failed with status: {response.StatusCode}");
                return new List<User>();
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"❌ Network error: {ex.Message}");
            return new List<User>();
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"❌ Request timeout: {ex.Message}");
            return new List<User>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"❌ JSON parsing error: {ex.Message}");
            return new List<User>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Unexpected error: {ex.Message}");
            return new List<User>();
        }
    }

    /// <summary>
    /// Fetches a specific user by ID
    /// </summary>
    /// <param name="userId">The user ID to fetch</param>
    /// <returns>A user object or null if not found</returns>
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        try
        {
            Console.WriteLine($"🔄 Fetching user {userId} from API...");

            var response = await _httpClient.GetAsync($"/users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(jsonContent, _jsonOptions);

                Console.WriteLine($"✅ Successfully fetched user: {user?.Name}");
                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"❌ User {userId} not found");
                return null;
            }
            else
            {
                Console.WriteLine($"❌ API request failed with status: {response.StatusCode}");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"❌ Network error: {ex.Message}");
            return null;
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"❌ Request timeout: {ex.Message}");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"❌ JSON parsing error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Unexpected error: {ex.Message}");
            return null;
        }
    }
}
