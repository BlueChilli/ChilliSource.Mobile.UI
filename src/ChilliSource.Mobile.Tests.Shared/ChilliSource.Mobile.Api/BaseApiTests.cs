#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Net.Http;
using System.Threading.Tasks;
using ChilliSource.Mobile.Api;

namespace Api
{
	public class BaseApiTests
	{

		protected HttpMessageHandler GetHandler(ApiToken token, HttpMessageHandler innerHandler)
		{
			return new ApiAuthenticatedHandler(() =>
			   {
				   return Task.FromResult(token);
			   }, innerHandler);
		}

		protected ApiConfiguration GetConfig(string baseUrl,
												  Func<HttpMessageHandler> handlerFactory,
												  bool connected = true
												  )
		{
			return new ApiConfiguration(baseUrl, () =>
			{
				return new NoNetworkHandler(new FakeConnectivity(connected), handlerFactory());

			});
		}

		protected IApi<T> GetApiManager<T>(ApiConfiguration config)
		{
			return new ApiManager<T>(config);
		}
	}
}
