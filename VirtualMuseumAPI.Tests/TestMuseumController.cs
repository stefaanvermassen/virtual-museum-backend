using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
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
                var postMuseum = getMuseumController().Post(testMuseums[0]) as OkNegotiatedContentResult<MuseumModel>;
                var getMuseum = getMuseumController().Get(postMuseum.Content.MuseumID) as OkNegotiatedContentResult<MuseumModel>;
                Assert.IsNotNull(postMuseum);
                Assert.IsNotNull(getMuseum);
                Assert.AreEqual(getMuseum.Content.Description, postMuseum.Content.Description);
                Assert.AreEqual(getMuseum.Content.Privacy, postMuseum.Content.Privacy);

                // Not commiting transaction to leave DB in clean state
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
    }
}

