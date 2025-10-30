using System.Text.Json.Serialization;

namespace PublicApiTableViewer.Models;

/// <summary>
/// Represents a user from the JSONPlaceholder API
/// </summary>
public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("website")]
    public string Website { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public Address Address { get; set; } = new();

    [JsonPropertyName("company")]
    public Company Company { get; set; } = new();
}

/// <summary>
/// Represents a user's address information
/// </summary>
public class Address
{
    [JsonPropertyName("street")]
    public string Street { get; set; } = string.Empty;

    [JsonPropertyName("suite")]
    public string Suite { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; set; } = string.Empty;

    [JsonPropertyName("geo")]
    public GeoLocation Geo { get; set; } = new();
}

/// <summary>
/// Represents geographical coordinates
/// </summary>
public class GeoLocation
{
    [JsonPropertyName("lat")]
    public string Latitude { get; set; } = string.Empty;

    [JsonPropertyName("lng")]
    public string Longitude { get; set; } = string.Empty;
}

/// <summary>
/// Represents company information
/// </summary>
public class Company
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("catchPhrase")]
    public string CatchPhrase { get; set; } = string.Empty;

    [JsonPropertyName("bs")]
    public string BusinessStrategy { get; set; } = string.Empty;
}
