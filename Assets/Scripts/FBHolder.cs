using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;
using System.Linq;
public class FBHolder : MonoBehaviour {
	public Canvas canvas;
	public GameObject[] purchaseAbleObjects;
	private List<object> scoresList = null;
	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;
	public Button backButton;
	void Awake() {
		FB.Init (SetInit, OnHideUnity);
	}
	private void SetInit() {
		if (FB.IsLoggedIn) {
			Debug.Log ("FB logged in");
		} else {
		}
	}
	private void OnHideUnity(bool isGameShown) {
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
	public void FBloginfromhome() {
		Application.LoadLevel ("Leaderboard");
		FB.Login ("user_friends, publish_actions, user_photos", AuthCallback);
	}

	public void FBlogin() {
		FB.Login ("user_friends, publish_actions, user_photos", AuthCallback);
	}

	void AuthCallback (FBResult result) {
		if (FB.IsLoggedIn) {
			Debug.Log ("FB login worked.");
		} else {
			Debug.Log ("FB Login fail.");
		}
	}
	
	void enableMenu(FBResult response) {
		Application.LoadLevel ("Scene1");
	}

	void handleInviteCallback(FBResult response) {
		foreach (GameObject item in canvas.GetComponent<menuScript>().menuObjects) {
			item.SetActive(true);
		}
		// get the list of users invited
		var responseObject = Json.Deserialize(response.Text) as Dictionary<string, object>;
		
		// get the amount of users invited and convert to an integer
		IEnumerable<object> invitesSent = (IEnumerable<object>)responseObject["to"];
		int numberOfInvites = invitesSent.Count();
		// for every user invited, do something
		for(int i = 1; i <= numberOfInvites; i++)
		{
			PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 15);
		}
		Application.LoadLevel ("Scene1");
	}
	public void ShareWithFriends() {
		foreach (GameObject item in canvas.GetComponent<menuScript>().menuObjects) {
			item.SetActive(false);
		}
		if (!FB.IsLoggedIn) {
			FB.Login ("user_friends, publish_actions, user_photos", shareCallback);
		} else {
			FB.Feed (
				linkCaption: "This game is so awesome, come try to beat me!",
				picture : "",
				linkName: "Check out JetSquare by clicking here!",
				linkDescription: "Play JetSquare, a fun and addicting game where minimalistic art is taken to a maximum. Try to beat your friends with infinite and forever entertaining gameplay that progressively gets harder as you go!",
				link: "http://google.com",
				callback: enableMenu
				);
		}
	}
	void shareCallback(FBResult response) {
		FB.Feed (
			linkCaption: "This game is so awesome, come try to beat me!",
			picture : "",
			linkName: "Check out JetSquare by clicking here!",
			linkDescription: "Play JetSquare, a fun and addicting game where minimalistic art is taken to a maximum. Try to beat your friends with infinite and forever entertaining gameplay that progressively gets harder as you go!",
			link: "http://google.com",
			callback: enableMenu
			);
	}
	public void inviteFriends() {
		foreach (GameObject item in canvas.GetComponent<menuScript>().menuObjects) {
			item.SetActive(false);
		}
		if (!FB.IsLoggedIn) {
			FB.Login ("user_friends, publish_actions, user_photos", inviteFriendsCallback);
		} else {
			FB.AppRequest(
				"Come play this great game!",
				null,
				null,
				null,
				10,
				string.Empty,
				"Invite friends to play JetSquare",
				handleInviteCallback
				);
		}
	}
	void inviteFriendsCallback(FBResult response) {
		FB.AppRequest(
			"Come play this great game!",
			null,
			null,
			null,
			10,
			string.Empty,
			"Invite friends to play JetSquare",
			handleInviteCallback
			);
	}
	//All Scores API related
	public void showLeaderboard() {
		backButton.GetComponent<Button> ().enabled = false;
		foreach (GameObject item in canvas.GetComponent<menuScript>().menuObjects) {
			item.SetActive(false);
		}
		foreach (GameObject item in canvas.GetComponent<menuScript>().leaderboardObjects) {
			item.SetActive (true);
		}
		if (!FB.IsLoggedIn) {
			FB.Login ("user_friends, publish_actions, user_photos", handleLeaderboard);
		} else {
			backButton.GetComponent<Button> ().enabled = true;
			SetScore ();
			QueryScores ();
		}
	}
	void handleLeaderboard(FBResult response) {
		backButton.GetComponent<Button> ().enabled = true;
		SetScore ();
		QueryScores ();
	}
	public void QueryScores() {
		FB.API ("/app/scores?fields=score,user.limit(30)", Facebook.HttpMethod.GET, ScoresCallback);
	}

	private void ScoresCallback(FBResult response) {
		scoresList = Util.DeserializeScores(response.Text);
		foreach (Transform item in ScoreScrollList.transform) {
			GameObject.Destroy (item);
		}
		foreach (object score in scoresList) {
			var entry = (Dictionary<string, object>) score;
			var user = (Dictionary<string, object>) entry["user"];

			GameObject scorePanel;
			scorePanel = Instantiate(ScoreEntryPanel) as GameObject;
			scorePanel.transform.parent = ScoreScrollList.transform;

			scorePanel.transform.Find ("Name").GetComponent<Text>().text = user["name"].ToString();
			scorePanel.transform.Find ("Score").GetComponent<Text>().text = entry["score"].ToString();
			Transform scoreImage = scorePanel.transform.Find ("Image");
			Image UserAvatar = scoreImage.GetComponent<Image>();
			FB.API (Util.GetPictureURL(user["id"].ToString (), 128, 128), Facebook.HttpMethod.GET, delegate(FBResult pictureResult) {
				if(pictureResult.Error != null) {
					Debug.Log (pictureResult.Error);
				} else {
					UserAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
				}
			});
		}

	}

	public void SetScore() {
		var scoreData = new Dictionary<string, string> ();
		scoreData ["score"] = PlayerPrefs.GetInt ("highscore").ToString ();
		FB.API ("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult result) {
			
		}, scoreData);
	}
}
