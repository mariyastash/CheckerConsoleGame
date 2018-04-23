using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
	class GameBoard
	{
		//Members:
		private int m_GameBoardSize;
		private Piece[,] m_GameBoard;

		//Methods:
		public GameBoard(int i_BoardSize)
		{
			m_GameBoardSize = i_BoardSize;
			InitializeNewGameBoard();
		}

		public int BoardSize
		{
			get { return m_GameBoardSize; }
		}

		public void InitializeNewGameBoard()
		{
			m_GameBoard = new Piece[m_GameBoardSize, m_GameBoardSize];

			for (int i = 0; i < m_GameBoardSize; i++)
			{
				for (int j = 0; j < m_GameBoardSize; j++)
				{
					m_GameBoard[i, j] = new Piece(i, j);
				}
			}

		}
	}
}
