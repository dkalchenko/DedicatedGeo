using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;

namespace DedicatedGeo.Mono.Core.Device.Handlers;

public class GetDeviceByIMEIInternalRequestHandler: IRequestHandler<GetDeviceByIMEIInternalRequest, GetDeviceInternalResponse>
{
    private readonly IDatabaseRepository _repository;

    public GetDeviceByIMEIInternalRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }

    
    public async Task<GetDeviceInternalResponse> Handle(GetDeviceByIMEIInternalRequest request, CancellationToken cancellationToken)
    {
        var device = _repository.Devices
            .FirstOrDefault(x => x.IMEI == request.DeviceId);

        if (device is null)
        {
            device = new Models.Device.Device
            {
                DeviceId = Guid.NewGuid(),
                IMEI = request.DeviceId,
                Name = GenerateTwoRandomWords(),
            };
            _repository.Devices.Add(device);
            await _repository.SaveChangesAsync(cancellationToken);
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