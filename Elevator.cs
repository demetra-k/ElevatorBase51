namespace ElevatorExamProject
{
    using Enums;
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    public class Elevator
    {
        public bool EmptyElevator { get; set; }

        public FloorLevels CurrentFloor = FloorLevels.G;

        private readonly ConcurrentQueue<Agent> _enteredAgents = new ConcurrentQueue<Agent>();
        private const int TopFloor = 4;

        public Elevator()
        {
            EmptyElevator = false;
            Task.Run(Worker);
        }

        public void Call(Agent agent)
        {
            Console.WriteLine($"{agent.SecurityLevelsAgent } agent with id {agent.Id} enters in the elevator. FloorToGo: {agent.FloorToGo}");
            _enteredAgents.Enqueue(agent);
        }

        public async Task Worker()
        {
            while (!EmptyElevator)
            {
                foreach (var enteredAgent in _enteredAgents)
                {
                    if (CurrentFloor != enteredAgent.CurrentFloor)
                        WaitingElevator(enteredAgent.CurrentFloor);

                    var hasAccess = CheckPermissions(enteredAgent);

                    while (!hasAccess)
                    {
                        enteredAgent.FloorToGo = DataGenerator.GenerateAgentFloorToGo();
                        hasAccess = CheckPermissions(enteredAgent);
                    }

                    StartMoving(enteredAgent.FloorToGo);
                    Leave(enteredAgent);
                }
            }
        }

        private void WaitingElevator(FloorLevels floorToGo)
        {
            StartMoving(floorToGo);
        }

        private void Descend(FloorLevels floor)
        {
            Console.WriteLine($"The elevator is descending from floor {floor}...");

            var currentFloorNumeric = (int)CurrentFloor;
            for (var i = currentFloorNumeric; i < 0; i--)
            {
                if (i == (int)floor)
                    CurrentFloor = floor;
            }
        }

        private void Ascend(FloorLevels floor)
        {
            Console.WriteLine($"The elevator is ascending to {floor}...");

            var currentFloorNumeric = (int)CurrentFloor;
            for (var i = currentFloorNumeric; i <= TopFloor; i++)
            {
                if (i == (int)floor)
                    CurrentFloor = floor;
            }
        }

        private void Leave(Agent agent)
        {
            CheckPermissions(agent);
            _enteredAgents.TryDequeue(out _);

            Console.WriteLine($"{agent.SecurityLevelsAgent} agent with id {agent.Id} left the elevator on {CurrentFloor} floor.");

            if (_enteredAgents.IsEmpty)
            {
                Console.WriteLine("Elevator is empty!");
                EmptyElevator = true;
            }
        }

        private void StartMoving(FloorLevels floorToGo)
        {
            if (floorToGo > CurrentFloor)
                Ascend(floorToGo);
            else
                Descend(floorToGo);

            CurrentFloor = floorToGo;
        }

        private bool CheckPermissions(Agent agent)
        {
            var hasAccess = agent.FloorsAllowed.Contains(agent.FloorToGo);
            if (!hasAccess)
                Console.WriteLine($"{agent.SecurityLevelsAgent} agent with id {agent.Id} has no access to {agent.FloorToGo}. Other floor should be chosen.");

            return hasAccess;
        }
    }
}

