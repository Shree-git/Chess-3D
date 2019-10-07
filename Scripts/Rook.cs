using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman {

    public override bool[,] PossibleMove
    {
        get
        {
            bool[,] r = new bool[8, 8];

            Chessman c;
            int i;

            //Right
            i = currentX;
            while (true)
            {
                i++;

                if (i >= 8)
                    break;

                c = BoardManager.Instance.Chessmans[i, currentY];

                if (c == null)
                {
                    r[i, currentY] = true;
                }
                else if (c.IsWhite != IsWhite)
                {

                    r[i, currentY] = true;

                    break;
                }
                else
                {
                    break;
                }
            }

            //Left
            i = currentX;
            while (true)
            {
                i--;

                if (i < 0)
                    break;

                c = BoardManager.Instance.Chessmans[i, currentY];

                if (c == null)
                {
                    r[i, currentY] = true;
                }
                else if (c.IsWhite != IsWhite)
                {

                    r[i, currentY] = true;

                    break;
                }
                else
                {
                    break;
                }
            }

            //Up
            i = currentY;
            while (true)
            {
                i++;

                if (i >= 8)
                    break;

                c = BoardManager.Instance.Chessmans[currentX, i];

                if (c == null)
                    r[currentX, i] = true;
                else if (c.IsWhite != IsWhite)
                {

                    r[currentX, i] = true;

                    break;
                }
                else
                {
                    break;
                }

            }


            //Down
            i = currentY;
            while (true)
            {
                i--;

                if (i < 0)
                    break;

                c = BoardManager.Instance.Chessmans[currentX, i];

                if (c == null)
                {
                    r[currentX, i] = true;
                }
                else if (c.IsWhite != IsWhite)
                {

                    r[currentX, i] = true;

                    break;
                }
                else
                {
                    break;
                }
            }

            return r;
        }
    }
}
