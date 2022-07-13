using CarSharing.Models;
using System;
using System.Collections.Generic;
using CarSharing.Interface;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarSharing.Service;

namespace CarSharing.Controllers
{
    public class CarController : ApiController
    {
        private CarRepository carRepository;

        public CarController()
        {
            this.carRepository = new CarRepository();
        }

        // GET: api/Car
        public List<Car> Get()
        {
            return carRepository.GetAllCar();
        }

        [Route("api/Car/SearchCarById/{Id:int}")]
        [HttpGet]
        public Car SearchCarById(int Id)
        {
            return carRepository.GetAllCar().Where(a => a.Id == Id).FirstOrDefault();
        }

        [Route("api/Car/SearchCar/{name}")]
        [HttpGet]
        public Car SearchCar(string name)
        {
            return carRepository.GetAllCar().Where(a => a.Name == name).FirstOrDefault();
        }

        [Route("api/Car/BookCar/{x:int}/{y:int}")]
        [HttpGet]
        public HttpResponseMessage BookCar(int x, int y)
        {
            try
            {
                var cars = carRepository.GetAllCar();
                if (cars.Exists(a => a.IsBooked))
                    return Request.CreateResponse(HttpStatusCode.OK, "You have booked the car, Please return the car before another book");
                else
                {
                    cars = cars.Where(a => !a.IsBooked).OrderBy(b => b.PositionX + b.PositionY).ToList();
                    foreach (var car in cars)
                    {
                        CarGenerate carGenerate = new CarGenerate(car);
                        bool booked = carGenerate.IsAvailable(x, y);
                        if (booked)
                        {
                            carGenerate.BookCar();
                            return Request.CreateResponse(HttpStatusCode.OK, "You book the car : " + car.Name);
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, "No cars can be booked");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Route("api/Car/ReturnCar")]
        [HttpGet]
        public HttpResponseMessage ReturnCar()
        {
            try
            {
                var car = carRepository.GetAllCar().Where(a => a.IsBooked).FirstOrDefault();
                if (car != null)
                {
                    CarGenerate carGenerate = new CarGenerate(car);
                    carGenerate.ReturnCar();
                    return Request.CreateResponse(HttpStatusCode.OK, "The car has been returned.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "You dont book any car");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
