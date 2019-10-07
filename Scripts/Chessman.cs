using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour {

	public int currentX{ get; set;}
	public int currentY{get; set;}
	public bool isWhite;

	public void SetPos(int x, int y){
		currentX = x;
		currentY = y;
	}

	public virtual bool[,] PossibleMove()
	{
		return new bool[8,8];
	}
}
