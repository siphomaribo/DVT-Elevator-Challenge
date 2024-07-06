using DVT.Elevator.Challenge.Domain.Abstraction.Interface;
using DVT.Elevator.Challenge.Domain.Model;
using DVT.Elevator.Challenge.Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Bind configuration to list of elevators
            var elevators = new List<ElevatorModel>();
            configuration.GetSection("Elevators").Bind(elevators);

            if (elevators == null || elevators.Count == 0)
            {
                Console.WriteLine("No elevators configured. Please check the appsettings.json file.");
                return;
            }

            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IElevatorService>(sp => new ElevatorService(20, elevators))
                .BuildServiceProvider();

            var _elevatorService = serviceProvider.GetService<IElevatorService>();

            while (true)
            {
                try
                {
                    Console.WriteLine("1. Request Elevator");
                    Console.WriteLine("2. Check Elevator Status");
                    Console.WriteLine("3. Display All Elevator Statuses");
                    Console.WriteLine("4. Exit");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter floor number: ");
                            if (!int.TryParse(Console.ReadLine(), out int floor))
                            {
                                Console.WriteLine("Invalid input for floor number. Please enter an integer.");
                                break;
                            }

                            Console.Write("Enter number of people: ");
                            if (!int.TryParse(Console.ReadLine(), out int people))
                            {
                                Console.WriteLine("Invalid input for number of people. Please enter an integer.");
                                break;
                            }

                            await _elevatorService.RequestElevatorAsync(floor, people);
                            break;

                        case "2":
                            Console.Write("Enter elevator ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.WriteLine("Invalid input for elevator ID. Please enter an integer.");
                                break;
                            }

                            var status = _elevatorService.GetElevatorStatus(id);
                            if (status == null)
                            {
                                Console.WriteLine("Elevator not found.");
                            }
                            else
                            {
                                Console.WriteLine(status);
                            }
                            break;

                        case "3":
                            var statuses = _elevatorService.DisplayElevatorStatuses();
                            Console.WriteLine(statuses.ToString());
                            break;

                        case "4":
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to start the application: {ex.Message}");
        }
    }
}
