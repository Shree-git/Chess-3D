using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman {
    public override bool[,] PossibleMove
    {
        get
        {
            bool[,] r = new bool[8, 8];

            Chessman c;

            int i;
            int j;

            //Top Side
            i = currentX - 1;
            j = currentY + 1;

            if (currentY != 7)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (i >= 0 || i < 8)
                    {
                        c = BoardManager.Instance.Chessmans[i, j];
                        if (c == null)
                        {
                            r[i, j] = true;
                        }
                        else if (IsWhite != c.IsWhite)
                        {
                            r[i, j] = true;
                        }
                    }
                    i++;
                }
            }

            //Bottom Side
            i = currentX - 1;
            j = currentY - 1;

            if (currentY != 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (i >= 0 || i < 8)
                    {
                        c = BoardManager.Instance.Chessmans[i, j];
                        if (c == null)
                        {
                            r[i, j] = true;
                        }
                        else if (IsWhite != c.IsWhite)
                        {
                            r[i, j] = true;
                        }
                    }
                    i++;
                }
            }

            //Middle Left
            if (currentX != 0)
            {
                c = BoardManager.Instance.Chessmans[currentX - 1, currentY];
                if (c == null)
                {
                    r[currentX - 1, currentY] = true;
                }
                else if (IsWhite != c.IsWhite)
                {
                    r[currentX - 1, currentY] = true;
                }
            }

            //Middle Right
            if (currentX != 7)
            {
                c = BoardManager.Instance.Chessmans[currentX + 1, currentY];
                if (c == null)
                {
                    r[currentX + 1, currentY] = true;
                }
                else if (IsWhite != c.IsWhite)
                {
                    r[currentX + 1, currentY] = true;
                }
            }

            return r;
        }
    }
}
