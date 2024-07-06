# DVT Elevator Challenge 

## Overview

This application simulates the operation of multiple elevators within a building environment. It allows users to interact with the elevators by requesting them to specific floors, checking their current status, and displaying overall system status. The simulation is configurable via the appsettings.json file, allowing customization of elevator configurations.
## Features

- **Request Elevator**: Users can input a floor number and the number of people needing the elevator. The system will dispatch the nearest available elevator to the requested floor.
  
- **Check Elevator Status**: Users can query the status of a specific elevator by entering its ID. The application displays its current floor, direction, occupancy, and type.
  
- **Display All Elevator Statuses**: Provides an overview of all elevators in the system, including their current positions, directions, occupancy levels, and types.
  
- **Configuration via appsettings.json**: Elevator configurations (such as IDs, current floors, capacities, types) are loaded from an `appsettings.json` file, making the application easily configurable.

- **Error Handling**: The application handles various errors gracefully, such as invalid input for floor numbers, number of people, and elevator IDs. It provides clear error messages to assist users in correct usage.

- **Dependency Injection**: Utilizes the Microsoft.Extensions.DependencyInjection framework to manage dependencies and facilitate unit testing.

- **Asynchronous Operations**: Uses asynchronous programming techniques (async/await) to handle elevator movements and user requests efficiently.

## Installation

1. **Clone Repository**: Clone the repository to your local machine using Git:

   ```bash
   git clone https://github.com/siphomaribo/DVT-Elevator-Challenge.git
   ```

2. **Restore Dependencies**: Ensure you have .NET Core SDK installed. Restore the project dependencies:

   ```bash
   dotnet restore
   ```

3. **Configure Elevators**: Modify the `appsettings.json` file to configure the elevators according to your building's setup. Example configuration:

   ```json
   {
     "Elevators": [
       {
         "Id": 1,
         "CurrentFloor": 1,
         "Direction": "Idle",
         "Capacity": 10,
         "Type": "Passenger"
       },
       {
         "Id": 2,
         "CurrentFloor": 5,
         "Direction": "Up",
         "Capacity": 8,
         "Type": "Passenger"
       }
     ]
   }
   ```

4. **Run the Application**: Start the application using the following command:

   ```bash
   dotnet run
   ```

   Follow the on-screen instructions to interact with the elevator simulation.

## Usage

- Upon running the application, a menu will prompt you to choose an action:
  - **1**: Request Elevator
  - **2**: Check Elevator Status
  - **3**: Display All Elevator Statuses
  - **4**: Exit
  
- Choose an option by entering the corresponding number and follow the prompts to proceed.

## Contributing

NA

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- This application was developed as a simulation project for a job application @ DVT.
- Special thanks to the [Microsoft.Extensions.DependencyInjection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) for facilitating dependency injection in .NET applications.

---
