using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
	class GameBoard
	{
		////Members:
		private int m_GameBoardSize;
		private Piece[,] m_GameBoard;
        private ePieceValue m_PieceValue;
        private dictPieces m_dictPieces;

        ////Methods:
        public GameBoard(int i_BoardSize)
		{
			m_GameBoardSize = i_BoardSize;
            m_dictPieces = new dictPieces();
			InitializeNewGameBoard();

        }

        /**
         * BoardSize  
         **/
		public int BoardSize
		{
			get { return m_GameBoardSize; }
		}

        private void initBoardPiecePosition()
        {
            for (int i = 0; i < m_GameBoardSize; i++)
            {
                for (int j = 0; j < m_GameBoardSize; j++)
                {
                    m_GameBoard[i, j] = new Piece(i, j);
                }
            }
        }

        private void initPlayerPiecesPosition(int i_StartRow, int i_EndRow, int i_BoardSize, ePieceValue i_PieceValue)
        {

            for (int i = i_StartRow; i < i_EndRow; i++)
            {
                if ((i % 2) == 0)
                {
                    for (int j = 1; j < i_BoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = i_PieceValue;
                    }
                }
                else
                {
                    for (int j = 0; j < i_BoardSize; j += 2)
                    {
                        m_GameBoard[i, j].PieceValue = i_PieceValue;
                    }
                }
            }
        }

		public void InitializeNewGameBoard()
		{
            m_GameBoard = new Piece[m_GameBoardSize, m_GameBoardSize];

            initBoardPiecePosition();

            ////First player's pieces postion - bottom player
            initPlayerPiecesPosition(m_GameBoardSize / 2 + 1, m_GameBoardSize, m_GameBoardSize, ePieceValue.O);

            ////Second player's pieces postion - top player
            initPlayerPiecesPosition(0, m_GameBoardSize / 2 - 1, m_GameBoardSize, ePieceValue.X);

            DrawBoard();
        }

		public void DrawBoard()
		{
			string columnBoardValue;
			string rowBoardValue;

			string boardred = new string('=', 5 * BoardSize + BoardSize + 1);

			for (int i = 0; i < BoardSize; i++)
			{
                m_dictPieces.dictCol.TryGetValue(i, out columnBoardValue);
				Console.Write("    " + columnBoardValue + " ");
			}

			Console.WriteLine();
			Console.WriteLine(boardred);

			for (int i = 0; i < BoardSize; i++)
			{
                m_dictPieces.dictRow.TryGetValue(i, out rowBoardValue);
                Console.Write(rowBoardValue + "|");

				for (int j = 0; j < BoardSize; j++)
				{
					Console.Write(m_GameBoard[i, j].PieceValue == ePieceValue.Empty ? "     " + "|" : "  " + m_GameBoard[i, j].PieceValue.ToString() + "  |");
				}

				Console.WriteLine("\n" + boardred);
			}
		}

        public Piece GetGameBoardPiece(string i_Coordinate)
        {
            Piece returnPiece = null;

            if (i_Coordinate.Length == 2)
            {
                int row = m_dictPieces.GetKeyByValue(i_Coordinate[0].ToString(), m_dictPieces.dictRow);
                int col = m_dictPieces.GetKeyByValue(i_Coordinate[1].ToString(), m_dictPieces.dictCol);

                if (row >= 0 && row < BoardSize && col >= 0 && col < BoardSize)
                {
                    returnPiece = m_GameBoard[row, col];
                }
            }

            return returnPiece;
        }

        public Piece[,] GameBoardPieces
        {
            get
            {
                return m_GameBoard;
            }
            set
            {
                m_GameBoard = value;
            }
        }
	}
}
