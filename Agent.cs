namespace ElevatorExamProject
{
    using Enums;
    using System.Collections.Generic;

    public class Agent
    {
        public int Id { get; set; }

        public FloorLevels FloorToGo { get; set; }
        
        public FloorLevels CurrentFloor { get; set; }

        public SecurityLevelsAgent SecurityLevelsAgent { get; set; }

        public List<FloorLevels> FloorsAllowed { get; set; }

        public Agent(int id, SecurityLevelsAgent agentLevel, FloorLevels currentFloor, FloorLevels floorToGo)
        {
            Id = id;
            CurrentFloor = currentFloor;
            FloorToGo = floorToGo;
            SecurityLevelsAgent = agentLevel;

            SetAllowedFloors(agentLevel);    
        }

        private void SetAllowedFloors(SecurityLevelsAgent agentLevel)
        {
            FloorsAllowed = new List<FloorLevels>();

            switch (agentLevel)
            {
                case SecurityLevelsAgent.Confidential:
                    FloorsAllowed.Add(FloorLevels.G);
                    break;

                case SecurityLevelsAgent.Secret:
                    FloorsAllowed.Add(FloorLevels.G);
                    FloorsAllowed.Add(FloorLevels.S);
                    break;

                case SecurityLevelsAgent.TopSecret:
                    FloorsAllowed.Add(FloorLevels.G);
                    FloorsAllowed.Add(FloorLevels.S);
                    FloorsAllowed.Add(FloorLevels.T1);
                    FloorsAllowed.Add(FloorLevels.T2);
                    break;
                default:
                    break;
            }
        }
    }
}
