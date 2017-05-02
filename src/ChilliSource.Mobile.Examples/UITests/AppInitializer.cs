#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Examples.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.StartApp();
			}

#if __TEST__
            var deviceId = "7279EAB6-1ED4-477B-9647-09BB2CB9ED26"
            return ConfigureApp.iOS.DeviceIdentifier(deviceId).StartApp();
#else
			return ConfigureApp.iOS.StartApp();
#endif
		}

        public static void ResetSimulator(string deviceId, Platform platform) {
            if (platform == Platform.Android){
                
            }
            else 
            {
                ResetSimulator(deviceId, platform);
            }
        }

        private static void ResetSimulator(string deviceId)
		{
			var shutdownProcess = Process.Start("xcrun", string.Format("simctl shutdown {0}", deviceId));
			shutdownProcess.WaitForExit();
			var eraseProcess = Process.Start("xcrun", string.Format("simctl erase {0}", deviceId));
			eraseProcess.WaitForExit();
		}
	}
}
