using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarSharing.Models;

namespace CarSharing.Service
{
    public class CarRepository
    {
        private const string CacheKey = "CarStore";

        private List<Car> GetDefaultCar()
        {
            return new List<Car>
            {
                new Car { Id = 1, Name = "Car1", PositionX = 3, PositionY = 5 },
                new Car { Id = 2, Name = "Car2", PositionX = 5, PositionY = 2 },
                new Car { Id = 3, Name = "Car3", PositionX = 1, PositionY = 1 },
                new Car { Id = 4, Name = "Car4", PositionX = 1, PositionY = 4 },
                new Car { Id = 5, Name = "Car5", PositionX = 2, PositionY = 3 }
            };
        }

        public CarRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var cars = GetDefaultCar();

                    ctx.Cache[CacheKey] = cars;
                }
            }
        }

        public List<Car> GetAllCar()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (List<Car>)ctx.Cache[CacheKey];
            }

            return GetDefaultCar();
        }

        public void UpdateCarRepo(List<Car> cars)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    ctx.Cache[CacheKey] = cars;
                }
            }
        }
    }
}