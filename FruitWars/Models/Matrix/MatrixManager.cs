using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitWars.Models
{
    public class MatrixManager
    {
        #region Fields

        private Random _randomGenerator = new Random();

        #endregion

        #region Public Methods

        public char[,] CreateMatrix(Player currentPlayer, Player otherPlayer, bool newGame, char[,] matrix, bool hasWinner = false)
        {
            if (matrix == null && newGame)
            {
                matrix = new char[Constants.MATRIX_SIZE, Constants.MATRIX_SIZE];
            }

            SetGameObjectsPosition(currentPlayer, otherPlayer, newGame, matrix);

            PrintMatrix(matrix);

            if (!hasWinner)
            {
                if (currentPlayer.Charecater.Equals(Constants.FIRST_PLAYER))
                {
                    Console.WriteLine($"Player 1: { currentPlayer.PlayerType.Power} Power; { currentPlayer.PlayerType.Speed} Speed;");
                    Console.WriteLine($"Player 2: { otherPlayer.PlayerType.Power} Power; { otherPlayer.PlayerType.Speed} Speed;");
                }
                else
                {
                    Console.WriteLine($"Player 1: { otherPlayer.PlayerType.Power} Power; { otherPlayer.PlayerType.Speed} Speed;");
                    Console.WriteLine($"Player 2: { currentPlayer.PlayerType.Power} Power; { currentPlayer.PlayerType.Speed} Speed;");
                }
            }
            return matrix;
        }

        public void RefreshPlayersRemainingMoves(Player currentPlayer, Player otherPlayer)
        {
            currentPlayer.RefreshRemainingMoves();
            otherPlayer.RefreshRemainingMoves();
        }

        #endregion 

        #region Private Methods

        private void SetGameObjectsPosition(Player currentPlayer, Player otherPlayer, bool newGame, char[,] matrix)
        {
            Point currentPlayerPosition = null;
            Point otherPlayerPosition = null;

            List<Point> fruits = new List<Point>();

            if (newGame)
            {
                currentPlayerPosition = new Point(_randomGenerator.Next(0, 7), _randomGenerator.Next(0, 7));
                otherPlayerPosition = new Point(_randomGenerator.Next(0, 7), _randomGenerator.Next(0, 7));

                do
                {
                    otherPlayerPosition = new Point(_randomGenerator.Next(0, 7), _randomGenerator.Next(0, 7));
                } while (!currentPlayerPosition.ValidateDistance(otherPlayerPosition, Constants.PLAYER_DISTANCE));

                currentPlayer.Position = currentPlayerPosition;
                otherPlayer.Position = otherPlayerPosition;

                for (int i = 0; i < Constants.APPLES_COUNT; i++)
                {
                    Point apple = null;
                    do
                    {
                        apple = new Point(_randomGenerator.Next(0, 7), _randomGenerator.Next(0, 7));
                    } while (currentPlayerPosition.Equal(apple) || otherPlayerPosition.Equal(apple) || fruits.Any(f => !f.ValidateDistance(apple, Constants.FRUITS_DISTANCE)));

                    matrix[apple.X, apple.Y] = Constants.APPLE;
                    fruits.Add(apple);
                }

                for (int i = 0; i < Constants.PEARS_COUNT; i++)
                {
                    Point pear = null;
                    do
                    {
                        pear = new Point(_randomGenerator.Next(0, 7), _randomGenerator.Next(0, 7));
                    } while (currentPlayerPosition.Equal(pear) || otherPlayerPosition.Equal(pear) || fruits.Any(f => !f.ValidateDistance(pear, Constants.FRUITS_DISTANCE)));

                    matrix[pear.X, pear.Y] = Constants.PEAR;
                    fruits.Add(pear);
                }
            }
            else
            {
                currentPlayerPosition = currentPlayer.Position;
                otherPlayerPosition = otherPlayer.Position;
            }
 
            matrix[currentPlayerPosition.X, currentPlayerPosition.Y] = currentPlayer.Charecater == Constants.FIRST_PLAYER ?
                Convert.ToChar(Constants.FIRST_PLAYER) :
                Convert.ToChar(Constants.SECOND_PLAYER);

            matrix[otherPlayerPosition.X, otherPlayerPosition.Y] = otherPlayer.Charecater == Constants.FIRST_PLAYER ?
                Convert.ToChar(Constants.FIRST_PLAYER) :
                Convert.ToChar(Constants.SECOND_PLAYER);

        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < Constants.MATRIX_SIZE; i++)
            {
                for (int j = 0; j < Constants.MATRIX_SIZE; j++)
                {
                    if (matrix[i, j] == '\0')
                    {
                        matrix[i, j] = Constants.DEFAULT_CHARACTER;
                    }
                    Console.Write($"{ matrix[i, j]} ");
                }
                Console.Write(Environment.NewLine, Environment.NewLine);
            }
        }

        #endregion
    }
}
