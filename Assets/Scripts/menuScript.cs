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
	public GameObject shop;
	public GameObject playbutton;
	public GameObject player;
	//public AudioSource menuSong;

	int coinsvalue;
	// Use this for initialization
	void Start () {
		//menuSong.Play ();
		Menu = Menu.GetComponent<Canvas> ();
		playGame = playGame.GetComponent<Button> ();

		highscorevalue = PlayerPrefs.GetInt ("highscore");
		highscore = highscore.GetComponent<Text>();
		highscore.text = highscorevalue.ToString ();

		coinsvalue = PlayerPrefs.GetInt ("coins");
		coins = coins.GetComponent<Text> ();
		coins.text = coinsvalue.ToString();

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
		Destroy (shop);
		Destroy (playbutton);
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
		coins.text = "0";
		highscore.text = "0";
	}

	public void Shop() {
		Application.LoadLevel ("ShopMenu");
	}


	public void ExitGame() {
		Application.Quit ();
	}
}
