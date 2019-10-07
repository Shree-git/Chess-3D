using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
	public GameObject playerWin;
	public Text playerWinText;
	public Text whiteWins;
	public Text blackWins;

	public AudioSource chessAudio;
	public AudioClip[] musicAudios;
	public AudioSource musicAudio;
	public Slider sfxSlider;
	public Slider musicSlider;

	private int whiteWinsCount;
	private int blackWinsCount;

	public static BoardManager Instance{ get; set; }

	private bool[,] allowedMoves{ get; set; }

	public Chessman[,] Chessmans{ get; set; }

	private Chessman selectedChessman;

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private Quaternion orientation = Quaternion.Euler (90f, 0f, 0f);

	private int selectionX = -1;
	private int selectionY = -1;

	private Material previousMat;
	public Material selectedMat;

	public List<GameObject> chessmanPieces;
	private List<GameObject> activeChessman;

	public bool isWhiteTurn = true;
	public bool textureChange;
	public bool textureBlackChange;

	void Start ()
	{
		Instance = this;
		SpawnAllChessman ();

		whiteWinsCount = 0;
		blackWinsCount = 0;

		textureChange = false;
		textureBlackChange = false;


		sfxSlider.value = chessAudio.volume;
		musicSlider.value = musicAudio.volume;



	}


	void Update ()
	{
		UpdateSelection ();
		DrawChessBoard ();

		if (textureChange) {
			ChangeWhiteTexture ();

		}

		if (textureBlackChange) {
			ChangeBlackTexture ();

		}

		if (Input.GetMouseButtonDown (0)) {
			if (selectionX >= 0 && selectionY >= 0) {
				if (selectedChessman == null) {
					SelectChessman (selectionX, selectionY);
				} else {
					MoveChessman (selectionX, selectionY);
				}
			}
		}


	}

	void SelectChessman (int x, int y)
	{
		if (Chessmans [x, y] == null)
			return;

		if (Chessmans [x, y].IsWhite != isWhiteTurn)
			return;

		bool hasAtleastOneMove = false;
		allowedMoves = Chessmans [x, y].PossibleMove ;
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (allowedMoves [i, j]) {
					hasAtleastOneMove = true;
				}
			}
		}

		if (!hasAtleastOneMove)
			return;


		selectedChessman = Chessmans [x, y];
		previousMat = selectedChessman.GetComponent<MeshRenderer> ().material;
		selectedMat.mainTexture = previousMat.mainTexture;
		selectedChessman.GetComponent<MeshRenderer> ().material = selectedMat;
		BoardHighlights.Instance.SetHighlights (allowedMoves);
	}

	void MoveChessman (int x, int y)
	{
		if (allowedMoves [x, y]) {
			Chessman c = Chessmans [x, y];

			if (c != null && c.IsWhite != isWhiteTurn) {
				//If it is the king
				if (c.GetType () == typeof(King)) {
					//End the game
					EndGame();
					return;
				}


				//Capture a piece
				activeChessman.Remove (c.gameObject);
				Destroy (c.gameObject);
			}

			Chessmans [selectedChessman.currentX, selectedChessman.currentY] = null;
			if (selectedChessman.GetType() == typeof(Knight)) {
				selectedChessman.transform.position = SetPosition (x, y) + new Vector3 (0.35f, 0f, 0f);
			} else {
				selectedChessman.transform.position = SetPosition (x, y);
			}
			selectedChessman.SetPos (x, y);
			Chessmans [x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;
		}
		chessAudio.Play ();
		selectedChessman.GetComponent<MeshRenderer> ().material = previousMat;
		BoardHighlights.Instance.RemoveHighlights ();
		selectedChessman = null;
	}

	void UpdateSelection ()
	{
		

		RaycastHit hit;

		if (Physics.Raycast (CameraChange.Instance.currentCam.ScreenPointToRay (Input.mousePosition), out hit, 25f, LayerMask.GetMask ("ChessPlane"))) {
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;

			if (selectionX >= 0 && selectionY >= 0) {
				Debug.DrawLine (
					Vector3.right * selectionX + Vector3.forward * selectionY, 
					Vector3.right * (selectionX + 1) + Vector3.forward * (selectionY + 1));

				Debug.DrawLine (
					Vector3.right * selectionX + Vector3.forward * (selectionY + 1), 
					Vector3.right * (selectionX + 1) + Vector3.forward * selectionY);

			} else {
				selectionX = -1;
				selectionY = -1;
			}
		}
	}

	void DrawChessBoard ()
	{
		Vector3 widthLine = Vector3.right * 8; 
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);

			for (int j = 0; j <= 8; j++) {
				start = Vector3.right * i;
				Debug.DrawLine (start, start + heightLine);
			}
		}
	}



	void SpawnChessman (int index, int x, int y)
	{
		GameObject go = Instantiate (chessmanPieces [index], chessmanPieces[index].transform.position+ SetPosition (x, y), chessmanPieces [index].transform.rotation) as GameObject;
		go.transform.SetParent (transform);
		Chessmans [x, y] = go.GetComponent<Chessman> ();
		Chessmans [x, y].SetPos (x, y);
		activeChessman.Add (go);


	}

	void SpawnAllChessman ()
	{
		activeChessman = new List<GameObject> ();
		Chessmans = new Chessman[8, 8];

		//White Pieces
		//King
		SpawnChessman (0, 4, 0);

		//Queen
		SpawnChessman (1, 3, 0);

		//Bishop
		SpawnChessman (2, 2, 0);
		SpawnChessman (2, 5, 0);

		//Rook
		SpawnChessman (3, 0, 0);
		SpawnChessman (3, 7, 0);

		//Knight
		SpawnChessman (4, 1, 0);
		SpawnChessman (4, 6, 0);

		//Pawn
		for (int i = 0; i < 8; i++) {
			SpawnChessman (5, i, 1);
		}



		//Black Pieces
		//King
		SpawnChessman (6, 4, 7);

		//Queen
		SpawnChessman (7, 3, 7);

		//Bishop
		SpawnChessman (8, 2, 7);
		SpawnChessman (8, 5, 7);

		//Rook
		SpawnChessman (9, 0, 7);
		SpawnChessman (9, 7, 7);

		//Knight
		SpawnChessman (10, 1, 7);
		SpawnChessman (10, 6, 7);

		//Pawn
		for (int i = 0; i < 8; i++) {
			SpawnChessman (11, i, 6);
		}


	}

	Vector3 SetPosition (int x, int y)
	{
		Vector3 origin = Vector3.zero; 
		origin.x += (TILE_SIZE * x) + TILE_OFFSET - 0.35f;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		origin.y += 0.7f;



		return origin;

	}

	public void EndGame()
	{
		playerWin.SetActive (true);
		if (isWhiteTurn) {
			playerWinText.text = "White Wins!";
			whiteWinsCount++;
			Debug.Log ("White Wins!");
		} else {
			playerWinText.text = "Black Wins!";
			blackWinsCount++;
			Debug.Log ("Black Wins!");
		}

		foreach (GameObject go in activeChessman) {
			Destroy (go);
		}


		StartCoroutine(Wait());



	}

	IEnumerator Wait(){
		PlayandWins.Instance.isPaused = true;
		whiteWins.text = "White Wins: " + whiteWinsCount;
		blackWins.text = "Black Wins: " + blackWinsCount;

		yield return new WaitForSeconds (3);

		playerWin.SetActive (false);

		isWhiteTurn = true;
		BoardHighlights.Instance.RemoveHighlights ();
		SpawnAllChessman ();

		PlayandWins.Instance.RestartWithTime ();
		PlayandWins.Instance.minutesPanel.SetActive (true);
	}


	public void ChangeWhiteTexture(){
		for (int i = 0; i < activeChessman.Count; i++) {
			if (activeChessman [i].GetComponent<Chessman> ().IsWhite) {
				activeChessman [i].GetComponent<MeshRenderer> ().material = PlayandWins.Instance.currentTexture;
			}
		}
		textureChange = false;

	}

	public void ChangeBlackTexture(){
		for (int i = 0; i < activeChessman.Count; i++) {
			if (!activeChessman [i].GetComponent<Chessman> ().IsWhite) {
					activeChessman [i].GetComponent<MeshRenderer> ().material = PlayandWins.Instance.currentBlackTexture;
				}
			}
		textureBlackChange = false;
	}


	public void Restart()
	{
		foreach (GameObject go in activeChessman) {
			Destroy (go);
		}
		isWhiteTurn = true;
		BoardHighlights.Instance.RemoveHighlights ();
		SpawnAllChessman ();

		ChangeWhiteTexture ();
		ChangeBlackTexture ();
	}

	public void OnSliderValueChange()
	{
		chessAudio.volume = sfxSlider.value;
	}

	public void OnMusicValueChange()
	{
		musicAudio.volume = musicSlider.value;
	}

	public void ChangeMusic(int index)
	{
		musicAudio.clip = musicAudios [index];
	}
}
