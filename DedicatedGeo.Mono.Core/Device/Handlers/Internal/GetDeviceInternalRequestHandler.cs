using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;

namespace DedicatedGeo.Mono.Core.Device.Handlers;

public class GetDeviceInternalRequestHandler: IRequestHandler<GetDeviceInternalRequest, GetDeviceInternalResponse>
{
    private readonly IDatabaseRepository _repository;

    public GetDeviceInternalRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }

    
    public async Task<GetDeviceInternalResponse> Handle(GetDeviceInternalRequest request, CancellationToken cancellationToken)
    {
        var device = _repository.Devices
            .FirstOrDefault(x => x.DeviceId == request.DeviceId.ToGuid());

        if (device is null)
        {
            return null;
        }

        return new GetDeviceInternalResponse
        {
            DeviceId = device.DeviceId,
        };
    }
    
    private static readonly Random random = new Random();
    private static readonly string[] words = 
    {
        "apple", "banana", "cloud", "dragon", "echo", "forest", "galaxy", "horizon",
        "island", "jungle", "kinetic", "lunar", "mountain", "nebula", "ocean", "pixel",
        "quantum", "river", "shadow", "tiger", "umbrella", "violet", "whisper", "xenon", "yellow", "zebra"
    };

    public static string GenerateTwoRandomWords()
    {
        string word1 = words[random.Next(words.Length)];
        string word2 = words[random.Next(words.Length)];
        return $"{word1} {word2}";
    }
}