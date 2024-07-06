using DVT.Elevator.Challenge.Domain.Abstraction.Interface;
using DVT.Elevator.Challenge.Domain.Model;
using System.Drawing;
using System.Text;

namespace DVT.Elevator.Challenge.Domain.Service
{
    public class ElevatorService : IElevatorService
    {
        private readonly List<ElevatorModel> _elevators;
        private readonly int _totalFloors;
        public ElevatorService(int totalFloors, List<ElevatorModel> elevators) {
            _totalFloors = totalFloors;
            _elevators = elevators;
        }

        public StringBuilder DisplayElevatorStatuses()
        {
            var statusBuilder = new StringBuilder();

            foreach (var elevator in _elevators)
            {
                statusBuilder.AppendLine(elevator.ToString());
            }

            return statusBuilder;
        }

        public ElevatorModel GetElevatorStatus(int elevatorId)
        {
            return _elevators.FirstOrDefault(elevator => elevator?.Id == elevatorId);
        }

        public void MoveElevator(int elevatorId, int targetFloor)
        {
            var elevator = _elevators.FirstOrDefault(elevator => elevator.Id == elevatorId);

            if (elevator != null)
            {
                elevator.Direction = elevator.CurrentFloor < targetFloor ? ElevatorDirectionEnum.Up : ElevatorDirectionEnum.Down;
                elevator.CurrentFloor = targetFloor;
                elevator.Direction = ElevatorDirectionEnum.Idle;
            }
        }

        public void RequestElevator(int floorNumber, int numberOfPeople)
        {
            ElevatorModel nearestElevator = null;
            int shortestDistance = int.MaxValue;

            foreach (var elevator in _elevators)
            {
                if (elevator.Occupied + numberOfPeople <= elevator.Capacity)
                {
                    int distance = Math.Abs(elevator.CurrentFloor - floorNumber);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestElevator = elevator;
                    }
                }
            }

            if (nearestElevator != null)
            {
                MoveElevator(nearestElevator.Id, floorNumber);
                nearestElevator.Occupied += numberOfPeople;
            }
            else
            {
                Console.WriteLine("No available elevator found. Please wait.");
            }
        }
    }
}
