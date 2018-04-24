using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
    class CheckerLogicalControl
    {
        ////Members:
        private GameBoard m_GameBoard;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private ePlayerEnemy m_ChoosenEnemy;
        private Player m_CurrentPlayer = null;
        private Player m_EnemyPlayer = null;
        private Piece[,] m_CurrentBoard;
        private List<Piece> m_MustToAttackList;

        ////Methods:
        public CheckerLogicalControl(Player i_FirstPlayer, Player i_SecondPlayer, int i_GameBoardSize, ePlayerEnemy i_ChoosenEnemy)
        {

            m_FirstPlayer = new Player(i_FirstPlayer.Name, ePieceValue.O);
            m_SecondPlayer = new Player(i_SecondPlayer.Name, ePieceValue.X);

            m_ChoosenEnemy = i_ChoosenEnemy; // Computer or Human

            m_CurrentPlayer = m_FirstPlayer;
            m_EnemyPlayer = m_SecondPlayer;

            m_GameBoard = new GameBoard(i_GameBoardSize);
            m_CurrentBoard = m_GameBoard.GameBoardPieces;
        }

        private bool isValidMove(Piece i_From, Piece i_To)
        {
            bool returnValue = false;
            bool isCurrentPlayerPiece = m_CurrentPlayer.PieceValue == i_From.PieceValue;
            bool isAttack = m_MustToAttackList.Contains(i_From);
            bool isToCoordinateEmpty = i_To.PieceValue.Equals(ePieceValue.Empty);

            if (m_MustToAttackList.Count > 0)
            {
                if(isAttack)
                {
                    if (m_CurrentPlayer.Equals(m_FirstPlayer))
                    {
                        returnValue = i_To.Row == i_From.Row - 2 && (i_To.Col == i_From.Col + 2 || i_To.Col == i_From.Col - 2);
                    }
                }
            }

            if(isCurrentPlayerPiece && isToCoordinateEmpty)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public void Move(String i_From, String i_To)
        {
            Piece fromPiece = m_GameBoard.GetGameBoardPiece(i_From);
            Piece toPiece = m_GameBoard.GetGameBoardPiece(i_To);

            setMustToAttackList();

            if (isValidMove(fromPiece, toPiece))
            {
                int fromRow = fromPiece.Row;
                int fromCol = fromPiece.Col;
                int toRow = toPiece.Row;
                int toCol = toPiece.Col;

                m_CurrentBoard[toRow, toCol].PieceValue = fromPiece.PieceValue;
                m_CurrentBoard[fromRow, fromCol].PieceValue = ePieceValue.Empty;

                m_GameBoard.GameBoardPieces = m_CurrentBoard;

                m_GameBoard.DrawBoard();
            }
            else {
              //TODO Handle ERROR
            }
            ////TODO
        }

        public Player CurrentPlayer 
        {
            set 
            {
                m_CurrentPlayer = value;
            }  
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player EnemyPlayer
        {
            set
            {
                m_EnemyPlayer = value;
            }
            get
            {
                return m_EnemyPlayer;
            }
        }

        private void setMustToAttackList()
        {
            // TODO REFACTRORING
            m_MustToAttackList = new List<Piece>();

            foreach(Piece obj in m_CurrentBoard)
            {
                // check if piece value belongs to current player
                if ( obj.PieceValue == m_CurrentPlayer.PieceValue)
                {
                    // check if current player is a bottom player 
                    if (m_CurrentPlayer.Equals(m_FirstPlayer))
                    {
                      
                        bool topLeftIsEnemy = isPieceValue(obj.Row - 1, obj.Col - 1, m_EnemyPlayer.PieceValue);
                        bool topTopLeftIsEmpty = isPieceValue(obj.Row - 2, obj.Col - 2, ePieceValue.Empty);
                        bool topRightIsEnemy = isPieceValue(obj.Row - 1, obj.Col + 1, m_EnemyPlayer.PieceValue);
                        bool topTopRightIsEmpty = isPieceValue(obj.Row - 2, obj.Col + 2, ePieceValue.Empty);

                        if (topLeftIsEnemy && topTopLeftIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                        else if (topRightIsEnemy && topTopRightIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                    }
                    // current player is top player
                    else
                    {

                        bool bottomLeftIsEnemy = isPieceValue(obj.Row + 1, obj.Col - 1, m_EnemyPlayer.PieceValue);
                        bool bottomBottomLeftIsEmpty = isPieceValue(obj.Row + 2, obj.Col - 2, ePieceValue.Empty);
                        bool bottomRightIsEnemy = isPieceValue(obj.Row + 1, obj.Col + 1, m_EnemyPlayer.PieceValue);
                        bool bottomBottompRightIsEmpty = isPieceValue(obj.Row + 2, obj.Col + 2, ePieceValue.Empty);

                        if (bottomLeftIsEnemy && bottomBottomLeftIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                        else if (bottomRightIsEnemy && bottomBottompRightIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }

                    }
                    ////TODO check queen;
                }
            }

        }

        private bool isPieceValue(int i_Row, int i_Col, ePieceValue i_PieceValue)
        {
            bool returnValue = false;

            if (i_Row >= 0 && i_Col > 0 && i_Row < m_GameBoard.BoardSize && i_Col < m_GameBoard.BoardSize)
            {
                if (m_CurrentBoard[i_Row, i_Col].PieceValue.Equals(i_PieceValue))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
    }
}
