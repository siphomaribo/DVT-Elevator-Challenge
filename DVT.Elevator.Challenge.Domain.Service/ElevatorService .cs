using DVT.Elevator.Challenge.Domain.Abstraction.Interface;
using DVT.Elevator.Challenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Challenge.Domain.Service
{
    public class ElevatorService : IElevatorService
    {
        private readonly List<ElevatorModel> _elevators;
        private readonly int _totalFloors;
        private readonly Queue<(int FloorNumber, int NumberOfPeople)> _requestsQueue = new();
        private const int WeightLimit = 10;

        public ElevatorService(int totalFloors, List<ElevatorModel> elevators)
        {
            _totalFloors = totalFloors;
            _elevators = elevators ?? throw new ArgumentNullException(nameof(elevators));
        }

        public StringBuilder DisplayElevatorStatuses()
        {
            var statusBuilder = new StringBuilder();
            _elevators.ForEach(elevator => statusBuilder.AppendLine(elevator.ToString()));
            return statusBuilder;
        }

        public ElevatorModel GetElevatorStatus(int elevatorId)
        {
            return _elevators.FirstOrDefault(elevator => elevator?.Id == elevatorId);
        }

        public async Task MoveElevatorAsync(int elevatorId, int targetFloor)
        {
            if (targetFloor < 0 || targetFloor > _totalFloors)
            {
                throw new ArgumentOutOfRangeException(nameof(targetFloor), "Target floor is out of range.");
            }

            var elevator = _elevators.FirstOrDefault(elevator => elevator.Id == elevatorId);
            if (elevator != null)
            {
                await MoveElevatorToFloorAsync(elevator, targetFloor);
            }
            else
            {
                throw new InvalidOperationException($"Elevator with ID {elevatorId} not found.");
            }
        }

        public async Task RequestElevatorAsync(int floorNumber, int numberOfPeople)
        {
            if (floorNumber < 0 || floorNumber > _totalFloors)
            {
                throw new ArgumentOutOfRangeException(nameof(floorNumber), "Requested floor number is out of range.");
            }

            if (numberOfPeople > WeightLimit)
            {
                throw new InvalidOperationException($"Number of people exceeds the weight limit of {WeightLimit}.");
            }

            _requestsQueue.Enqueue((floorNumber, numberOfPeople));
            await ProcessRequestsQueueAsync();
        }

        private async Task ProcessRequestsQueueAsync()
        {
            while (_requestsQueue.Count > 0)
            {
                var (floorNumber, numberOfPeople) = _requestsQueue.Dequeue();
                var nearestElevator = FindNearestAvailableElevator(floorNumber, numberOfPeople);

                if (nearestElevator != null)
                {
                    await MoveElevatorToFloorAsync(nearestElevator, floorNumber);
                    nearestElevator.Occupied += numberOfPeople;
                }
                else
                {
                    Console.WriteLine("No available elevator found. Please wait.");
                }
            }
        }

        private ElevatorModel FindNearestAvailableElevator(int floorNumber, int numberOfPeople)
        {
            return _elevators
                .Where(elevator => elevator.Occupied + numberOfPeople <= elevator.Capacity && numberOfPeople <= WeightLimit)
                .OrderBy(elevator => Math.Abs(elevator.CurrentFloor - floorNumber))
                .FirstOrDefault();
        }

        private async Task MoveElevatorToFloorAsync(ElevatorModel elevator, int targetFloor)
        {
            // Simulate elevator movement delay
            await Task.Delay(1000 * Math.Abs(elevator.CurrentFloor - targetFloor));

            // Determine direction based on current and target floors
            elevator.Direction = elevator.CurrentFloor < targetFloor ? ElevatorDirectionEnum.Up : ElevatorDirectionEnum.Down;

            // Move elevator to the target floor
            elevator.CurrentFloor = targetFloor;

            // Set the elevator to idle after reaching the target floor
            elevator.Direction = ElevatorDirectionEnum.Idle;
        }
    }
}
