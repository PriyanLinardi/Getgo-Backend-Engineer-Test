using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Models.Tests
{
    [TestClass()]
    public class CarGenerateTests
    {
        [TestMethod()]
        public void IsAvailableTest()
        {
            int x = 3, y = 2;
            Car car = new Car{ PositionX = 4, PositionY = 2 };
            CarGenerate carGenerate = new CarGenerate(car);
            Assert.IsTrue(carGenerate.IsAvailable(x,y));
        }
    }
}