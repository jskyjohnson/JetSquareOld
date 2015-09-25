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
	public Text countdown;
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
	public GameObject achievements;
	public bool fadeIn;
	public float opacity;
	public GameObject[] menuObjects;
	public GameObject[] leaderboardObjects;
	public GameObject[] buttonBounce;
	public GameObject instructionText;
	public Button inviteButton;
	public GameObject enterPromoButton;
	public GameObject promoContainer;

	//public AudioSource menuSong;

	int coinsvalue;
	// Use this for initialization
	void Start () {
		promoContainer.SetActive (false);
		//menuSong.Play ();
		foreach(GameObject item in buttonBounce) {
			item.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 200f);
		}

		if(PlayerPrefs.GetInt ("TotalScore") == 0) {
			PlayerPrefs.SetInt ("TotalScore", PlayerPrefs.GetInt ("highscore"));
		}
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
		if (PlayerPrefs.GetString ("DontShowShare") == "True") {
			share.SetActive (false);
		}
	}
	IEnumerator bounce(){
		foreach(GameObject item in buttonBounce) {
			if(item.GetComponent<RectTransform>().sizeDelta.x > 177f) {
				Vector2 newSize = new Vector2(item.GetComponent<RectTransform>().sizeDelta.x - 1f, item.GetComponent<RectTransform>().sizeDelta.y -1f);
				item.GetComponent<RectTransform>().sizeDelta = newSize;
				yield return 0;
			}
		}
	}
	void Update() {
		if(playbutton != null) {
			StartCoroutine(bounce());
		}
		if(Player.isDemo) {
			if(player.GetComponent<Player>().score <= 1) {
				instructionText.GetComponent<Text>().text = "Tap and hold to build power, release to jump.";
			} else if (player.GetComponent<Player>().score <= 2) {
				instructionText.GetComponent<Text>().text = "You can double jump.";
			} else if (player.GetComponent<Player>().score <= 3) {
				instructionText.GetComponent<Text>().text = "Press play to start after dying.";
			}
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
		Destroy (achievements);
		Destroy (enterPromoButton);
		StartCoroutine(beginGame());
		coins.text = "0";
		text.text = "0";
		highscore.text = "0";
	}

	public void Shop() {
		Application.LoadLevel ("ShopMenu");
	}

	IEnumerator beginGame() {
		int count = 2;
		while(count > 0) {
			countdown.text = count.ToString();
			count --;
			yield return new WaitForSeconds(1.0f);
		}
		Destroy (countdown);
		player.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	public void showMenu() {
		foreach (GameObject item in menuObjects) {
			if (PlayerPrefs.GetString ("DontShowShare") == "True" && item.Equals(share)) {
			} else {
				item.SetActive(true);
			}
		}
	}

	public void Stats() {
		Application.LoadLevel ("Stats");
	}

	public void Demo() {
		Player.isDemo = true;
		Play ();
	}

	public void backtoScene1() {
		Application.LoadLevel ("Scene1");
	}
	public void ExitGame() {
		Application.Quit ();
	}
	public void openPromo() {
		promoContainer.SetActive (true);
	}
	public void checkIfPromoCodeIsCorrect(string code) {
		if(code == "Update2.0") {
			if(PlayerPrefs.GetString ("rewardedUpdate2.0") != "true") {
				PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 1200);
				coins.text = PlayerPrefs.GetInt ("coins").ToString();
				PlayerPrefs.SetString ("rewardedUpdate2.0", "true");
			}
		} else if (code == "BeholdTheYanmaster") {
			if(PlayerPrefs.GetString ("rewardedBeholdTheYanmaster") != "true") {
				PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 1200);
				coins.text = PlayerPrefs.GetInt ("coins").ToString();
				PlayerPrefs.SetString ("rewardedBeholdTheYanmaster", "true");
			}
		}
	}
}
