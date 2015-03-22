using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using VirtualMuseumAPI.Controllers;
using VirtualMuseumAPI.Helpers;
using VirtualMuseumAPI.Models;


namespace VirtualMuseumAPI.Tests
{
    [TestClass]
    class TestMuseumController
    {


        [TestMethod]
        public void PostMuseum_ShouldReturnCorrectMuseum()
        {
            var testMuseums = GetTestMuseums();
            var controller = new MuseumController();
            var response = controller.Post(testMuseums[0]);
            ObjectContent objContent = response.Content as ObjectContent;
            MuseumModel museum = objContent.Value as MuseumModel;
            Assert.IsNotNull(museum);
            Assert.AreEqual(museum.Description, testMuseums[0].Description);
            Assert.AreEqual(museum.Privacy, testMuseums[0].Privacy);
        }

        private List<MuseumModel> GetTestMuseums()
        {
            var testMuseums = new List<MuseumModel>();
            testMuseums.Add(new MuseumModel { Description = "DemoMuseumPrivate", Privacy = Privacy.Levels.PRIVATE });
            testMuseums.Add(new MuseumModel { Description = "DemoMuseumPublic", Privacy = Privacy.Levels.PUBLIC });
            return testMuseums;
        }
    }
}

