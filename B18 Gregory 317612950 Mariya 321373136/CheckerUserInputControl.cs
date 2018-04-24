using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
	class CheckerUserInputControl
	{
		////Members:
		private CheckerLogicalControl m_LogicalControl;
		private const string k_Quit = "Q";
        Player firstPlayer, secondPlayer;

        ////Methods:
        public CheckerUserInputControl()
		{
			welcomeMessage();
			defineGameSettings();
			startToPlay();
		}

		private void welcomeMessage()
		{
			string msg = string.Format(
@"Welcome to CHECKER GAME!
Checker is a strategy board games for two players
which involve diagonal moves of X / O game pieces
and mandatory captures by jumping over opponent pieces.
GOOD LUCK!"
				);
			Console.WriteLine(msg);
			Console.WriteLine();
		}

		private void defineGameSettings()
		{
			//Player firstPlayer, secondPlayer;
			int gameBoardSize = getGameBoardSizeInput();

			ePlayerEnemy enemy = getPlayerChoiceEnemy();
			getPlayerInput(out firstPlayer, out secondPlayer, enemy);

			m_LogicalControl = new CheckerLogicalControl(firstPlayer, secondPlayer, gameBoardSize, enemy);
        }

		private void getPlayerInput(out Player o_FisrtPlayer, out Player o_SecondPlayer, ePlayerEnemy i_Enemy)
		{
			string playerName;
			Console.WriteLine("Please enter your Name:");
			playerName =  Console.ReadLine();

			o_FisrtPlayer = new Player(playerName, ePieceValue.O);

			if (i_Enemy.Equals(ePlayerEnemy.AgainstAnotherPlayer))
			{
				Console.WriteLine("Enter second player name:");
				playerName = Console.ReadLine();
			}
			else
			{
				playerName = "Computer";
			}

			o_SecondPlayer = new Player(playerName, ePieceValue.X);
        }

		private int getGameBoardSizeInput()
		{
			bool validInput = false;
			string stringBoardSizeInput;
			int boardSize;

			do
			{
				Console.WriteLine("Please enter game board size (6x6, 8x8, 10x10):");
				stringBoardSizeInput = Console.ReadLine();
				validInput = int.TryParse(stringBoardSizeInput, out boardSize);

				if (!validInput)
				{
					Console.WriteLine("Invalid size, please try again.");
					validInput = false;
                }
				if (boardSize != 6 && boardSize != 8 && boardSize != 10)
				{
					Console.WriteLine("Please select only: 6 or 8 or 10 size.");
					validInput = false;
                }
			}
			while (!validInput);

            return boardSize;
		}

		private ePlayerEnemy getPlayerChoiceEnemy()
		{
			string msg;
			string playerChoicenEnemy;
			int playerEnemy;
			bool validChoiceEnemy = false;
			ePlayerEnemy choicenEnemy;

			msg = string.Format(
@"Please select your enemy:
Against Computer: enter 1.
Against Human: enter 2."
				);
			do
			{
				Console.WriteLine(msg);
				playerChoicenEnemy = Console.ReadLine();
				validChoiceEnemy = int.TryParse(playerChoicenEnemy, out playerEnemy);
				choicenEnemy = (ePlayerEnemy)Enum.Parse(typeof(ePlayerEnemy), playerChoicenEnemy);
				if (choicenEnemy != ePlayerEnemy.AgainstAnotherPlayer && choicenEnemy != ePlayerEnemy.AgainstTheComputer)
				{
					Console.WriteLine("Invalid input, try again.");
					validChoiceEnemy = false;
                }
			}
			while (!validChoiceEnemy);

			return choicenEnemy;
		}


		private void startToPlay()
		{

            string playerTurn;
            string fromMove;
            string toMove;

            Console.WriteLine("{0} turn:", firstPlayer.Name);
            playerTurn = Console.ReadLine();

            fromMove = playerTurn.Substring(0, 2);
            toMove = playerTurn.Substring(3, 2);

            m_LogicalControl.Move(fromMove, toMove);

        }
	}
}
