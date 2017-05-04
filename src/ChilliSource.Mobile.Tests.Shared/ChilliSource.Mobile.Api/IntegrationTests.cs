#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Xunit;
using ChilliSource.Mobile.Api;
using Refit;
using ModernHttpClient;
using ChilliSource.Mobile.Core;

namespace Api
{
    public class Attachment
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("contentPath")]
        public string contentPath { get; set; }

        [JsonProperty("attachmentType")]
        public string attachmentType { get; set; }
    }

    public class Hazard
    {

        
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("altitude")]
        public double altitude { get; set; }

        [JsonProperty("siteId")]
        public int siteId { get; set; }

        [JsonProperty("hazardTypeId")]
        public int hazardTypeId { get; set; }

        [JsonProperty("notes")]
        public string notes { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> attachments { get; set; }

        [JsonProperty("imageFile")]
        public FileInfo ImageFile { get; set; }
    }

    public class IdItem
    {
        public int Id { get; set; }
    }
    public interface IHazardApi
    {
        [Multipart]
        [Headers("Accept: */*")]
        [Post("/hazards")]
        Task<Hazard> Create(MultipartData<Hazard> hazard);

        [Get("/account/status")]
        Task<HttpResponseMessage> GetAccoutStatus();

        [Post("/hazards/ack")]
        Task<HttpResponseMessage> Update(MultipartData<IdItem> item);
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class BatchUser
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PagedList<T>
    {
        public PagedList()
        {
            Data = new List<T>();
        }

        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public List<T> Data { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public interface IBatchApi : IAsyncBatchable, IObservableBatchable
    {

        [Post("/v1/users/login")]
        Task<BatchUser> Login([Body()] Login login);

        [Get("/v1/company/current")]
        Task<Company> GetCompany();

        [Get("/v1/users/profile")]
        Task<BatchUser> GetProfile();
    }
   


    public class IntegrationTests
    {
        [Fact(Skip ="Run it in locally")]
        //[Fact()]
        public async Task ShouldSuccessfullyCallHazardApi()
        {
            const string apiKey = "CF5252EB-4537-4460-A1F6-6D9BF0DBDBFA";
            const string userKey = "0872d545-0639-4847-9dda-bc0e31bd871a";

            var url = "https://dev.bluechilli.com/safetycompass/api";
            var token = new ApiToken(apiKey, EnvironmentInformation.Empty, userKey)
            {

            };

            var manager = new ApiManager<IHazardApi>(new ApiConfiguration(url, () =>
            {
                return new ApiAuthenticatedHandler(() =>
               {
                   return Task.FromResult(token);
               }, new NativeMessageHandler() { CookieContainer = new CookieContainer() });
            }, ApiConfiguration.DefaultJsonSerializationSettingsFactory));
                      
            var hazard = new Hazard()
            {
                siteId = 8,
                name = "BlueChilli7",
                latitude = -33.8716715,
                longitude = 151.20616129999996,
                altitude = 24,
                hazardTypeId = 1,
                notes = "Lorem ipsum dolor sit amet, ",
                attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        name = "Attach1",
                        attachmentType = "VideoLink",
                        contentPath = "https://www.youtube.com/2945kdf49fk"
                       
                    }
                },
                ImageFile = new FileInfo(@"C:\Temp\messenger-hover.png")
            };

            var r = await manager.Client.Create(MultipartData<Hazard>.Create(hazard))
                             .WaitForResponse(false);
            
            Assert.NotNull(r);

        }

        [Fact(Skip = "Run it in locally")]
        //[Fact()]

        public async Task MultipartBatchAsyncRequestShouldSucceed()
        {
            const string apiKey = "F5311DE2-6F54-443E-8FC6-863AE944CE4A";
            const string userKey = "c7334a86-e4ba-454b-b7fa-c0a337a0a21d";

            var url = "https://benchon-dev.azurewebsites.net/api";

            var token = new ApiToken(apiKey, EnvironmentInformation.Empty, userKey)
            {

            };

            var manager = new ApiManager<IBatchApi>(new ApiConfiguration(url, () =>
            {
                return new ApiAuthenticatedHandler(() =>
               {
                   return Task.FromResult(token);
               }, new NativeMessageHandler() { CookieContainer = new CookieContainer() });
            }, ApiConfiguration.DefaultJsonSerializationSettingsFactory));
            var builder = BatchRequestBuilder.For<IBatchApi>();
            var req = builder
                .AddRequest(nameof(IBatchApi.Login), new Login()
                {
                    Email = "max@bluechilli.com",
                    Password = "123456"
                })
                .AddRequest(nameof(IBatchApi.GetProfile))
                .AddRequest(nameof(IBatchApi.GetCompany))
                .Build("/$batch/sequential");

            var r = await manager.Client.BatchAsync(req);
            Assert.NotNull(r);
            var c = r.GetResults<Company>(nameof(IBatchApi.GetCompany)).FirstOrDefault();
            Assert.NotNull(c);
            Assert.Equal("Max Company", c.Value.Name);

        }

        [Fact(Skip = "Run it in locally")]
        //[Fact()]

        public async Task MultipartBatchObservableRequestShouldSucceed()
        {
            const string apiKey = "F5311DE2-6F54-443E-8FC6-863AE944CE4A";
            const string userKey = "c7334a86-e4ba-454b-b7fa-c0a337a0a21d";

            var url = "https://benchon-dev.azurewebsites.net/api";
            var token = new ApiToken(apiKey, EnvironmentInformation.Empty, userKey)
            {

            };

            var login = new Login()
            {
                Email = "max@bluechilli.com",
                Password = "123456"
            };
            var manager = new ApiManager<IBatchApi>(new ApiConfiguration(url, () =>
            {
                return new ApiAuthenticatedHandler(() =>
               {
                   return Task.FromResult(token);
               }, new NativeMessageHandler() { CookieContainer = new CookieContainer() });
            }, ApiConfiguration.DefaultJsonSerializationSettingsFactory));  var builder = BatchRequestBuilder.For<IBatchApi>();
            var req = builder
                // Can not do this in iOS mono does not implement expression tree with => CompileMemberInitExpression
                /* .AddRequest(api => api.Login(new Login()
                {
                    Email = "max@bluechilli.com",
                    Password = "123456"
                }))*/
                    .AddRequest(api => api.Login(login))
                    .AddRequest(api => api.GetProfile())
                    .AddRequest(api => api.GetCompany())
                    .Build("/$batch/sequential");

         
            var r = await manager.Client.Batch(req);
            Assert.NotNull(r);
            var c = r.GetResults<Company>(nameof(IBatchApi.GetCompany)).FirstOrDefault();
            Assert.NotNull(c);
            Assert.Equal("Max Company", c.Value.Name);

        }
    }
}
