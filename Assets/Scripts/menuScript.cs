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
	public bool OpacitizeMenu;
	public bool fadeIn;
	public float opacity;
	public float instructionOpacity;
	public GameObject[] instructions;
	public GameObject[] menuObjects;
	//public AudioSource menuSong;

	int coinsvalue;
	// Use this for initialization
	void Start () {
		OpacitizeMenu = false;
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
		opacitize (false, menuObjects, instructions);
	}

	public void showMenu() {
		opacitize (true, menuObjects, instructions);
	}

	public void opacitize(bool fadeInMenu, GameObject[] objects, GameObject[] otherobjects) {
		if (!fadeInMenu) {;
			foreach(GameObject item in objects) {
				try {
					Color textcolor = item.GetComponentInChildren<Text> ().color;
					textcolor.a = 0f;
					item.GetComponentInChildren<Text> ().color = textcolor;
				} catch {}
				try {
					Color textcolor = item.GetComponent<Image> ().color;
					textcolor.a = 0f;
					item.GetComponent<Image> ().color = textcolor;
				} catch {}
			}
			foreach(GameObject item in otherobjects) {
				try {
					Color textcolor = item.GetComponentInChildren<Text> ().color;
					textcolor.a = 1f;
					item.GetComponentInChildren<Text> ().color = textcolor;
				} catch{}
				try {
					Color textcolor = item.GetComponent<Image> ().color;
					textcolor.a = 1f;
					item.GetComponent<Image> ().color = textcolor;
				} catch{}
			}
		} else {
			foreach(GameObject item in objects) {
				try {
					Color textcolor = item.GetComponentInChildren<Text> ().color;
					textcolor.a = 1f;
					item.GetComponentInChildren<Text> ().color = textcolor;
				} catch {}
				try {
					Color textcolor = item.GetComponent<Image> ().color;
					textcolor.a = 1f;
					item.GetComponent<Image> ().color = textcolor;
				} catch {}
			}
			foreach(GameObject item in otherobjects) {
				try {
					Color textcolor = item.GetComponentInChildren<Text> ().color;
					textcolor.a = 0f;
					item.GetComponentInChildren<Text> ().color = textcolor;
				} catch{}
				try {
					Color textcolor = item.GetComponent<Image> ().color;
					textcolor.a = 0f;
					item.GetComponent<Image> ().color = textcolor;
				} catch{}
			}
		}
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
