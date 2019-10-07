
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman {

    public override bool[,] PossibleMove
    {
        get
        {
            bool[,] r = new bool[8, 8];

            //UpLeft
            KnightMove(currentX - 1, currentY + 2, ref r);

            //UpRight
            KnightMove(currentX + 1, currentY + 2, ref r);

            //LeftUp
            KnightMove(currentX - 2, currentY + 1, ref r);

            //RightUp
            KnightMove(currentX + 2, currentY + 1, ref r);

            //BottomLeft
            KnightMove(currentX - 1, currentY - 2, ref r);

            //BottomRight
            KnightMove(currentX + 1, currentY - 2, ref r);

            //LeftBottom
            KnightMove(currentX - 2, currentY - 1, ref r);

            //RightBottom
            KnightMove(currentX + 2, currentY + -1, ref r);

            return r;
        }
    }

    public void KnightMove(int x,  int y, ref bool[,] r)
	{
		Chessman c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.Chessmans [x, y];
			if (c == null) {
			
				r [x, y] = true;
			}
			else if(IsWhite != c.IsWhite)
			{
				r [x, y] = true;	
			}
			
		}
	}
}