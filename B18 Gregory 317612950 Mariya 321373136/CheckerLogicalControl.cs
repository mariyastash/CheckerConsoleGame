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
        private bool m_GameOver = false;
        private List<Piece> m_MustToAttackList;

        ////Methods:
        public CheckerLogicalControl(Player i_FirstPlayer, Player i_SecondPlayer, int i_GameBoardSize, ePlayerEnemy i_ChoosenEnemy)
        {

            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;

            m_ChoosenEnemy = i_ChoosenEnemy; // Computer or Human

            m_CurrentPlayer = m_FirstPlayer;
            m_EnemyPlayer = m_SecondPlayer;

            m_GameBoard = new GameBoard(i_GameBoardSize);
            m_CurrentBoard = m_GameBoard.GameBoardPieces;
        }

        private bool isValidMove(Piece i_From, Piece i_To)
        {
            bool returnValue = true;
            bool isCurrentPlayerPiece = m_CurrentPlayer.IsPlayerPiece(i_From);
            bool isAttack = m_MustToAttackList.Contains(i_From);
            bool isToCoordinateEmpty = i_To.PieceValue.Equals(ePieceValue.Empty);

            if (isCurrentPlayerPiece && isToCoordinateEmpty)
            {
                if (m_MustToAttackList.Count > 0)
                {
                    if (isAttack)
                    {
                        Piece underAttack = getUnderAttackPiece(i_From, i_To);
                        returnValue = isValidJumpByStep(i_To, i_From, 2) && m_EnemyPlayer.IsPlayerPiece(underAttack);//underAttack.PieceValue.Equals(m_EnemyPlayer.PieceValue);
                    }
                    else
                    {
                        returnValue = !returnValue;
                    };
                }
                else
                {
                    returnValue = isValidJumpByStep(i_To, i_From, 1);
                }

            }
            else
            {
                returnValue = !returnValue;
            }

            return returnValue;
        }

        private bool isValidJumpByStep(Piece i_To, Piece i_From, int steps)
        {
            bool returnValue = true;
            bool isColValid = i_To.Col == i_From.Col + steps || i_To.Col == i_From.Col - steps;
            bool isQueen = m_CurrentPlayer.QueenPieceValue.Equals(i_From.PieceValue);

            if (isColValid)
            {
                if(isQueen)
                {
                    returnValue = i_To.Row == i_From.Row - steps || i_To.Row == i_From.Row + steps;
                }
                ////bottom player
                else if (m_CurrentPlayer.Equals(m_FirstPlayer))
                {
                    returnValue = i_To.Row == i_From.Row - steps;
                }
                ////top player
                else if (m_CurrentPlayer.Equals(m_SecondPlayer))
                {
                    returnValue = i_To.Row == i_From.Row + steps;
                }

            }
            else
            {
                returnValue = !returnValue;
            }
            return returnValue;
        }

        public void Move(String i_From, String i_To)
        {
            Piece fromPiece = m_GameBoard.GetGameBoardPiece(i_From);
            Piece toPiece = m_GameBoard.GetGameBoardPiece(i_To);
            bool attacked = false;
            string errorMsg = null;

			setMustToAttackList();

            if (isValidMove(fromPiece, toPiece))
            {
                int fromRow = fromPiece.Row;
                int fromCol = fromPiece.Col;
                int toRow = toPiece.Row;
                int toCol = toPiece.Col;
                

                if (m_MustToAttackList.Count > 0)
                {
                    Piece pieceUnderAttack = getUnderAttackPiece(fromPiece, toPiece);
                    m_CurrentBoard[pieceUnderAttack.Row, pieceUnderAttack.Col].PieceValue = ePieceValue.Empty;
                    attacked = true;
                }

                //Queens
                if (toRow == m_GameBoard.BoardSize-1 && fromPiece.PieceValue.Equals(ePieceValue.X))
                {
                    m_CurrentBoard[toRow, toCol].PieceValue = ePieceValue.U;
                }
                else if(toRow == 0 && fromPiece.PieceValue.Equals(ePieceValue.O))
                {
                    m_CurrentBoard[toRow, toCol].PieceValue = ePieceValue.K;
                }
                else
                {
                    m_CurrentBoard[toRow, toCol].PieceValue = fromPiece.PieceValue;
                }

                m_CurrentBoard[fromRow, fromCol].PieceValue = ePieceValue.Empty;

                setMustToAttackList();
                m_GameBoard.GameBoardPieces = m_CurrentBoard;

                if (!isGameOver())
                {
                    if (attacked)
                    {
                        if (m_MustToAttackList.Count == 0)
                        {
                            SwitchPlayer();
                        }
                    }
                    else
                    {
                        SwitchPlayer();
                    }
                }
                
            }
            else {
                errorMsg = "Invalid Move!";
            }

            m_GameBoard.DrawBoard();

            if (errorMsg != null)
            {
                throw new Exception(errorMsg);
            }
        }

        public void AutomaticMove()
        {
            Piece fromPiece = null;
            Piece toPiece = null;

            setMustToAttackList();

            if (m_MustToAttackList.Count > 0)
            {
                fromPiece = m_MustToAttackList[0];
            }
            else
            {
                fromPiece = getMoveFromPiece();
            }

            if (fromPiece != null)
            {
                toPiece = getToPiece(fromPiece);
                Move(fromPiece.RowString + fromPiece.ColString, toPiece.RowString + toPiece.ColString);
            }
        }

        private Piece getToPiece(Piece i_From)
        {
            Piece returnPiece = null;
            bool isAttack = m_MustToAttackList.Contains(i_From);
            bool isQueen = i_From.PieceValue.Equals(m_CurrentPlayer.QueenPieceValue);

            bool topLeftIsEnemy = isEnemyPiece(i_From.Row - 1, i_From.Col - 1);
            bool topTopLeftIsEmpty = isPieceValue(i_From.Row - 2, i_From.Col - 2, ePieceValue.Empty);
            bool topRightIsEnemy = isEnemyPiece(i_From.Row - 1, i_From.Col + 1);
            bool topTopRightIsEmpty = isPieceValue(i_From.Row - 2, i_From.Col + 2, ePieceValue.Empty);
            bool bottomLeftIsEnemy = isEnemyPiece(i_From.Row + 1, i_From.Col - 1);
            bool bottomBottomLeftIsEmpty = isPieceValue(i_From.Row + 2, i_From.Col - 2, ePieceValue.Empty);
            bool bottomRightIsEnemy = isEnemyPiece(i_From.Row + 1, i_From.Col + 1);
            bool bottomBottompRightIsEmpty = isPieceValue(i_From.Row + 2, i_From.Col + 2, ePieceValue.Empty);


            if(m_CurrentPlayer.Equals(m_SecondPlayer))
            {
                if (isAttack)
                {
                    if (bottomLeftIsEnemy && bottomBottomLeftIsEmpty)
                    {
                        returnPiece = m_CurrentBoard[i_From.Row + 2, i_From.Col - 2];
                    }
                    else if (bottomRightIsEnemy && bottomBottompRightIsEmpty)
                    {
                        returnPiece = m_CurrentBoard[i_From.Row + 2, i_From.Col + 2];
                    }
                    else if(isQueen)
                    {
                        if (topLeftIsEnemy && topTopLeftIsEmpty)
                        {
                            returnPiece = m_CurrentBoard[i_From.Row - 2, i_From.Col - 2];
                        }
                        else if (topRightIsEnemy && topTopRightIsEmpty)
                        {
                            returnPiece = m_CurrentBoard[i_From.Row - 2, i_From.Col + 2];
                        }
                    }
                }
                else
                {
                    if (isPieceValue(i_From.Row + 1, i_From.Col + 1, ePieceValue.Empty))
                    {
                        returnPiece = m_CurrentBoard[i_From.Row + 1, i_From.Col + 1];
                    }
                    else if (isPieceValue(i_From.Row + 1, i_From.Col - 1, ePieceValue.Empty))
                    {
                        returnPiece = m_CurrentBoard[i_From.Row + 1, i_From.Col - 1];
                    }
                    else if(isQueen)
                    {
                        if (isPieceValue(i_From.Row - 1, i_From.Col - 1, ePieceValue.Empty))
                        {
                            returnPiece = m_CurrentBoard[i_From.Row - 1, i_From.Col - 1];
                        }
                        else if (isPieceValue(i_From.Row - 1, i_From.Col + 1, ePieceValue.Empty))
                        {
                            returnPiece = m_CurrentBoard[i_From.Row - 1, i_From.Col + 1];
                        }
                    }
                }
            }
            return returnPiece;
        }

        private Piece getMoveFromPiece()
        {
            Piece returnPiece = null;

            foreach (Piece obj in m_CurrentBoard)
            {
                if (m_CurrentPlayer.IsPlayerPiece(obj))
                {
                    if (m_CurrentPlayer.Equals(m_SecondPlayer))
                    {
                        if (isPieceValue(obj.Row + 1, obj.Col + 1, ePieceValue.Empty) || isPieceValue(obj.Row + 1, obj.Col - 1, ePieceValue.Empty))
                        {
                            returnPiece = obj;
                        }
                        else if(m_CurrentPlayer.QueenPieceValue.Equals(obj.PieceValue))
                        {
                            if (isPieceValue(obj.Row - 1, obj.Col - 1, ePieceValue.Empty) || isPieceValue(obj.Row - 1, obj.Col + 1, ePieceValue.Empty))
                            {
                                returnPiece = obj;
                            }
                        }
                    }
                    else
                    {
                        if (isPieceValue(obj.Row - 1, obj.Col + 1, ePieceValue.Empty) || isPieceValue(obj.Row - 1, obj.Col - 1, ePieceValue.Empty))
                        {
                            returnPiece = obj;
                        }
                        else if (m_CurrentPlayer.QueenPieceValue.Equals(obj.PieceValue))
                        {
                            if (isPieceValue(obj.Row - 1, obj.Col - 1, ePieceValue.Empty) || isPieceValue(obj.Row - 1, obj.Col + 1, ePieceValue.Empty))
                            {
                                returnPiece = obj;
                            }
                        }
                    }
                }
            }

            return returnPiece;
        }

        public ePlayerEnemy ChoosenEnimy
        {
            get
            {
                return m_ChoosenEnemy;
            }
        }

        private bool isGameOver()
        {
            bool v_IsGameOver = true;
            foreach (Piece obj in m_CurrentBoard)
            {
                if (EnemyPlayer.IsPlayerPiece(obj))
                {
                    v_IsGameOver = false;
                }
            }

            if(v_IsGameOver)
            {
                m_GameOver = v_IsGameOver;
                m_CurrentPlayer.Score++;
            }
            return v_IsGameOver;
        }

        public bool GameOver
        {
            get
            {
				return m_GameOver;
            }
        }

        public void SwitchPlayer()
        {
            Player tempPlayer = m_CurrentPlayer;
            m_CurrentPlayer = m_EnemyPlayer;
            m_EnemyPlayer = tempPlayer; 
        }

        private Piece getUnderAttackPiece(Piece i_FromPiece, Piece i_ToPiece)
        {
            Piece returnPiece;

            if (i_ToPiece.Col > i_FromPiece.Col)
            {
				if (i_ToPiece.Row > i_FromPiece.Row)
				{
					returnPiece = m_CurrentBoard[i_FromPiece.Row + 1, i_FromPiece.Col + 1];
				}
				else
				{
					returnPiece = m_CurrentBoard[i_FromPiece.Row - 1, i_FromPiece.Col + 1];
				}

            }
            else
            {
                if (i_ToPiece.Row > i_FromPiece.Row)
                {
                    returnPiece = m_CurrentBoard[i_FromPiece.Row + 1, i_FromPiece.Col - 1];
                }
                else
                {
                    returnPiece = m_CurrentBoard[i_FromPiece.Row - 1, i_FromPiece.Col - 1];
                }
            }

            return returnPiece;
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
            bool topLeftIsEnemy = true;
            bool topTopLeftIsEmpty = true;
            bool topRightIsEnemy = true;
            bool topTopRightIsEmpty = true;
            bool bottomLeftIsEnemy = true;
            bool bottomBottomLeftIsEmpty = true;
            bool bottomRightIsEnemy = true;
            bool bottomBottompRightIsEmpty = true;

            foreach (Piece obj in m_CurrentBoard)
            {
                topLeftIsEnemy = isEnemyPiece(obj.Row - 1, obj.Col - 1);
                topTopLeftIsEmpty = isPieceValue(obj.Row - 2, obj.Col - 2, ePieceValue.Empty);
                topRightIsEnemy = isEnemyPiece(obj.Row - 1, obj.Col + 1);
                topTopRightIsEmpty = isPieceValue(obj.Row - 2, obj.Col + 2, ePieceValue.Empty);
                bottomLeftIsEnemy = isEnemyPiece(obj.Row + 1, obj.Col - 1);
                bottomBottomLeftIsEmpty = isPieceValue(obj.Row + 2, obj.Col - 2, ePieceValue.Empty);
                bottomRightIsEnemy = isEnemyPiece(obj.Row + 1, obj.Col + 1);
                bottomBottompRightIsEmpty = isPieceValue(obj.Row + 2, obj.Col + 2, ePieceValue.Empty);

                // check if piece value belongs to current player
                if (m_CurrentPlayer.IsPlayerPiece(obj))
                {
                    bool isQueen = obj.PieceValue.Equals(m_CurrentPlayer.QueenPieceValue);

                    // check if current player is a bottom player 
                    if (m_CurrentPlayer.Equals(m_FirstPlayer))
                    {

                        if (topLeftIsEnemy && topTopLeftIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                        else if (topRightIsEnemy && topTopRightIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                        else if (isQueen)
                        {
                            if (bottomLeftIsEnemy && bottomBottomLeftIsEmpty)
                            {
                                m_MustToAttackList.Add(obj);
                            }
                            else if (bottomRightIsEnemy && bottomBottompRightIsEmpty)
                            {
                                m_MustToAttackList.Add(obj);
                            }
                        }
                    }
                    // current player is top player
                    else
                    {

                        if (bottomLeftIsEnemy && bottomBottomLeftIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                        else if (bottomRightIsEnemy && bottomBottompRightIsEmpty)
                        {
                            m_MustToAttackList.Add(obj);
                        }
                        else if (isQueen)
                        {

                            if (topLeftIsEnemy && topTopLeftIsEmpty)
                            {
                                m_MustToAttackList.Add(obj);
                            }
                            else if (topRightIsEnemy && topTopRightIsEmpty)
                            {
                                m_MustToAttackList.Add(obj);
                            }

                        }

                    }
                    ////TODO check queen;
                }
            }
        }

        private bool isPieceValue(int i_Row, int i_Col, ePieceValue i_PieceValue)
        {
            bool returnValue = false;

            if (i_Row >= 0 && i_Col >= 0 && i_Row < m_GameBoard.BoardSize && i_Col < m_GameBoard.BoardSize)
            {
                if (m_CurrentBoard[i_Row, i_Col].PieceValue.Equals(i_PieceValue))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private bool isEnemyPiece(int i_Row, int i_Col)
        {
            bool returnValue = true;

            if (i_Row >= 0 && i_Col >= 0 && i_Row < m_GameBoard.BoardSize && i_Col < m_GameBoard.BoardSize)
            {
                returnValue = m_EnemyPlayer.IsPlayerPiece(m_CurrentBoard[i_Row, i_Col]);
            }
            else
            {
                returnValue = false;
            }

            return returnValue;
        }

        
    }
}
