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
        private Player m_currentPlayer = null;

        //Methods:
        public CheckerLogicalControl(Player i_FirstPlayer, Player i_SecondPlayer, int i_GameBoardSize, ePlayerEnemy i_Enemy)
        {
            m_FirstPlayer = new Player(i_FirstPlayer.Name, ePieceValue.X);
            m_SecondPlayer = new Player(i_SecondPlayer.Name, ePieceValue.O);
            m_PlayerEnemy = i_Enemy;
            m_GameBoard = new GameBoard(i_GameBoardSize);
        }

        private bool isValidMove()
        {
            // TODO
            return true;
        }

        public void Move(String i_From, String i_To)
        {
            //TODO
        }

        public Player CurrentPlayer 
        {
            set 
            {
                m_currentPlayer = value;
            }  
            get
            {
                return m_currentPlayer;
            }
        }

        public List<Piece> GetMustToAttack(ePieceValue i_PieceValue)
        {
            Piece[,] currentBoard;
            List<Piece> mustToAttack = null;

            currentBoard = m_GameBoard.GameBoardPieces;

            foreach(Piece obj in currentBoard)
            {
                if( obj.PieceValue == i_PieceValue)
                {
                    //TODO;
                }
            }

            return mustToAttack;
        }
    }
}
