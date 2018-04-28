using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
    class Piece
    {
        private ePieceValue m_PieceValue;
        private dictPieces m_dictPieces;
        private string m_RowString;
        private string m_ColString;
        private int m_RowIndex;
        private int m_ColIndex;

        public Piece(int i_row, int i_col)
        {
            m_RowIndex = i_row;
            m_ColIndex = i_col;
            m_PieceValue = ePieceValue.Empty;
            m_dictPieces = new dictPieces();
            m_dictPieces.dictRow.TryGetValue(i_row, out m_RowString);
            m_dictPieces.dictCol.TryGetValue(i_col, out m_ColString);
        }

        public ePieceValue PieceValue
        {
            set
            {
                m_PieceValue = value;
            }
            get
            {
                return m_PieceValue;
            }
        }

        public int Col
        {
            get
            {
                return m_ColIndex;
            }
        }

        public int Row
        {
            get
            {
                return m_RowIndex;
            }
        }

        public string ColString
        {
            get
            {
                return m_ColString;
            }
        }

        public string RowString
        {
            get
            {
                return m_RowString;
            }
        }
    }
}
