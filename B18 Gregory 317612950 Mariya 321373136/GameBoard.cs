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
        private ePieceValue m_PieceValue;

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

        private void initiateBoardPieces()
        {
            for (int i = 0; i < m_GameBoardSize; i++)
            {
                for (int j = 0; j < m_GameBoardSize; j++)
                {
                    m_GameBoard[i, j] = new Piece(i, j);
                }
            }
        }

        private void initiateFirstPlayerPieces()
        {
            //enemy
            for (int i = 0; i < m_GameBoardSize / 2; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 1; j < m_GameBoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = ePieceValue.X;
                    }
                }
                else
                {
                    for (int j = 0; j < m_GameBoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = ePieceValue.X;
                    }
                }
            }
        }

        private void initiateSecondPlayerPieces()
        {
            //first player
            for (int i = m_GameBoardSize - 1; i > m_GameBoardSize / 2; i--)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < m_GameBoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = ePieceValue.O;
                    }
                }
                else
                {
                    for (int j = 1; j < m_GameBoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = ePieceValue.O;
                    }
                }
            }
        }

        public void InitializeNewGameBoard()
		{
			m_GameBoard = new Piece[m_GameBoardSize, m_GameBoardSize];

            initiateBoardPieces();
            initiateFirstPlayerPieces();
            initiateSecondPlayerPieces();

        }

        public Piece[,] GameBoardPieces
        {
            get
            {
                return m_GameBoard;
            }
        }
	}
}
