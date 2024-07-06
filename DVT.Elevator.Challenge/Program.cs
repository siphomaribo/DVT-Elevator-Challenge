using DVT.Elevator.Challenge.Domain.Abstraction.Interface;
using DVT.Elevator.Challenge.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using DVT.Elevator.Challenge.Domain.Model;

var serviceProvider = new ServiceCollection()
                .AddSingleton<IElevatorService>(sp => new ElevatorService(10, new List<ElevatorModel>
                {
                new ElevatorModel() { Id = 1, CurrentFloor = 1, Direction = ElevatorDirectionEnum.Idle, Capacity = 10, Type = ElevatorTypeEnum.Passenger },
                new ElevatorModel() { Id = 2, CurrentFloor = 5, Direction = ElevatorDirectionEnum.Up, Capacity = 8, Type = ElevatorTypeEnum.Passenger },
                new ElevatorModel() { Id = 3, CurrentFloor = 10, Direction = ElevatorDirectionEnum.Down, Capacity = 12, Type = ElevatorTypeEnum.HighSpeed },
                new ElevatorModel() { Id = 4, CurrentFloor = 15, Direction = ElevatorDirectionEnum.Down, Capacity = 15, Type = ElevatorTypeEnum.Freight }
                }))
                .BuildServiceProvider();

var elevatorService = serviceProvider.GetService<IElevatorService>();

Console.WriteLine("Hello, World!");