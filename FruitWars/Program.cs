using FruitWars.Enums;
using FruitWars.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitWars
{
    class Program
    {
        const int MATRIX_ROWS = 8;
        const int MATRIX_COLUMNS = 8;

        static void Main(string[] args)
        {
            Player firstPlayer = CreatePlayer("Player1");
            Player secondPlayer = CreatePlayer("Player2");

            char[,] matrix = new char[MATRIX_ROWS, MATRIX_COLUMNS];

            IDictionary<Point, char> points = CreateMatrix(matrix, firstPlayer, secondPlayer, true);
            Console.WriteLine($"Player1 : { firstPlayer.PlayerType.Power} Power; { firstPlayer.PlayerType.Speed} Speed;");
            Console.WriteLine($"Player2 : { secondPlayer.PlayerType.Power} Power; { secondPlayer.PlayerType.Speed} Speed;");

            //first check the value then count?
            for (int i = 0; i < firstPlayer.PlayerType.Speed; i++)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Directions direction = ReadUserKeyMove(key);
                switch (direction)
                {
                    case Directions.Left:
                        int positionLeftY = firstPlayer.Position.Y - 1;
                        if (positionLeftY >= MATRIX_ROWS)
                        {
                            Console.WriteLine("Change the direction");
                            return;
                        }
                        firstPlayer.Position.Y = positionLeftY;
                        Turn(matrix, firstPlayer, secondPlayer);
                        break;
                    case Directions.Right:
                        int positionRightY = firstPlayer.Position.Y + 1;
                        if (positionRightY >= MATRIX_ROWS)
                        {
                            Console.WriteLine("Change the direction");
                            return;
                        }
                        firstPlayer.Position.Y = positionRightY;
                        Turn(matrix, firstPlayer, secondPlayer);

                        break;
                    case Directions.Up:
                        firstPlayer.Position.X -= 1;

                        int positionUpX = firstPlayer.Position.X - 1;
                        if (positionUpX >= MATRIX_ROWS)
                        {
                            Console.WriteLine("Change the direction");
                            return;
                        }
                        firstPlayer.Position.X = positionUpX;
                        Turn(matrix, firstPlayer, secondPlayer);

                        break;
                    case Directions.Down:
                        firstPlayer.Position.X += 1;

                        int positionDownX = firstPlayer.Position.X - 1;
                        if (positionDownX >= MATRIX_ROWS)
                        {
                            Console.WriteLine("Change the direction");
                            return;
                        }
                        firstPlayer.Position.X = positionDownX;
                        Turn(matrix, firstPlayer, secondPlayer);
                        break;
                    default:
                        break;
                }
                if (i == firstPlayer.PlayerType.Speed - 1)
                {
                    Console.WriteLine("Player1, make a move please!");
                }
            }

        }

        private static Player CreatePlayer(string playerName)
        {
            Console.WriteLine($"{playerName}, please choose a warrior.");
            Console.WriteLine("Insert 1 for turtle / 2 for monkey / 3 for pigeon");
            string userInput = Console.ReadLine(); //Validate input btw (1,2,3)
            int userChoice = ValidateUserInput(userInput);
            return ChoosePlayer(userChoice);
        }

        private static Player ChoosePlayer(int playerChoice)
        {
            Player player = new Player();
            switch (playerChoice)
            {
                case 1:
                    player.PlayerType = new Turtle();
                    return player;
                case 2:
                    player.PlayerType = new Monkey();
                    return player;
                default:
                    player.PlayerType = new Pigeon();
                    return player;
            }
        }

        private static int ValidateUserInput(string userInput)
        {
            var list = new List<int> { 1, 2, 3 };

            int number;
            if (!Int32.TryParse(userInput, out number) || list.IndexOf(number) == -1)
            {
                Console.WriteLine("Player, please choose a valid warrior.");
            }
            return number;
        }

        private static IDictionary<Point, char> CreateMatrix(char[,] matrix, Player firstPlayer, Player secondPlayer, bool isFirstMatrix)
        {
            Random rng = new Random();
            Point player1Position = new Point(rng.Next(0, 7), rng.Next(0, 7));
            Point player2Position = new Point(rng.Next(0, 7), rng.Next(0, 7));

            while (!player1Position.ValidateDistance(player2Position, 3))
            {
                player2Position = new Point(rng.Next(0, 7), rng.Next(0, 7));
            }

            firstPlayer.Position = player1Position;
            secondPlayer.Position = player2Position;

            matrix[player1Position.X, player1Position.Y] = '1';
            matrix[player2Position.X, player2Position.Y] = '2';

            IDictionary<Point, char> fruits = new Dictionary<Point, char>();

            //List<Point> fruitsPoints = new List<Point>();

            if (isFirstMatrix)
            {
                for (int i = 0; i < 4; i++)
                {
                    Point apple = new Point(rng.Next(0, 7), rng.Next(0, 7));
                    while (player1Position.Equals(apple) || player2Position.Equals(apple) || fruits.Keys.Any(f => !f.ValidateDistance(apple, 2)))
                    {
                        apple = new Point(rng.Next(0, 7), rng.Next(0, 7));
                    }
                    matrix[apple.X, apple.Y] = 'A';
                    //fruitsPoints.Add(apple);
                    fruits.Add(apple, 'A');
                }

                for (int i = 0; i < 3; i++)
                {
                    Point pear = new Point(rng.Next(0, 7), rng.Next(0, 7));
                    while (player1Position.Equals(pear) || player2Position.Equals(pear) || fruits.Keys.Any(f => !f.ValidateDistance(pear, 2)))
                    {
                        pear = new Point(rng.Next(0, 7), rng.Next(0, 7));
                    }
                    matrix[pear.X, pear.Y] = 'P';
                    //fruitsPoints.Add(pear);
                    fruits.Add(pear, 'A');
                }
            }
            PrintMatrix(matrix);

            return fruits;
        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_COLUMNS; j++)
                {
                    if (matrix[i, j] == '\0')
                    {
                        matrix[i, j] = '-';
                    }
                    Console.Write($"{ matrix[i, j]} ");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        private static Directions ReadUserKeyMove(ConsoleKeyInfo userKey)
        {
            switch (userKey.Key)
            {
                case ConsoleKey.DownArrow:
                    return Directions.Down;

                case ConsoleKey.UpArrow:
                    return Directions.Up;

                case ConsoleKey.LeftArrow:
                    return Directions.Left;

                case ConsoleKey.RightArrow:
                    return Directions.Right;

                default:
                    Console.WriteLine("Select one key from left, right, up, down");
                    return Directions.Unknown;
            }
        }

        private static void Turn(char[,] matrix, Player firstPlayer, Player secondPlayer)
        {
            CreateMatrix(matrix, firstPlayer, secondPlayer, true);
            Console.WriteLine($"Player1 : { firstPlayer.PlayerType.Power} Power; { firstPlayer.PlayerType.Speed} Speed;");
            Console.WriteLine($"Player2 : { secondPlayer.PlayerType.Power} Power; { secondPlayer.PlayerType.Speed} Speed;");
        }
    }
}
