using DVT.Elevator.Challenge.Domain.Model;
using System.Text;

namespace DVT.Elevator.Challenge.Domain.Abstraction.Interface
{
    public interface IElevatorService
    {
        Task RequestElevatorAsync(int floorNumber, int numberOfPeople);
        Task MoveElevatorAsync(int elevatorId, int targetFloor);
        ElevatorModel GetElevatorStatus(int elevatorId);
        StringBuilder DisplayElevatorStatuses();
    }
}
