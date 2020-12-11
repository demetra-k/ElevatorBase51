namespace ElevatorExamProject
{
    using System.Threading.Tasks;

    public class Program
    {
        private static async Task Main()
        {
            var elevator = new Elevator();

            for (var i = 1; i <= 3; i++)
            {
                var agentLevel = DataGenerator.GenerateAgentLevel();
                var agentCurrentFloor = DataGenerator.GenerateAgentCurrentFloor();
                var agentFloorToGo = DataGenerator.GenerateAgentFloorToGo();

                var agent = new Agent(i, agentLevel, agentCurrentFloor, agentFloorToGo);

                Task.Run(() => elevator.Call(agent));
            }

            await elevator.Worker();
        }
    }
}
