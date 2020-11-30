using Mirroculous.Controllers;
using Mirroculous.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace XUnitTest_Mirroculous
{
    public class UnitTest1
    {
        [Fact]
        public void AssertNotNullResult()
        {
            //................Act............

            var expected = FakeMirrorList();
            // fake data from  own unit test class
            var controller = new MirrorController();

            // .............Arrange........and  define a type which you get the object
            var result = controller.Get() as List<Mirror>; //we need object value
            var actual = result;

            //............Asset..............
            Assert.NotNull(result);
            Debug.Assert(expected != null, nameof(expected) + " != null");
            // Assert.Equal(expected, actual);
        }
        

        [Fact]
        public void GetMirror_ShouldReturnCorrectMirror()
        {
            var testProducts = FakeMirrorList();

            // create my list
            var controller = new MirrorController();

            var result = controller.Get(4);
            
            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("21-11-20 00:00"), testProducts[0].DateTime);
        }

        [Fact]
        public void GetAllMirror_ShouldReturnAllMirror()
        {
            var testProducts = FakeMirrorList();
            var controller = new MirrorController();
            int output=controller.GetMirrorFromDB2().Count;

            var result = controller.Get()as List<Mirror>;
            Assert.Equal(4, output);
        }


       

        // for unit Testing we must do define a fake data for testing purpose
        public List<Mirror> FakeMirrorList()
        {
            List<Mirror> mirror = new List<Mirror>
            {
                 new Mirror(1, 25, 10, DateTime.Parse("21-11-20 00:00")),
                 new Mirror(2, 35, 30, DateTime.Parse("23-11-20 00:00")),
                 new Mirror(3, 41, 40, DateTime.Parse("22-11-20 00:00")),
                 new Mirror(4, 22, 222, DateTime.Parse("22-11-20 00:00"))
            };

            return mirror;
        }


    }
}
