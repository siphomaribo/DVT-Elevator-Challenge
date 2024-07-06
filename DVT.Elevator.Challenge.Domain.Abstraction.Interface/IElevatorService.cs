using DVT.Elevator.Challenge.Domain.Model;

namespace DVT.Elevator.Challenge.Domain.Abstraction.Interface
{
    public interface IElevatorService
    {
        void RequestElevator(int floorNumber, int numberOfPeople);
        void MoveElevator(int elevatorId, int targetFloor);
        ElevatorModel GetElevatorStatus(int elevatorId);
        void DisplayElevatorStatuses();
    }
}
