using DVT.Elevator.Challenge.Domain.Abstraction.Interface;
using DVT.Elevator.Challenge.Domain.Model;

namespace DVT.Elevator.Challenge.Domain.Service
{
    public class ElevatorService : IElevatorService
    {
        public void DisplayElevatorStatuses()
        {
            throw new NotImplementedException();
        }

        public ElevatorModel GetElevatorStatus(int elevatorId)
        {
            throw new NotImplementedException();
        }

        public void MoveElevator(int elevatorId, int targetFloor)
        {
            throw new NotImplementedException();
        }

        public void RequestElevator(int floorNumber, int numberOfPeople)
        {
            throw new NotImplementedException();
        }
    }
}
