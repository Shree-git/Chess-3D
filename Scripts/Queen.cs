using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman {

    public override bool[,] PossibleMove
    {
        get
        {
            bool[,] r = new bool[8, 8];

            Chessman c;

            int i;
            int j;

            //TopLeft
            i = currentX;
            j = currentY;
            while (true)
            {
                i--;
                j++;

                if (i < 0 || j >= 8)
                    break;

                c = BoardManager.Instance.Chessmans[i, j];


                if (c == null)
                {
                    r[i, j] = true;
                }
                else if (c.IsWhite != IsWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            //TopRight
            i = currentX;
            j = currentY;
            while (true)
            {
                i++;
                j++;

                if (i >= 8 || j >= 8)
                    break;

                c = BoardManager.Instance.Chessmans[i, j];


                if (c == null)
                {
                    r[i, j] = true;
                }
                else if (c.IsWhite != IsWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            //BottomLeft
            i = currentX;
            j = currentY;
            while (true)
            {
                i--;
                j--;

                if (i < 0 || j < 0)
                    break;

                c = BoardManager.Instance.Chessmans[i, j];


                if (c == null)
                {
                    r[i, j] = true;
                }
                else if (c.IsWhite != IsWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            //BottomRight
            i = currentX;
            j = currentY;
            while (true)
            {
                i++;
                j--;

                if (i >= 8 || j < 0)
                    break;

                c = BoardManager.Instance.Chessmans[i, j];


                if (c == null)
                {
                    r[i, j] = true;
                }
                else if (c.IsWhite != IsWhite)
                {
                    r[i, j] = true;
                    break;
                }
                else
                {
                    break;
                }
            }

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
