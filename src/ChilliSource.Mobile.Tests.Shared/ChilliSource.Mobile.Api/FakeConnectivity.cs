#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;

namespace Api
{
	public class FakeConnectivity : IConnectivity
	{
		readonly bool mockConnected;

		public FakeConnectivity(bool mockConnected)
		{
			this.mockConnected = mockConnected;
		}

		public IEnumerable<ulong> Bandwidths
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerable<ConnectionType> ConnectionTypes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsConnected
		{
			get
			{
				return mockConnected;
			}
		}

		public event ConnectivityChangedEventHandler ConnectivityChanged;

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsReachable(string host, int msTimeout = 5000)
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsRemoteReachable(string host, int port = 80, int msTimeout = 5000)
		{
			throw new NotImplementedException();
		}
	}
}
