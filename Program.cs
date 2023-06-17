using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ToyRobotPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRobotService, RobotService>()
                .BuildServiceProvider();

            var robot = serviceProvider.GetService<IRobotService>();
            string command = "";
            do
            {
                Console.Write("Enter Command: ");
                command = Console.ReadLine();
                if (!string.IsNullOrEmpty(command))
                    robot.executeCommand(command);
            } while (command != "EXIT");
        }
    }
}
