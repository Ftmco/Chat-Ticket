using ChTi.Client.Abstraction;

namespace ChTi.Client.Implemention;

public class GrpcService : IGrpcRule
{
    readonly IConfiguration _configuration;

    public GrpcService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<GrpcChannel> OpenChannelAsync()
    {
        var channelAddress = _configuration["ChTi:Address:gRPC"];
        return await OpenChannelAsync(channelAddress);
    }

    public Task<GrpcChannel> OpenChannelAsync(string channelAddress)
    {
        GrpcChannel? channel = GrpcChannel.ForAddress(channelAddress);
        return Task.FromResult(channel);
    }
}
