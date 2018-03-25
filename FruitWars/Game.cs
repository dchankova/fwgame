using FruitWars.Enums;
using FruitWars.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FruitWars
{
    class Game
    {
        static void Main()
        {
            bool newGame;
            do
            {
                newGame = false;
                
                //Create the players.
                Player firstPlayer = CreatePlayer(1);
                Player secondPlayer = CreatePlayer(2);
                Console.Write(Environment.NewLine, Environment.NewLine);

                //Create the matrix.
                MatrixManager matrixManager = new MatrixManager();
                char[,] matrix = matrixManager.CreateMatrix(firstPlayer, secondPlayer, newGame: true, matrix: null);

                //Start gameplay.
                //First player will be with index 0. Second player witll be with index 1.
                int currentPlayerIndex = 0;
                while (true)
                {
                    Move selectedMove;
                    do
                    {
                        Player currentPlayer = currentPlayerIndex == 0 ? firstPlayer : secondPlayer;
                        Player otherPlayer = currentPlayerIndex == 1 ? firstPlayer : secondPlayer;

                        do
                        {
                            selectedMove = MakeMove(currentPlayer, matrix);
                        } while (selectedMove == Move.InvalidDirection);

                        if (selectedMove == Move.Battle)
                        {
                            matrix = ReturnBattleResult(matrixManager, matrix, currentPlayer, otherPlayer);

                            if (ReadAnotherGameResponse() == Constants.NO_ANSWER)
                            {
                                return;
                            }
                            else
                            {
                                newGame = true;
                                break;
                            }
                        }

                        matrix = matrixManager.CreateMatrix(currentPlayer, otherPlayer, newGame: false, matrix: matrix);

                    } while (selectedMove == Move.MoreMoves);

                    if (newGame) break;

                    matrixManager.RefreshPlayersRemainingMoves(firstPlayer, secondPlayer);

                    if (selectedMove == Move.NoMoreMoves)
                    {
                        //Calculatate next player index;
                        currentPlayerIndex = (++currentPlayerIndex) % Constants.PLAYER_COUNT;
                    }
                }
            } while (newGame);
        }

        private static char[,] ReturnBattleResult(MatrixManager matrixManager, char[,] matrix, Player currentPlayer, Player otherPlayer)
        {
            int battleResult = currentPlayer.Battle(otherPlayer);

            if (battleResult > 0)
            {
                matrix = matrixManager.CreateMatrix(currentPlayer, currentPlayer, newGame: false, matrix: matrix, hasWinner: true);

                Console.WriteLine($"Player {currentPlayer.Charecater} wins the game.");
                Console.WriteLine($"{currentPlayer.PlayerType.GetType().Name} with Power: {currentPlayer.PlayerType.Power}, Speed: {currentPlayer.PlayerType.Speed}");
            }
            else if (battleResult == 0)
            {
                Console.WriteLine(Constants.DRAW_GAME_MSG);
            }
            else
            {
                matrix = matrixManager.CreateMatrix(otherPlayer, otherPlayer, newGame: false, matrix: matrix, hasWinner: true);

                Console.WriteLine($"Player {otherPlayer.Charecater} wins the game.");
                Console.WriteLine($"{otherPlayer.PlayerType.GetType().Name} with Power: {otherPlayer.PlayerType.Power}, Speed: {otherPlayer.PlayerType.Speed}");
            }

            return matrix;
        }

        private static Player CreatePlayer(int playerNumber)
        {
            int playerChoice = -1;
            do
            {
                Console.WriteLine($"Player {playerNumber}, please choose a warrior");
                Console.WriteLine(Constants.CHOOSE_WARRIOR_MSG);
                string userInput = Console.ReadLine();
                playerChoice = ValidateWarriorChoice(userInput);
            } while (playerChoice == -1);

            return ChoosePlayer(playerNumber, playerChoice);
        }

        private static Player ChoosePlayer(int playerNumber, int playerChoice)
        {
            Player player = new Player(playerNumber.ToString());
            switch (playerChoice)
            {
                case 1:
                    player.PlayerType = new Turtle();
                    break;
                case 2:
                    player.PlayerType = new Monkey();
                    break;
                default:
                    player.PlayerType = new Pigeon();
                    break;
            }
            player.RemainingMoves = player.PlayerType.Speed;
            return player;
        }

        private static int ValidateWarriorChoice(string userInput)
        {
            var list = new List<int> { 1, 2, 3 };
            int number = Int32.TryParse(userInput, out number) && list.IndexOf(number) != -1 ? number : -1;
            return number;
        }

        private static Move MakeMove(Player player, char[,] matrix)
        {
            Console.WriteLine($"Player {player.Charecater}, make a move please!");
            Console.Write(Environment.NewLine);

            int positionX = player.Position.X;
            int positionY = player.Position.Y;

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    positionY = player.Position.Y - 1;
                    break;
                case ConsoleKey.RightArrow:
                    positionY = player.Position.Y + 1;
                    break;
                case ConsoleKey.UpArrow:
                    positionX = player.Position.X - 1;
                    break;
                case ConsoleKey.DownArrow:
                    positionX = player.Position.X + 1;
                    break;
                default:
                    Console.WriteLine(Constants.VALIDATION_KEY_INPUT_MSG);
                    return Move.InvalidDirection;
            }

            if (OutsideMatrix(positionX) || OutsideMatrix(positionY))
            {
                return Move.InvalidDirection;
            }

            matrix[player.Position.X, player.Position.Y] = Constants.EMPTY_CELL;

            player.RemainingMoves -= 1;

            object cellObject = FindCellObject(matrix[positionX, positionY]);

            Enum fruit = cellObject as Enum;

            if (cellObject == null || fruit != null)
            {
                if (fruit != null)
                {
                    player.PlayerType.Eat((Fruit)fruit);
                }
                player.Position.X = positionX;
                player.Position.Y = positionY;

                matrix[player.Position.X, player.Position.Y] = Convert.ToChar(player.Charecater);

                return player.RemainingMoves > 0 ? Move.MoreMoves : Move.NoMoreMoves;
            }

            player.Position.X = positionX;
            player.Position.Y = positionY;

            return Move.Battle;
        }

        private static string ReadAnotherGameResponse()
        {
            string anotherGameResponse;
            do
            {
                Console.WriteLine(Constants.START_NEW_GAME_MSG);
                anotherGameResponse = Console.ReadLine();

            } while (!Regex.IsMatch(anotherGameResponse, Constants.YES_NO_REGEX));
            return anotherGameResponse;
        }

        private static bool OutsideMatrix(int delta)
        {
            return delta < 0 || 7 < delta;
        }

        private static object FindCellObject(char gameCharacter)
        {
            switch (gameCharacter)
            {
                case 'A':
                    return Fruit.Apple;
                case 'P':
                    return Fruit.Pear;
                case '1':
                    return Constants.FIRST_PLAYER;
                case '2':
                    return Constants.SECOND_PLAYER;
                default:
                    return null;
            }
        }
    }
}
