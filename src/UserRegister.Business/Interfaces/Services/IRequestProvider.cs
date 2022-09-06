namespace UserRegister.Business.Interfaces.Services;

public interface IRequestProvider
{
    Task<TResult> GetAsync<TResult>(string uri, Dictionary<string, string> headers = null);
    Task<TResult> PostAsync<TResult>(string uri, Dictionary<string, string> headers = null);
    Task<TResult> PostAsync<T, TResult>(string uri, T data = default(T), Dictionary<string, string> headers = null);
    Task<TResult> DeleteAsync<T, TResult>(string uri, T data = default(T), Dictionary<string, string> headers = null);
    Task<TResult> PutAsync<T, TResult>(string uri, T data, Dictionary<string, string> headers = null);
    Task DeleteAsync(string uri);
    Task DeleteAsync(string uri, Dictionary<string, string> headers = null);
    void SetBaseUrl(string baseUrl);
}