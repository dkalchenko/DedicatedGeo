using System.Net;
using DedicatedGeo.Mono.Common.Exceptions;

namespace DedicatedGeo.Mono.Common;

public static class OwnConstants
{

    public const int UpdateDeviceStatusBackgroundDelayInMillisecond = 2000;
    public const int DeviceIsInactivityStatusAfterInSecond = 2;
    
    public static class DeviceStatusNames
    {
        public const string BatteryLevel = "BatteryLevel";
        public const string IsInAlarm = "IsInAlarm";
        public const string IsButtonPressed = "IsButtonPressed";
        public const string IsInCharge = "IsInCharge";
        public const string IsGPSOnline = "IsGPSOnline";
        public const string IsDeviceOnline = "IsDeviceOnline";
        
        public static readonly IEnumerable<string> AllNames =
        [
            BatteryLevel,
            IsInAlarm,
            IsButtonPressed,
            IsInCharge,
            IsGPSOnline,
            IsDeviceOnline
        ];
    }
    
    public static class EnvironmentKeys
    {
        public const string MySqlConnectionString = "IDEALTEX_MYSQL_CONNECTION_STRING";
        public const string AspnetcoreEnvironment = "ASPNETCORE_ENVIRONMENT";
        public const string LogFolder = "IDEALTEX_LOG_FOLDER";
    }
    
    public static class ErrorTemplates
    {
        public const string RequestExceptionMessage = "Request was ended with Code: {Code} and Message: {Message}";

        public static ExceptionMaker ResourceNotFound => new(
            "The requested {Resource} was not found.",
            HttpStatusCode.NotFound
        );
    }
    
}