using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class menuScript : MonoBehaviour {
	public Canvas Menu;
	public Button playGame;
	int highscorevalue;
	public Text text;
	public Text coins;
	public GameObject shop;
	public GameObject playbutton;
	public GameObject player;
	public GameObject blackbackground;
	public Text highscore;
	public GameObject howtoplay;
	public GameObject leaderboard;
	public GameObject highscoretext;
	public GameObject share;
	public GameObject mainMenu;
	public GameObject invite;
	public bool fadeIn;
	public float opacity;
	public float instructionOpacity;
	public GameObject[] instructions;
	public GameObject[] menuObjects;
	public GameObject[] leaderboardObjects;
	public Button inviteButton;
	//public AudioSource menuSong;

	int coinsvalue;
	// Use this for initialization
	void Start () {
		//menuSong.Play ();
		text.GetComponent<Text> ().text = PlayerPrefs.GetInt ("LastScore").ToString ();
		Menu = Menu.GetComponent<Canvas> ();
		playGame = playGame.GetComponent<Button> ();

		highscorevalue = PlayerPrefs.GetInt ("highscore");
		highscore = highscore.GetComponent<Text>();
		highscore.text = "Highscore: " + highscorevalue;

		coinsvalue = PlayerPrefs.GetInt ("coins");
		coins = coins.GetComponent<Text> ();
		coins.text = coinsvalue.ToString();

		if (!(Array.IndexOf (PlayerPrefsX.GetStringArray ("purchased"), "Default") >= 0)) {
			PlayerPrefsX.SetStringArray ("purchased", new string[]{"Default"});
			PlayerPrefs.Save ();
		}
		foreach(GameObject item in leaderboardObjects) {
			item.SetActive(false);
		}
		foreach (GameObject item in instructions) {
			item.SetActive(false);
		}
	}
	public void Play() {
		//Debug.Log ("Clicked");
		Destroy (shop);
		Destroy (playbutton);
		Destroy (blackbackground);
		Destroy (howtoplay);
		Destroy (leaderboard);
		Destroy (share);
		Destroy (highscoretext);
		Destroy (invite);
		foreach (GameObject item in instructions) {
			Destroy (item);
		}
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
		coins.text = "0";
		highscore.text = "0";
	}

	public void Shop() {
		Application.LoadLevel ("ShopMenu");
	}

	public void showHowToPlay() {
		foreach (GameObject item in menuObjects) {
			item.SetActive(false);
		}
		foreach (GameObject item in instructions) {
			item.SetActive(true);
		}
	}

	public void showMenu() {
		foreach (GameObject item in menuObjects) {
			item.SetActive(true);
		}
		foreach (GameObject item in instructions) {
			item.SetActive(false);
		}
	}

	public void backtoScene1() {
		Application.LoadLevel ("Scene1");
	}
	public void ExitGame() {
		Application.Quit ();
	}
}
