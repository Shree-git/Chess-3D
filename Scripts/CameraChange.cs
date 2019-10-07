using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

	public static CameraChange Instance{ get; set;}

	public Camera whiteCam;
	public Camera blackCam;
	public Camera currentCam;
	public Camera secondCam;
	public Camera secondCamBlack;
	private int currentCamNum;

	void Start()
	{
		Instance = this;
		currentCamNum = 1;
		currentCam = whiteCam;
		whiteCam.gameObject.SetActive (true);
		blackCam.gameObject.SetActive (false);
		secondCam.gameObject.SetActive (false);
		secondCamBlack.gameObject.SetActive (false);
	}

	void LateUpdate()
	{
		if (currentCamNum == 1) {
			if (BoardManager.Instance.isWhiteTurn) {
				whiteCam.gameObject.SetActive (true);
				blackCam.gameObject.SetActive (false);
				secondCamBlack.gameObject.SetActive (false);
				secondCam.gameObject.SetActive (false);
				currentCam = whiteCam;
			} else {
				whiteCam.gameObject.SetActive (false);
				blackCam.gameObject.SetActive (true);
				secondCamBlack.gameObject.SetActive (false);
				secondCam.gameObject.SetActive (false);
				currentCam = blackCam;
			}
		} else if (currentCamNum == 2) {
			if (BoardManager.Instance.isWhiteTurn) {
				secondCam.gameObject.SetActive (true);
				secondCamBlack.gameObject.SetActive (false);
				whiteCam.gameObject.SetActive (false);
				blackCam.gameObject.SetActive (false);
				currentCam = secondCam;
			} else {
				secondCamBlack.gameObject.SetActive (true);
				secondCam.gameObject.SetActive (false);
				whiteCam.gameObject.SetActive (false);
				blackCam.gameObject.SetActive (false);
				currentCam = secondCamBlack;
			}	
		}
	}

	public void ChangeCam(int index){
		currentCamNum = index;
	}

}
