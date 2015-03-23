﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using VirtualMuseumAPI.Controllers;
using VirtualMuseumAPI.Helpers;
using VirtualMuseumAPI.Models;


namespace VirtualMuseumAPI.Tests
{
    [TestClass]
    public class TestMuseumController
    {


        [TestMethod]
        public void PostMuseum_ShouldReturnCorrectMuseum()
        {    
            using (TransactionScope transaction = new TransactionScope())
            {
                var testMuseums = GetTestMuseums();
                var museumController = getMuseumController();
                var postMuseum = museumController.Post(testMuseums[0]) as OkNegotiatedContentResult<MuseumModel>;
                var getMuseum = museumController.Get(postMuseum.Content.MuseumID) as OkNegotiatedContentResult<MuseumModel>;
                Assert.IsNotNull(postMuseum);
                Assert.IsNotNull(getMuseum);
                Assert.AreEqual(getMuseum.Content.Description, postMuseum.Content.Description);
                Assert.AreEqual(getMuseum.Content.Privacy, postMuseum.Content.Privacy);

                // Not commiting transaction to leave DB in clean state
            }
        }

        [TestMethod]
        public void GetRandomMuseum_ShouldNotFindMuseum()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                var museumController = getMuseumController();
                //Initially, no museums in the database, so it should not find a random
                var testMuseums = GetTestMuseums();
                var getRandomMuseum = museumController.GetRandomMuseum();
                Assert.IsInstanceOfType(getRandomMuseum, typeof(NotFoundResult));

                //Post a private museum, after this, it should not find a random 
                museumController.Post(testMuseums[0]);
                getRandomMuseum = museumController.GetRandomMuseum();
                Assert.IsInstanceOfType(getRandomMuseum, typeof(NotFoundResult));

                //Post a public museum, but with no data. After this, it should not find a random 
                museumController.Post(testMuseums[1]);
                getRandomMuseum = museumController.GetRandomMuseum();
                Assert.IsInstanceOfType(getRandomMuseum, typeof(NotFoundResult));

                // Not commiting transaction to leave DB in clean state
            }
        }

        [TestMethod]
        public async Task GetRandomMuseum_ShouldFindMuseum()
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var museumData = new Byte[100];
                var museumDataController = getMuseumController(museumData);
                var testMuseums = GetTestMuseums();
                //Create a new museum
                var postMuseum = museumDataController.Post(testMuseums[1]) as OkNegotiatedContentResult<MuseumModel>;
                //Assign museumData to it
                var postMuseumData = await museumDataController.PostAsync(postMuseum.Content.MuseumID);

                var getRandomMuseum = museumDataController.GetRandomMuseum() as OkNegotiatedContentResult<MuseumModel>;
                Assert.AreEqual(getRandomMuseum.Content.MuseumID, postMuseum.Content.MuseumID);

                // Not commiting transaction to leave DB in clean state
            }
        }

        [TestMethod]
        public async Task PostMuseumData_ShouldReturnCorrectMuseum()
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Data to send
                var museumData = new Byte[100];
                //Get controller with MultipartFormDataContent
                var museumController = getMuseumController(museumData);
                //Get testmuseums array
                var testMuseums = GetTestMuseums();
                //Create a new museum
                var postMuseum = museumController.Post(testMuseums[0]) as OkNegotiatedContentResult<MuseumModel>;
                //Assign museumData to it
                var postMuseumData = await museumController.PostAsync(postMuseum.Content.MuseumID);
                //Get the new museum
                var getMuseum = museumController.GetMuseumData(postMuseum.Content.MuseumID) as VirtualMuseumDataResult;

                Assert.IsNotNull(postMuseumData);
                Assert.IsNotNull(getMuseum);
                //Make an byte array from the received stream to compare
                byte[] getMuseumByteArray;
                using (BinaryReader br = new BinaryReader(getMuseum.getStream()))
                {
                    getMuseumByteArray = br.ReadBytes(museumData.Length);
                }
                CollectionAssert.AreEqual(getMuseumByteArray, museumData);
                
            }
            
        }

        private List<MuseumModel> GetTestMuseums()
        {
            var testMuseums = new List<MuseumModel>();
            testMuseums.Add(new MuseumModel { Description = "DemoMuseumPrivate", Privacy = Privacy.Levels.PRIVATE });
            testMuseums.Add(new MuseumModel { Description = "DemoMuseumPublic", Privacy = Privacy.Levels.PUBLIC });
            return testMuseums;
        }

        private MuseumController getMuseumController()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize<HttpRequestMessage>(c => c
            .Without(x => x.Content)
            .Do(x => x.Properties[HttpPropertyKeys.HttpConfigurationKey] =
                new HttpConfiguration())); 
            fixture.Customize<MuseumController>(c => c
                .OmitAutoProperties()
                .With(x => x.Request, fixture.Create<HttpRequestMessage>()));
            //Simulate this user to execute controller methods
            var claim = new Claim("VirtualMuseum", "0fdf6184-964e-40e3-a828-076853cbd5d0");
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);
            fixture.Customize<MuseumController>(c => c
                .OmitAutoProperties()
                .With(x => x.User, Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity)));
            return fixture.CreateAnonymous<MuseumController>();
        }

        private MuseumController getMuseumController(byte[] buffer)
        {
            var museumController = getMuseumController();
            museumController.ControllerContext = FakeControllerContextWithMultiPartContentFactory.Create(buffer);
            var claim = new Claim("VirtualMuseum", "0fdf6184-964e-40e3-a828-076853cbd5d0");
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);
            museumController.User = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);
            return museumController;
        }

        public class FakeControllerContextWithMultiPartContentFactory
        {
            public static HttpControllerContext Create(byte[] buffer)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "");
                var content = new MultipartFormDataContent();

                var fileContent = new ByteArrayContent(buffer);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Foo.txt"
                };
                content.Add(fileContent);
                request.Content = content;

                return new HttpControllerContext(new HttpConfiguration(), new HttpRouteData(new HttpRoute("")), request);
            }

        }
    }
}
