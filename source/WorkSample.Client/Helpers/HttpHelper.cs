using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace WorkSample.Client.Helpers;

/// <summary>
///     Provides methods to communicate with webservice.
/// </summary>
public static class HttpHelper
{
    /// <summary>
    ///     Gets content.
    /// </summary>
    /// <typeparam name="T">Type that needs to be returned.</typeparam>
    /// <param name="path">Path to the ressource to get.</param>
    /// <returns>The content returned from webservice.</returns>
    public static async Task<T?> GetAsync<T>(string path)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(path);

        return await response.Content.ReadFromJsonAsync<T>();
    }

    /// <summary>
    ///     Posts content.
    /// </summary>
    /// <typeparam name="T">Type that needs to be posted.</typeparam>
    /// <param name="path">Path to post to.</param>
    /// <param name="content">Content to post.</param>
    public static async Task PostAsync<T>(string path, T content)
    {
        using var client = new HttpClient();
        using var jsonContent =
            new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");
        var response = await client.PostAsync(path, jsonContent);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    ///     Puts content.
    /// </summary>
    /// <typeparam name="T">Type that needs to be put.</typeparam>
    /// <param name="path">Path to put to.</param>
    /// <param name="content">Content to put.</param>
    public static async Task PutAsync<T>(string path, T content)
    {
        using var client = new HttpClient();
        using var jsonContent =
            new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");
        var response = await client.PutAsync(path, jsonContent);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    ///     Deletes content.
    /// </summary>
    /// <param name="path">Path to delete from.</param>
    public static async Task DeleteAsync(string path)
    {
        using var client = new HttpClient();
        var response = await client.DeleteAsync(path);
        response.EnsureSuccessStatusCode();
    }
}
