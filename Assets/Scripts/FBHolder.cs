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
	
	void enableMenu(FBResult response) {
		if(response.Error != null)
		{
			Debug.LogError("OnActionShared: Error: " + response.Error);
		}
		
		if (response == null || response.Error != null)
		{
			Application.LoadLevel ("Scene1");
		}
		else
		{
			var responseObject = Json.Deserialize(response.Text) as Dictionary<string, object>;
			object obj = 0;
			if (responseObject == null || responseObject.Count <= 0 || responseObject.TryGetValue("cancelled", out obj))
			{
				Application.LoadLevel ("Scene1");
			}
			else if (responseObject.TryGetValue("id", out obj) || responseObject.TryGetValue("post_id", out obj) )
			{
				PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 500);
				PlayerPrefs.SetString("DontShowShare", "True");
				Application.LoadLevel ("Scene1");
				//Do something it is succeeded
			}
		}
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
			FB.Login ("publish_actions,user_friends", shareCallback);
		} else {
			FB.Feed (
				linkCaption: "My Highscore is " + PlayerPrefs.GetInt ("highscore") + ". Can you beat me?",
				picture : "",
				linkName: "Check out JetSquare by clicking here!",
				linkDescription: "Play JetSquare, a fun and addicting game where minimalistic art is taken to a maximum. Try to beat your friends with infinite and forever entertaining gameplay that progressively gets harder as you go!",
				link: "https://www.facebook.com/jetsquareapp",
				callback: enableMenu
				);
		}
	}
	void shareCallback(FBResult response) {
		try {
		FB.Feed (
			linkCaption: "My Highscore is " + PlayerPrefs.GetInt ("highscore") + ". Can you beat me?",
			picture : "",
			linkName: "Check out JetSquare by clicking here!",
			linkDescription: "Play JetSquare, a fun and addicting game where minimalistic art is taken to a maximum. Try to beat your friends with infinite and forever entertaining gameplay that progressively gets harder as you go!",
			link: "https://www.facebook.com/jetsquareapp",
			callback: enableMenu
			);
		} catch {
			Application.LoadLevel ("Scene1");
		}
	}
	public void inviteFriends() {
		foreach (GameObject item in canvas.GetComponent<menuScript>().menuObjects) {
			item.SetActive(false);
		}
		if (!FB.IsLoggedIn) {
			FB.Login ("publish_actions,user_friends", inviteFriendsCallback);
		} else {
			FB.AppRequest(
				"Come play this great game!",
				null,
				null,
				null,
				20,
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
			20,
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
		backButton.GetComponent<Button> ().enabled = true;
		SetScore ();
		QueryScores ();
	}
	public void enableLeaderboard() {
		FB.Login ("publish_actions, user_friends", handleLeaderboard);
	}
	void handleLeaderboard(FBResult response) {
		Application.LoadLevel ("Scene1");
	}
	public void QueryScores() {
		FB.API ("/app/scores?fields=score,user.limit(30)", Facebook.HttpMethod.GET, ScoresCallback);
	}

	private void ScoresCallback(FBResult response) {
		scoresList = Util.DeserializeScores(response.Text);
		Debug.Log ("scoresList");
		Debug.Log (scoresList);
		foreach (Transform item in ScoreScrollList.transform) {
			GameObject.Destroy (item);
		}
		foreach (object score in scoresList) {
			var entry = (Dictionary<string, object>) score;
			var user = (Dictionary<string, object>) entry["user"];
			Debug.Log ("score:");
			Debug.Log (entry["score"].ToString());
			Debug.Log ("name:");
			Debug.Log (user["name"].ToString());
			GameObject scorePanel;
			scorePanel = Instantiate(ScoreEntryPanel) as GameObject;
			scorePanel.transform.SetParent(ScoreScrollList.transform, false);

			scorePanel.transform.Find ("Name").GetComponent<Text>().text = user["name"].ToString();
			scorePanel.transform.Find ("Score").GetComponent<Text>().text = entry["score"].ToString();
			Transform scoreImage = scorePanel.transform.Find ("Image");
			Image UserAvatar = scoreImage.GetComponent<Image>();
			FB.API (GetPictureURL(user["id"].ToString(), 128,128), Facebook.HttpMethod.GET, delegate(FBResult pictureResult)
			        {
				string imageUrl = DeserializePictureURLString(pictureResult.Text);
				StartCoroutine(LoadPictureEnumerator(imageUrl,pictureTexture =>
				                                     {
					UserAvatar.sprite = Sprite.Create (pictureTexture, new Rect(0,0,128,128), new Vector2(0,0));
				}));
			});
		}
	}

	delegate void LoadPictureCallback (Texture2D texture);
	
	IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)    
	{
		WWW www = new WWW(url);
		yield return www;
		callback(www.texture);
	}
	
	public void SetScore() {
		var scoreData = new Dictionary<string, string> ();
		scoreData ["score"] = PlayerPrefs.GetInt ("highscore").ToString ();
		FB.API ("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult result) {
			
		}, scoreData);
	}
	public string DeserializePictureURLString(string response)
	{
		return DeserializePictureURLObject(Json.Deserialize(response));
	}
	
	public string DeserializePictureURLObject(object pictureObj)
	{
		var picture = (Dictionary<string, object>)(((Dictionary<string, object>)pictureObj)["data"]);
		object urlH = null;
		if (picture.TryGetValue("url", out urlH))
		{
			return (string)urlH;
		}
		return null;
	}
	
	private string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
	{
		string url = string.Format("/{0}/picture", facebookID);
		string query = width != null ? "&width=" + width.ToString() : "";
		query += height != null ? "&height=" + height.ToString() : "";
		query += type != null ? "&type=" + type : "";
		query += "&redirect=false";
		if (query != "") url += ("?g" + query);
		return url;
	}
}
