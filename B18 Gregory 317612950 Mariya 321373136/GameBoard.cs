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
        
            //enemy
            for(int i = 0; i < m_GameBoardSize/2 -1; i++)
            {
                if( i % 2 == 0 )
                {
                    for( int j = 1; j < m_GameBoardSize; j += 2 )
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

            //first player
            for (int i = m_GameBoardSize - 1; i > m_GameBoardSize / 2; i--)
            {
                if (i % 2 == 0)
                {
                    for (int j = 1; j < m_GameBoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = ePieceValue.O;
                    }
                }
                else
                {
                    for (int j = 0; j < m_GameBoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = ePieceValue.O;
                    }
                }
            }
			drawBoard();
        }

		private void drawBoard()
		{
			string columnBoardValue;
			string rowBoardValue;
			dictPieces dict = new dictPieces();

			string boardred = new string('=', 5 * BoardSize + BoardSize + 1);
			for (int i = 0; i < BoardSize; i++)
			{
				dict.dictCol.TryGetValue(i, out columnBoardValue);
				Console.Write("    " + columnBoardValue + " ");
			}
			Console.WriteLine();
			Console.WriteLine(boardred);

			for (int i = 0; i < BoardSize; i++)
			{
				dict.dictRow.TryGetValue(i, out rowBoardValue);
                Console.Write(rowBoardValue + "|");

				for (int j = 0; j < BoardSize; j++)
				{
					Console.Write(m_GameBoard[i, j].PieceValue == ePieceValue.Empty ? "     " + "|" : "  " + m_GameBoard[i, j].PieceValue.ToString() + "  |");
				}

				Console.WriteLine("\n" + boardred);
			}
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
