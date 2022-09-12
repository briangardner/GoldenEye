using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace GoldenEye.Commands;

public interface IExternalCommandBus
{
    Task Post<T>(string url, string path, T command, CancellationToken cancellationToken = default) where T: class, ICommand;
    Task Put<T>(string url, string path, T command, CancellationToken cancellationToken = default) where T: class, ICommand;
    Task Delete<T>(string url, string path, T command, CancellationToken cancellationToken = default) where T: class, ICommand;
}

public class ExternalCommandBus: IExternalCommandBus
{
    public Task Post<T>(string url, string path, T command, CancellationToken cancellationToken = default) where T : class, ICommand
    {
        var client = new RestClient(url);

        var request = new RestRequest(path, Method.Post){RequestFormat = DataFormat.Json};
        request.AddJsonBody(command);

        return client.PostAsync<dynamic>(request, cancellationToken);
    }

    public Task Put<T>(string url, string path, T command, CancellationToken cancellationToken = default) where T : class, ICommand
    {
        var client = new RestClient(url);

        var request = new RestRequest(path, Method.Put){RequestFormat = DataFormat.Json};
        request.AddJsonBody(command);

        return client.PutAsync<dynamic>(request, cancellationToken);
    }

    public Task Delete<T>(string url, string path, T command, CancellationToken cancellationToken = default) where T : class, ICommand
    {
        var client = new RestClient(url);

        var request = new RestRequest(path, Method.Delete){RequestFormat = DataFormat.Json};
        request.AddJsonBody(command);

        return client.DeleteAsync<dynamic>(request, cancellationToken);
    }
}
