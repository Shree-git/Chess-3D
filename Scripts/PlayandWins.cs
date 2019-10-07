using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayandWins : MonoBehaviour {
	public static PlayandWins Instance{ get; set; }

	public Material[] textures;
	public Material currentTexture;
	public Material currentBlackTexture;

	public ReflectionProbe glassProbe;


	public Button play;
	public GameObject playerWin;
	public GameObject boardManager;
	public GameObject pausePanel;
	public Button pause;
	public GameObject minutesPanel;
	public GameObject exit;
	public GameObject optionsPanel;
	public GameObject texturesPanel;
	public GameObject soundPanel;
	public GameObject cameraPanel;
	public Image background;

	public Text whiteTimeText;
	private float whiteStartingTime;
	public Text blackTimeText;
	private float blackStartingTime;
	public bool isPaused;
	//private bool toggle = false;
	//private bool toggleBoardManager = false;


	void Start()
	{
		Instance = this;
		boardManager.SetActive (false);	
		playerWin.SetActive (false);
		pausePanel.SetActive (false);
		pause.gameObject.SetActive (false);
		isPaused = true;


	
		whiteStartingTime = 300f;
		blackStartingTime = 300f;

		whiteTimeText.gameObject.SetActive (false);
		blackTimeText.gameObject.SetActive (false);
		minutesPanel.SetActive (false);

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pausePanel.SetActive (true);
			boardManager.SetActive (false);
			pause.gameObject.SetActive (false);
			isPaused = true;

		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			pausePanel.SetActive (false);
			boardManager.SetActive (true);
			pause.gameObject.SetActive (true);
			isPaused = false;

		}

		if (pausePanel.activeInHierarchy) {
			boardManager.SetActive (false);
		}



		if (BoardManager.Instance.isWhiteTurn && !isPaused) {
			SetWhiteTimer ();
		} else if(!BoardManager.Instance.isWhiteTurn && !isPaused) {
			SetBlackTimer ();

		}

		if (whiteStartingTime <= 0f) {
			BoardManager.Instance.isWhiteTurn = false;
			BoardManager.Instance.EndGame ();

			whiteStartingTime = 300f;
			blackStartingTime = 300f;


		} else if (blackStartingTime <= 0f) {
			BoardManager.Instance.isWhiteTurn = true;
			BoardManager.Instance.EndGame ();


			whiteStartingTime = 300f;
			blackStartingTime = 300f;


		}
		//if (whiteStartingTime < 10f || blackStartingTime < 10f) {
			
	
	}

	public void OnPressPause()
	{
		pausePanel.SetActive (true);
		boardManager.SetActive (false);
		pause.gameObject.SetActive (false);
		isPaused = true;

		BoardManager.Instance.musicAudio.Pause ();
		background.gameObject.SetActive (true);
		BoardHighlights.Instance.RemoveHighlights ();
	}

	public void Resume()
	{
		pausePanel.SetActive (false);
		boardManager.SetActive (true);
		pause.gameObject.SetActive (true);
		isPaused = false;
		BoardManager.Instance.musicAudio.Play ();
		background.gameObject.SetActive (false);

	}

	public void Restart()
	{
		BoardManager.Instance.Restart ();



		pausePanel.SetActive (false);
		boardManager.SetActive (false);
		pause.gameObject.SetActive (false);
		isPaused = true;

		whiteTimeText.text = "Time: ";
		blackTimeText.text = "Time: ";

		minutesPanel.SetActive (true);

	}

	public void RestartWithTime()
	{
		minutesPanel.SetActive (false);
		pausePanel.SetActive (false);
		boardManager.SetActive (true);
		pause.gameObject.SetActive (false);
		isPaused = true;

		whiteTimeText.text = "Time: ";
		blackTimeText.text = "Time: ";


	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void OnPressPlay()
	{
		minutesPanel.SetActive (true);



	}

	void SetWhiteTimer()
	{
		int whiteMinutes =(int)whiteStartingTime / 60;
		int whiteSeconds = (int)whiteStartingTime % 60;
		whiteStartingTime -= Time.deltaTime;

		whiteTimeText.text = "Time: " + whiteMinutes + ": " + whiteSeconds;
	}

	void SetBlackTimer()
	{
		int blackMinutes =(int)blackStartingTime / 60;
		int blackSeconds = (int)blackStartingTime % 60;
		blackStartingTime -= Time.deltaTime;

		blackTimeText.text = "Time: " + blackMinutes + ": " + blackSeconds;
	}

	public void GetMinutes(float minutes)
	{
		whiteStartingTime = minutes * 60f;
		blackStartingTime = minutes * 60f;

		minutesPanel.SetActive (false);
		boardManager.SetActive (true);
		play.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
		pause.gameObject.SetActive (true);
		isPaused = false;
		whiteTimeText.gameObject.SetActive (true);
		blackTimeText.gameObject.SetActive (true);

		BoardManager.Instance.musicAudio.Play ();
		background.gameObject.SetActive (false);
	}

	public void OnWhiteTextureClick(int i)
	{
		currentTexture = textures [i];
		BoardManager.Instance.textureChange = true;

		/*if (currentTexture == textures [4] || currentTexture == textures [5]) {
			glassProbe.gameObject.SetActive (true);
		} else {
			glassProbe.gameObject.SetActive (false);
		}*/
	}

	public void OnBlackTextureClick(int i)
	{
		currentBlackTexture = textures [i];
		BoardManager.Instance.textureBlackChange = true;

		/*if (currentTexture == textures [5] ||currentTexture == textures[4]) {
			glassProbe.gameObject.SetActive (true);
		} else {
			glassProbe.gameObject.SetActive (false);
		}*/
	}

	public void Options()
	{
		pausePanel.SetActive (false);
		optionsPanel.SetActive (true);
	}

	public void BackToPause()
	{
		pausePanel.SetActive (true);
		optionsPanel.SetActive (false);
	}

	public void Textures()
	{
		optionsPanel.SetActive (false);
		texturesPanel.SetActive (true);
	}

	public void BackToOptions()
	{
		optionsPanel.SetActive (true);
		texturesPanel.SetActive (false);
		soundPanel.SetActive (false);
		cameraPanel.SetActive (false);
	}

	public void Sound()
	{
		soundPanel.SetActive (true);
		optionsPanel.SetActive (false);
	}

	public void Camera()
	{
		cameraPanel.SetActive (true);
		optionsPanel.SetActive (false);
	}
}

