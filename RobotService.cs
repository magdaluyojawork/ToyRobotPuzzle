using System;
using System.Text.RegularExpressions;

namespace ToyRobotPuzzle
{
    public class RobotService : IRobotService
    {
        private static string[] _arrDirections = { "NORTH", "EAST", "SOUTH", "WEST" };
        private const int _units = 4; //from point 0
        static bool _is1stCommandPLACEExecuted = false;
        static int _currentX;
        static int _currentY;
        static string _currentDirection;

        public RobotService(){
            _currentX = 0;
            _currentY = 0;
            _currentDirection = "NORTH";
        }
        public void executeCommand(string command)
        {
            Match isCommandPLACE = Regex.Match(command, Command.PLACE.ToString());
            bool isCommandMOVE = command == Command.MOVE.ToString();
            bool isCommandLEFT = command == Command.LEFT.ToString();
            bool isCommandRIGHT = command == Command.RIGHT.ToString();
            bool isCommandREPORT = command == Command.REPORT.ToString();
            if (isCommandPLACE.Success)
            {
                validatePLACECommand(command);
                    
            }
            else if (_is1stCommandPLACEExecuted && isCommandMOVE)
            {
                move();
            }
            else if (_is1stCommandPLACEExecuted && isCommandLEFT)
            {
                turn("LEFT");
            }
            else if (_is1stCommandPLACEExecuted && isCommandRIGHT)
            {
                turn("RIGHT");
            }
            else if (_is1stCommandPLACEExecuted && isCommandREPORT)
            {
                report();
            }

        }
        private void validatePLACECommand(string commandPLACE)
        {
            bool isValidPLACECommand = true;
            string parameters = commandPLACE.Remove(0, 5).Trim();
            string[] arrParameters = parameters.Split(",");

            if (arrParameters.Length < 2 || arrParameters.Length > 3)
                isValidPLACECommand = false;

            string paramX = arrParameters[0].Trim();
            string paramY = arrParameters[1].Trim();
            int x, y;
            var isMoreThan1Space = Regex.Matches(commandPLACE, @"[\s]+");

            if (isMoreThan1Space.Count > 1)
                isValidPLACECommand = false;
            else if (!_is1stCommandPLACEExecuted && arrParameters.Length != 3)
                isValidPLACECommand = false;
            else if (!(int.TryParse(paramX, out x) && x >= 0))
                isValidPLACECommand = false;
            else if (!(int.TryParse(paramY, out y) && y >= 0))
                isValidPLACECommand = false;
            else if (Convert.ToInt32(paramX) < 0 || Convert.ToInt32(paramY) < 0 || Convert.ToInt32(paramX) > _units || Convert.ToInt32(paramY) > _units)
                isValidPLACECommand = false;
            else
            {
                isValidPLACECommand = true;
            }

            if (isValidPLACECommand)
            {
                _currentX = Convert.ToInt32(paramX);
                _currentY = Convert.ToInt32(paramY);
                _currentDirection = arrParameters.Length == 2 ? _currentDirection : arrParameters[2].Trim();
                _is1stCommandPLACEExecuted = true;
            }
        }
        private void move()
        {
            if (_currentDirection == "NORTH")
            {
                if (_currentY < _units)
                    _currentY++;
            }
            else if (_currentDirection == "EAST")
            {
                if (_currentX < _units)
                    _currentX++;
            }
            else if (_currentDirection == "SOUTH")
            {
                if (_currentY > 1)
                    _currentY--;
            }
            else if (_currentDirection == "WEST")
            {
                if (_currentX > 1)
                    _currentX--;
            }
        }
        private void turn(string turnTo)
        {
            int currentDirectionIndex = Array.IndexOf(_arrDirections, _currentDirection);
            if (turnTo == "LEFT")
            {
                if (currentDirectionIndex == 0)
                    _currentDirection = _arrDirections[_arrDirections.Length - 1];
                else
                    _currentDirection = _arrDirections[currentDirectionIndex - 1];
            }
            else
            {
                if (currentDirectionIndex == _arrDirections.Length)
                    _currentDirection = _arrDirections[0];
                else
                    _currentDirection = _arrDirections[currentDirectionIndex + 1];
            }
        }
        private void report()
        {
            Console.WriteLine($"{_currentX},{_currentY},{_currentDirection}");
        }
    }
}
