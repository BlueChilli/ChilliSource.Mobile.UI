#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using Xunit;
using Refit;

namespace Api
{

	public interface ITestApi
	{
		[Get("/testmessage")]
		Task<string> GetTestMessage();
        [Get("/sessionexpired")]
        Task<string> TestSessionExpired();
        [Get("/networktest1")]
        Task TestNoNetwork();
        [Get("/testworktest2")]
		Task TestNoNetwork2();
    }

}
