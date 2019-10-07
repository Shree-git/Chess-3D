using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman {

    public override bool[,] PossibleMove
    {
        get
        {
            bool[,] r = new bool[8, 8];

            Chessman c, c2;

            //White Pawn
            if (IsWhite)
            {
                //Diagonal Left
                if (currentX != 0 && currentY != 7)
                {
                    c = BoardManager.Instance.Chessmans[currentX - 1, currentY + 1];

                    if (c != null && !c.IsWhite)
                    {
                        r[currentX - 1, currentY + 1] = true;
                    }
                }

                //Diagonal Right
                if (currentX != 7 && currentY != 7)
                {
                    c = BoardManager.Instance.Chessmans[currentX + 1, currentY + 1];

                    if (c != null && !c.IsWhite)
                    {
                        r[currentX + 1, currentY + 1] = true;
                    }
                }

                //Middle
                if (currentY != 7)
                {
                    c = BoardManager.Instance.Chessmans[currentX, currentY + 1];

                    if (c == null)
                    {
                        r[currentX, currentY + 1] = true;
                    }
                }


                //Middle Two
                if (currentY == 1)
                {
                    c = BoardManager.Instance.Chessmans[currentX, currentY + 1];
                    c2 = BoardManager.Instance.Chessmans[currentX, currentY + 2];

                    if (c == null && c2 == null)
                    {
                        r[currentX, currentY + 2] = true;
                    }
                }

            }
            else
            {
                //Diagonal Left
                if (currentX != 0 && currentY != 0)
                {
                    c = BoardManager.Instance.Chessmans[currentX - 1, currentY - 1];

                    if (c != null && c.IsWhite)
                    {
                        r[currentX - 1, currentY - 1] = true;
                    }
                }

                //Diagonal Right
                if (currentX != 7 && currentY != 0)
                {
                    c = BoardManager.Instance.Chessmans[currentX + 1, currentY - 1];

                    if (c != null && c.IsWhite)
                    {
                        r[currentX + 1, currentY - 1] = true;
                    }
                }

                //Middle
                if (currentY != 0)
                {
                    c = BoardManager.Instance.Chessmans[currentX, currentY - 1];

                    if (c == null)
                    {
                        r[currentX, currentY - 1] = true;
                    }
                }


                //Middle Two
                if (currentY == 6)
                {
                    c = BoardManager.Instance.Chessmans[currentX, currentY - 1];
                    c2 = BoardManager.Instance.Chessmans[currentX, currentY - 2];

                    if (c == null && c2 == null)
                    {
                        r[currentX, currentY - 2] = true;
                    }
                }
            }


            return r;
        }
    }
}
