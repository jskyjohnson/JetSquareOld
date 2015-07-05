using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class menuScript : MonoBehaviour {
	public Canvas Menu;
	public Button playGame;
	int highscorevalue;
	public Text highscore;
	public Text coins;
	int coinsvalue;
	// Use this for initialization
	void Start () {
		Menu = Menu.GetComponent<Canvas> ();
		playGame = playGame.GetComponent<Button> ();

		highscorevalue = PlayerPrefs.GetInt ("highscore");
		highscore = highscore.GetComponent<Text>();
		highscore.text = "Highscore: " + highscorevalue;

		coinsvalue = PlayerPrefs.GetInt ("coins");
		coins = coins.GetComponent<Text> ();
		coins.text = "Total Coins: " + coinsvalue;

		if (!(Array.IndexOf (PlayerPrefsX.GetStringArray ("purchased"), "Default") >= 0)) {
			PlayerPrefsX.SetStringArray ("purchased", new string[]{"Default"});
			PlayerPrefs.Save ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play() {
		//Debug.Log ("Clicked");
		Destroy (GameObject.Find ("Score"));
		Application.LoadLevel ("Scene1");
	}

	public void Shop() {
		Application.LoadLevel ("ShopMenu");
	}


	public void ExitGame() {
		Application.Quit ();
	}
}
