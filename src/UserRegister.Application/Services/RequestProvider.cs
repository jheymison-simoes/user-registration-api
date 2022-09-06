using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Interfaces.Services;

namespace UserRegister.Application.Services;

public class RequestProvider : IRequestProvider
{
    private StringContent Content;
    private HttpClient _httpClient;
    private string _token;
    private string _baseUrl;
    private JsonSerializerSettings _jsonSerializer;

    public RequestProvider()
    {
        _httpClient = DefineHttpClient();
        _jsonSerializer = ConfigJsonSerializer();
    }
    
    public async Task<TResult> GetAsync<TResult>(string uri, Dictionary<string, string> headers = null)
    {
        if (headers is not null) AddHeaderParameter(_httpClient, headers);
        ValidateToken();
        var response = await _httpClient.GetAsync($"{_baseUrl}{uri}").ConfigureAwait(false);
        return await HandlerResponse<TResult>(response);
    }

    public async Task<TResult> PostAsync<TResult>(string uri, Dictionary<string, string> headers = null)
    {
        if (headers is not null) AddHeaderParameter(_httpClient, headers);
        ValidateToken();
        Content = new StringContent(string.Empty);
        Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}{uri}", Content);
        return await HandlerResponse<TResult>(response);
    }

    public async Task<TResult> PostAsync<T, TResult>(string uri, T data = default(T), Dictionary<string, string> headers = null)
    {
        if (headers is not null) AddHeaderParameter(_httpClient, headers);
        ValidateToken();
        Content = new StringContent(JsonConvert.SerializeObject(data));
        Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}{uri}", Content);
        return await HandlerResponse<TResult>(response);
    }

    public async Task<TResult> DeleteAsync<T, TResult>(string uri, T data = default(T), Dictionary<string, string> headers = null)
    {
        if (headers is not null) AddHeaderParameter(_httpClient, headers);
        ValidateToken();
        Content = new StringContent(JsonConvert.SerializeObject(data));
        var dataSerialized = JsonConvert.SerializeObject(data); 
        var message = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}{uri}")
        {
            Content = new StringContent(dataSerialized, Encoding.UTF8, "application/json")
        };
        var response = await _httpClient.SendAsync(message);
        return await HandlerResponse<TResult>(response);
    }
    
    public async Task<TResult> PutAsync<T, TResult>(string uri, T data, Dictionary<string, string> headers = null)
    {
        if (headers is not null) AddHeaderParameter(_httpClient, headers);
        ValidateToken();
        Content = new StringContent(JsonConvert.SerializeObject(data));
        Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _httpClient.PutAsync($"{_baseUrl}{uri}", Content);
        return await HandlerResponse<TResult>(response); ;
    }

    public async Task DeleteAsync(string uri)
    {
        ValidateToken();
        await _httpClient.DeleteAsync($"{_baseUrl}{uri}");            
    }

    public async Task DeleteAsync(string uri, Dictionary<string, string> headers = null)
    {
        if (headers is not null) AddHeaderParameter(_httpClient, headers);
        ValidateToken();
        await _httpClient.DeleteAsync($"{_baseUrl}{uri}");
    }

    public void SetBaseUrl(string baseUrl)
    {
        _baseUrl = baseUrl;
    }
    
    public void Dispose()
    {            
        ContentDispose();
    }
    
    #region Methods Private
    private JsonSerializerSettings ConfigJsonSerializer()
    {
        var jsonConfig = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };
        jsonConfig.Converters.Add(new StringEnumConverter());
        return jsonConfig;
    }
    
    private async Task<TResult> HandlerResponse<TResult>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw new CustomException(await response.Content.ReadAsStringAsync());
        var serializer = await response.Content.ReadAsStringAsync();
        Dispose();
        return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serializer, _jsonSerializer));
    }
    
    private void ValidateToken()
    {
        if (_httpClient.DefaultRequestHeaders.Authorization is not null && string.IsNullOrEmpty(_token)) return;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }
    
    private void AddHeaderParameter(HttpClient httpClient, Dictionary<string,string> parameter)
    {
        if (httpClient == null) return;
        httpClient.DefaultRequestHeaders.Clear();
        foreach (var item in parameter.Where(item => !string.IsNullOrEmpty(item.Value)))
            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value );
    }
    
    private HttpClient DefineHttpClient()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (!string.IsNullOrEmpty(_token)) httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        return httpClient;
    }
    
    private void ContentDispose()
    {
        Content?.Dispose();
    }
    #endregion
}