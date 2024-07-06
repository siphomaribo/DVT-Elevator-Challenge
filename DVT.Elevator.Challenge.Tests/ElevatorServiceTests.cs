using DVT.Elevator.Challenge.Domain.Model;
using DVT.Elevator.Challenge.Domain.Service;
using System.Text;

namespace DVT.Elevator.Challenge.Tests
{
    [TestFixture]
    public class ElevatorServiceTests
    {
        private ElevatorService _elevatorService;
        private List<ElevatorModel> _elevators;

        [SetUp]
        public void Setup()
        {
            _elevators = new List<ElevatorModel>
            {
                new ElevatorModel() { Id = 1, CurrentFloor = 1, Direction = ElevatorDirectionEnum.Idle, Capacity = 10, Type = ElevatorTypeEnum.Passenger },
                new ElevatorModel() { Id = 2, CurrentFloor = 5, Direction = ElevatorDirectionEnum.Up, Capacity = 8, Type = ElevatorTypeEnum.Passenger },
                new ElevatorModel() { Id = 3, CurrentFloor = 10, Direction = ElevatorDirectionEnum.Down, Capacity = 12, Type = ElevatorTypeEnum.HighSpeed },
                new ElevatorModel() { Id = 4, CurrentFloor = 15, Direction = ElevatorDirectionEnum.Down, Capacity = 15, Type = ElevatorTypeEnum.Freight }
            };

            _elevatorService = new ElevatorService(20, _elevators);
        }

        [Test]
        public void RequestElevator_ShouldMoveNearestElevatorToRequestedFloor()
        {
            _elevators[0].CurrentFloor = 1;
            _elevators[1].CurrentFloor = 5;
            _elevators[2].CurrentFloor = 10;
            _elevators[3].CurrentFloor = 15;

            _elevatorService.RequestElevator(6, 3);

            var nearestElevator = _elevators.Find(e => e.CurrentFloor == 6);

            Assert.That(nearestElevator, Is.Not.Null, "No elevator moved to the requested floor.");
            Assert.That(nearestElevator.CurrentFloor, Is.EqualTo(6));
            Assert.That(nearestElevator.Direction, Is.EqualTo(ElevatorDirectionEnum.Idle));
            Assert.That(nearestElevator.Occupied, Is.EqualTo(3));
        }

        [Test]
        public void RequestElevator_ShouldNotExceedCapacity()
        {
            _elevatorService.RequestElevator(6, 20);
            Assert.That(_elevators[0].CurrentFloor, Is.EqualTo(1)); 
            Assert.That(_elevators[0].Occupied, Is.EqualTo(0));
        }

        [Test]
        public void RequestElevator_NoAvailableElevator_ShouldNotMoveAnyElevator()
        {
            _elevators[0].Occupied = 10; 
            _elevators[1].Occupied = 8; 
            _elevators[2].Occupied = 12; 
            _elevators[3].Occupied = 15; 

            _elevatorService.RequestElevator(6, 3);
            Assert.That(_elevators[0].CurrentFloor, Is.EqualTo(1)); 
            Assert.That(_elevators[1].CurrentFloor, Is.EqualTo(5));
            Assert.That(_elevators[2].CurrentFloor, Is.EqualTo(10));
            Assert.That(_elevators[3].CurrentFloor, Is.EqualTo(15));
        }

        [Test]
        public void MoveElevator_ShouldMoveElevatorToTargetFloor()
        {
            _elevatorService.MoveElevator(1, 7);
            Assert.That(_elevators[0].CurrentFloor, Is.EqualTo(7));
            Assert.That(_elevators[0].Direction, Is.EqualTo(ElevatorDirectionEnum.Idle));
        }

        [Test]
        public void GetElevatorStatus_ShouldReturnCorrectStatus()
        {
            var status = _elevatorService.GetElevatorStatus(1);
            Assert.That(status.Id, Is.EqualTo(1));
            Assert.That(status.CurrentFloor, Is.EqualTo(1));
            Assert.That(status.Capacity, Is.EqualTo(10));
            Assert.That(status.Occupied, Is.EqualTo(0));
            Assert.That(status.Direction, Is.EqualTo(ElevatorDirectionEnum.Idle));
            Assert.That(status.Type, Is.EqualTo(ElevatorTypeEnum.Passenger));
        }

        [Test]
        public void DisplayElevatorStatuses_ShouldReturnCorrectStatusForAllElevators()
        {
            _elevators[0].CurrentFloor = 3;
            _elevators[0].Occupied = 4;
            _elevators[1].CurrentFloor = 6;
            _elevators[1].Occupied = 2;

            StringBuilder statuses = _elevatorService.DisplayElevatorStatuses();

            StringBuilder expectedStatuses = new StringBuilder();

            expectedStatuses.AppendLine("Elevator 1: Floor 3, Direction Idle, Occupied 4/10, Type Passenger");
            expectedStatuses.AppendLine("Elevator 2: Floor 6, Direction Up, Occupied 2/8, Type Passenger");
            expectedStatuses.AppendLine("Elevator 3: Floor 10, Direction Down, Occupied 0/12, Type HighSpeed");
            expectedStatuses.AppendLine("Elevator 4: Floor 15, Direction Down, Occupied 0/15, Type Freight");

            Assert.That(statuses.ToString(), Is.EqualTo(expectedStatuses.ToString()));
        }
    }
}
