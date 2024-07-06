namespace DVT.Elevator.Challenge.Domain.Model
{
    public class ElevatorModel
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int Capacity { get; set; }
        public int Occupied { get; set; }
        public ElevatorDirectionEnum Direction { get; set; }
        public ElevatorTypeEnum Type { get; set; }

    }
}
