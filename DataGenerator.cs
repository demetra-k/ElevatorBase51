namespace ElevatorExamProject
{
    using System;
    using Enums;

    public static class DataGenerator
    {
        public static Random Generator = new Random();

        public static SecurityLevelsAgent GenerateAgentLevel() => (SecurityLevelsAgent)Generator.Next(0, 2);
       
        public static FloorLevels GenerateAgentCurrentFloor() => (FloorLevels)Generator.Next(1, 4);
      
        public static FloorLevels GenerateAgentFloorToGo() => (FloorLevels)Generator.Next(1, 4);
    }
}
