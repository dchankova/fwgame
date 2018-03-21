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

            CreateMatrix(matrix);
            Console.WriteLine($"Player1 : { firstPlayer.PlayerType.Power} Power; { firstPlayer.PlayerType.Speed} Speed;");
            Console.WriteLine($"Player2 : { secondPlayer.PlayerType.Power} Power; { secondPlayer.PlayerType.Speed} Speed;");
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

        private static void CreateMatrix(char[,] matrix)
        {
            Random rng = new Random();
            Point player1 = new Point(rng.Next(0, 7), rng.Next(0, 7));
            Point player2 = new Point(rng.Next(0, 7), rng.Next(0, 7));

            while (!player1.ValidateDistance(player2, 3))
            {
                player2 = new Point(rng.Next(0, 7), rng.Next(0, 7));
            }

            matrix[player1.X, player1.Y] = '1';
            matrix[player2.X, player2.Y] = '2';

            List<Point> fruitsPoints = new List<Point>();

            for (int i = 0; i < 4; i++)
            {
                Point apple = new Point(rng.Next(0, 7), rng.Next(0, 7));
                while (player1.Equals(apple) || player2.Equals(apple) || fruitsPoints.Any(f => !f.ValidateDistance(apple, 2)))
                {
                    apple = new Point(rng.Next(0, 7), rng.Next(0, 7));
                }
                matrix[apple.X, apple.Y] = 'A';
                fruitsPoints.Add(apple);
            }

            for (int i = 0; i < 3; i++)
            {
                Point pear = new Point(rng.Next(0, 7), rng.Next(0, 7));
                while (player1.Equals(pear) || player2.Equals(pear) || fruitsPoints.Any(f => !f.ValidateDistance(pear, 2)))
                {
                    pear = new Point(rng.Next(0, 7), rng.Next(0, 7));
                }
                matrix[pear.X, pear.Y] = 'P';
                fruitsPoints.Add(pear);
            }
            PrintMatrix(matrix);
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
    }
}
