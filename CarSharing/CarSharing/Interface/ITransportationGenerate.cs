using CarSharing.Interface;

namespace CarSharing.Interface
{
    public interface ITransportationGenerate
    {
        bool IsAvailable(int x, int y);
        void BookCar();
        void ReturnCar();
    }
}