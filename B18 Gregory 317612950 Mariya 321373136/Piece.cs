using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
	class Piece
	{
		private ePieceValue m_PieceValue;
		private int m_PieceIndexRow;
		private int m_pieceIndexCol;



		public Piece(int i_PieceIndexRow, int i_PieceIndexCol)
		{
			m_PieceIndexRow = i_PieceIndexRow;
			m_pieceIndexCol = i_PieceIndexCol;
			m_PieceValue = ePieceValue.Empty; ////????????????
		}
	}
}
