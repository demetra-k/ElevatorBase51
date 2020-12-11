# ElevatorBase51

I have created three separate classes for agents, the elevator and random data generation.
In the Elevator class I have simulated the movements of the elevator as the CheckPermissions method checks if the agent has access to a floor.
The agents are managed by threads and when an agent enters the elevator it is saved in ConcurrentQueque.Тhe elevator is also controlled by a thread that checks what has
in the queue and according to this it moves the elevator.
