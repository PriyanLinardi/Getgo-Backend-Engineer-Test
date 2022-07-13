namespace CarSharing.Interface
{
    public interface ITransportation
    {
        int Id { get; set; }
        string Name { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        bool IsBooked { get; set; }

    }
}