using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoftwareUpdater.ApiSoftware;

internal class HttpEngine
{
    private static HttpClient httpClient;
    private static readonly TimeSpan httpTimeout = new TimeSpan(0, 0, 30);
    protected internal static HttpClient HttpClient
    {
        get
        {
            if (HttpEngine.httpClient == null)
            {
                var httpClientHandler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                HttpEngine.httpClient = new HttpClient(httpClientHandler)
                {
                    Timeout = HttpEngine.httpTimeout
                };

                HttpEngine.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return HttpEngine.httpClient;
        }
        set => HttpEngine.httpClient = value;
    }

    public static async Task<NewestReleasesResponse> QueryAsync(NewestReleasesRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var message = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        var response = await ProcessResponseAsync(message).ConfigureAwait(false);

        return response;
    }
    private static async Task<HttpResponseMessage> ProcessRequestAsync(NewestReleasesRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        string serializeObject = JsonSerializer.Serialize<NewestReleasesRequest>(request);

        using (var stringContent = new StringContent(serializeObject, System.Text.Encoding.UTF8, "application/json"))
        {
            return await HttpEngine.HttpClient.PostAsync(request.RequestUrl, stringContent, cancellationToken).ConfigureAwait(false);
        }
    }
    private static async Task<NewestReleasesResponse> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        if (httpResponse == null)
            throw new ArgumentNullException(nameof(httpResponse));

        using (httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            var rawJson = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            JsonSerializerOptions jOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            NewestReleasesResponse response = JsonSerializer.Deserialize<NewestReleasesResponse>(rawJson, jOptions);

            if (response == null)
                throw new NullReferenceException(nameof(response));

            response.RawJson = rawJson;

            response.Status = httpResponse.IsSuccessStatusCode
                ? response.Status ?? "Ok"
                : "HttpError";

            return response;
        }
    }
}
