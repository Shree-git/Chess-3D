using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour {

	public static BoardHighlights Instance{ get; set; }

	public GameObject highlightPrefab;
	private List<GameObject> highlights;

	void Start()
	{
		Instance = this;
		highlights = new List<GameObject> ();
	}

	public GameObject GetHighlightsPrefab()
	{
		GameObject go;
		go = highlights.Find (g => !g.activeSelf);

		if (go == null) {
			go = Instantiate (highlightPrefab) as GameObject;
			highlights.Add (go);
		}

		return go;
	}

	public void SetHighlights(bool[,] array)
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (array [i, j]) 
				{
					GameObject go = GetHighlightsPrefab ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i+0.5f, 0, j+0.5f);
				}
			}
		}
	}

	public void RemoveHighlights()
	{
		foreach (GameObject go in highlights) {
			go.SetActive (false);
		}
	}
}
