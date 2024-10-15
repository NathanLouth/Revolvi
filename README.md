# Revolvi

Revolvi is a WPF application designed to automate tasks on your Windows system based on user inactivity. It allows users to perform actions such as refreshing a page or rotating browser tabs after a specified period of inactivity.

![Image of Revolvi Program](https://github.com/NathanLouth/Revolvi/blob/main/img/Example.png?raw=true)

## Features

- **Automatic Actions**: Automatically refresh or rotate browser tabs based on user-defined parameters.
- **Idle Time Detection**: Monitors system idle time to determine when to trigger actions.
- **User-Friendly Interface**: Simple and intuitive user interface built using WPF.

## Installation

1. Download the latest "Revolvi_Installer.msi" MSI from the Releases section
2. Install the MSI
3. Run Revolvi

## Build Project

1. Clone the repository:
   ```bash
   git clone https://github.com/NathanLouth/Revolvi.git
   ```
2. Open the solution file in Visual Studio.
3. Build the project to restore dependencies and compile the application.

## Usage

1. Launch the application.
2. Adjust the delay slider to set the desired inactivity time (in seconds).
3. Check the options for refresh, rotate, inactive, and awake settings based on your preferences.
4. Click the **Start** button to begin monitoring for user inactivity.
5. To stop the monitoring, click the **Stop** button.

### Controls

- **Slider**: Adjust the delay before actions are executed.
- **Check Boxes**: Toggle actions (refresh, rotate, inactive, awake).

## Code Overview

- The application uses the `System.Windows` namespace for the UI and leverages the `System.Runtime.InteropServices` namespace to access Windows API functions for managing system state.
- It uses a background task to monitor user inactivity and perform specified actions based on user settings.

### Key Classes

- **MainWindow**: The main window of the application that contains UI controls and logic for managing application behavior.
- **IdleTimeFinder**: A utility class to determine the system's idle time.

### Important Methods

- `BackGroundTask`: Asynchronously monitors idle time and executes actions based on user settings.
- `SetThreadExecutionState`: Prevents the system from entering sleep mode while the application is active.

## Dependencies

- .NET Framework 4.5 or higher
- WPF libraries for UI components

## Contributing

If you would like to contribute to Revolvi, please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License.

## Contact

For questions or feedback, please reach out.

