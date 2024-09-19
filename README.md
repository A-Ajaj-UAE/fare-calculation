# Fare-Calculation
This solution is designed to allow back-office staff to calculate driver payments based on multiple dynamic fare bands. 

The number of bands is flexible and can be modified in the future.

# Document Analysis
The requirements discuss the concept of multiple fare bands but do not specify handling for gaps between bands. 

There’s also a logical inconsistency where the cost drops for miles 2-6 by rate 2, increases for the next 10 miles by rate 3, and then decreases again, which could be a business requirement or an error in the document.

Same for Limits, logicly it should increased

# For this implementation:

I’ve made assumptions and created a solution that allow dynamic change on the bands, also add a separte property for default Fare.

I have added IsStrictMode to throw exceptions for logical inconsistency if its enabled and display warning if it disabled


# Data Structure for Fare Bands
The solution is built with a flexible data structure, ideal for easy of usage and configure with dynamic abbility to change

# Band Attributes:
Limit (int): how many miles that apply.

Fare (decimal): The amount to pay for each mile within the limit.

Order (int): Determines excution order of bands .


# Solution Architecture
The project is kept simple and consists of two core parts:

## Console Application:

This holds the core logic, including entities, validators, and utility functions to perform the fare calculations.
It writes output to the console, simulating logs and providing feedback on exceptions.

## Unit Test Project:

The test project is linked to the main project and verifies key functionality, such as band validation and fare calculation logic.

# How to Run the Application
Clone the project to your local machine.

```
git clone <repository-url>
```

# Install the required dependencies.

```.NET 8 SDK: Ensure that you have .NET 8 installed on your system.```

# Run the console application.


```
dotnet run
```

The application will display output in the console, detailing the fare calculations based on the bands.
