using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
    class Piece
    {
        private ePieceValue m_PieceValue;
        //private ePieceRow m_PieceIndexRow;
        //private ePieceCol m_PieceIndexCol;
        private dictPieces m_dictPieces;
        private string m_RowString;
        private string m_ColString;
        private int m_RowIndex;
        private int m_ColIndex;


        //public Piece(string i_PieceIndexRow, string i_PieceIndexCol)
        //{
        //    m_PieceIndexRow = (ePieceRow)Enum.Parse(typeof(ePieceRow), i_PieceIndexRow);
        //    m_PieceIndexCol = (ePieceCol)Enum.Parse(typeof(ePieceCol), i_PieceIndexCol);
        //    m_PieceValue = ePieceValue.Empty; ////????????????
        //}

        public Piece(int i_row, int i_col)
        {
            m_RowIndex = i_row;
            m_RowIndex = i_col;
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
    }
}
