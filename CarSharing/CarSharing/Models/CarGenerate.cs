using CarSharing.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarSharing.Service;

namespace CarSharing.Models
{
    public class CarGenerate : ITransportationGenerate
    {
        public CarGenerate(ITransportation car)
        {
            this.Transportation = car;
            this.CarRepository = new CarRepository();
        }

        public ITransportation Transportation { get; set; }
        public CarRepository CarRepository { get; set; }

        public bool IsAvailable(int x, int y)
        {
            int valueX = (Transportation.PositionX > x ? Transportation.PositionX - x : x - Transportation.PositionX);
            int valueY = (Transportation.PositionY > y ? Transportation.PositionY - y : y - Transportation.PositionY);
            return (valueY + valueX <= 2);
        }

        public void BookCar()
        {
            List<Car> cars = CarRepository.GetAllCar();
            if (cars.Exists(a => a.Id == Transportation.Id))
            {
                Car carSelected = cars.Where(a => a.Id == Transportation.Id).FirstOrDefault();
                carSelected.IsBooked = true;
                CarRepository.UpdateCarRepo(cars);
            }
        }

        public void ReturnCar()
        {
            List<Car> cars = CarRepository.GetAllCar();
            if (cars.Exists(a => a.Id == Transportation.Id))
            {
                Car carSelected = cars.Where(a => a.Id == Transportation.Id).FirstOrDefault();
                carSelected.IsBooked = false;
                CarRepository.UpdateCarRepo(cars);
            }
        }
    }
}