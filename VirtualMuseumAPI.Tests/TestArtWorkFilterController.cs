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
using VirtualMuseumAPI.Models;

namespace VirtualMuseumAPI.Tests
{
    [TestClass]
    public class TestArtWorkFilterController
    {
        [TestMethod]
        public void PostArtWorkFilter_ShouldReturnCorrectFilter()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                var testFilters = GetTestFilters();
                var filterController = getArtWorkFilterController();
                //New filter in DB
                filterController.Post(testFilters[0]);
                //Should now be connected to this user
                var connectedFilters = filterController.Get();
                Assert.IsTrue(connectedFilters.ArtWorkFilters.Any(f => f.Name == testFilters[0].Name && f.Value == testFilters[0].Value));

                //make a new where the key does already exist
                var postFilter = filterController.Post(testFilters[1]) as OkNegotiatedContentResult<KeyValuePair>;
                Assert.AreEqual(testFilters[1].Name, postFilter.Content.Name);
                Assert.AreEqual(testFilters[1].Value, postFilter.Content.Value);

                //make a new where key and value already exist
                postFilter = filterController.Post(testFilters[1]) as OkNegotiatedContentResult<KeyValuePair>; 
                Assert.AreEqual(testFilters[1].Name, postFilter.Content.Name);
                Assert.AreEqual(testFilters[1].Value, postFilter.Content.Value);
            }
        }

        [TestMethod]
        public void SearchArtWorkFilter_ShouldReturnCorrectFilter()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                var testFilters = GetTestFilters();
                var filterController = getArtWorkFilterController();
                //New filter in DB
                filterController.Post(testFilters[0]);
                //Should find this filter
                var searchedFilters = filterController.Get(testFilters[0]);
                Assert.IsTrue(searchedFilters.ArtWorkFilters.Any(f => f.Name == testFilters[0].Name && f.Value == testFilters[0].Value));

                //Search with name only
                searchedFilters = filterController.Get(new KeyValuePair() { Name="testkey1"});
                Assert.IsTrue(searchedFilters.ArtWorkFilters.Any(f => f.Name == testFilters[0].Name && f.Value == testFilters[0].Value));

                //Search with value only
                searchedFilters = filterController.Get(new KeyValuePair() { Value = "testvalue1" });
                Assert.IsTrue(searchedFilters.ArtWorkFilters.Any(f => f.Name == testFilters[0].Name && f.Value == testFilters[0].Value));
            }
        }

        [TestMethod]
        public void DeleteArtWorkFilter_ShouldNotContainFilter()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                var testFilters = GetTestFilters();
                var filterController = getArtWorkFilterController();
                //New filter in DB
                var postFilter = filterController.Post(testFilters[0]) as OkNegotiatedContentResult<KeyValuePair>;
                filterController.Delete(postFilter.Content.Id);
                //Should now be connected to this user
                var connectedFilters = filterController.Get();
                Assert.IsFalse(connectedFilters.ArtWorkFilters.Any(f => f.Name == testFilters[0].Name && f.Value == testFilters[0].Value));
            }
        }

        private List<KeyValuePair> GetTestFilters()
        {
            var testfilters = new List<KeyValuePair>();
            testfilters.Add(new KeyValuePair() { Name = "testkey1", Value = "testvalue1" });
            testfilters.Add(new KeyValuePair() { Name = "testkey1", Value = "testvalue2" });
            return testfilters;
        }

        private ArtWorkFilterController getArtWorkFilterController()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize<HttpRequestMessage>(c => c
            .Without(x => x.Content)
            .Do(x => x.Properties[HttpPropertyKeys.HttpConfigurationKey] =
                new HttpConfiguration()));
            fixture.Customize<ArtWorkFilterController>(c => c
                .OmitAutoProperties()
                .With(x => x.Request, fixture.Create<HttpRequestMessage>()));
            //Simulate this user to execute controller methods
            var claim = new Claim("VirtualMuseum", "d32895c4-8d71-4340-884c-89293cc50784");
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);
            fixture.Customize<ArtWorkFilterController>(c => c
                .OmitAutoProperties()
                .With(x => x.User, Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity)));
            return fixture.CreateAnonymous<ArtWorkFilterController>();
        }
    }
}
