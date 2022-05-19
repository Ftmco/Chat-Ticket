namespace ChTi.Client.Abstraction;

public interface IGrpcRule : IAsyncDisposable
{
    Task<GrpcChannel> OpenChannelAsync();

    Task<GrpcChannel> OpenChannelAsync(string channelAddress);
}
