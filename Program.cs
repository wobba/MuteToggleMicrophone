using System;
using NAudio.CoreAudioApi;

namespace MuteToggleMicrophone
{
    /// <summary>
    /// "all" toggles all microphones
    /// "name" toggles microphone containing that name
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string match = string.Empty;
            if (args.Length > 0)
            {
                match = args[0].ToLower();
            }

            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();

            MMDeviceCollection devices = DevEnum.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            foreach (MMDevice deviceAt in devices)
            {
                if (string.IsNullOrWhiteSpace(match))
                {
                    Console.WriteLine(deviceAt.FriendlyName);
                    continue;
                }
                string deviceName = deviceAt.FriendlyName.ToLower();
                if (!deviceName.Contains(match) && !deviceName.Equals("all")) continue;

                deviceAt.AudioEndpointVolume.Mute = !deviceAt.AudioEndpointVolume.Mute;
            }
        }
    }
}
