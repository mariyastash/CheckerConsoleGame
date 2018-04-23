using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
	class CheckerLogicalControl
	{
		//Members:
		private GameBoard m_GameBoard;
		private Player m_FirstPlayer;
		private Player m_SecondPlayer;
		private ePlayerEnemy m_PlayerEnemy;
		//private Player m_currentPlayer = m_FirstPlayer;

		//Methods:
		public CheckerLogicalControl(Player i_FirstPlayer, Player i_SecondPlayer,int i_GameBoardSize, ePlayerEnemy i_Enemy)
		{
			m_FirstPlayer = new Player(i_FirstPlayer.Name, ePieceValue.X);
			m_SecondPlayer = new Player(i_SecondPlayer.Name, ePieceValue.O);
			m_PlayerEnemy = i_Enemy;
			m_GameBoard = new GameBoard(i_GameBoardSize);
		}
	}
}
