using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
	class Player
	{
		////Members:
		private string m_PlayerName; 
		private int m_score;
		private ePieceValue m_Piece; ////????????????

		////Methods:
		public Player(string i_PlayerName, ePieceValue i_Piece)
		{
			m_PlayerName = i_PlayerName;
			m_Piece = i_Piece;
			m_score = 0;
		}

		public string Name
		{
			get { return m_PlayerName; }
			set { m_PlayerName = value; }
		}

		public int Score
		{
			get { return m_score; }
			set { m_score++; }
		}

		public ePieceValue PieceValue
        {
			get { return m_Piece; }
			set { m_Piece = value; }
		}
	}
}
