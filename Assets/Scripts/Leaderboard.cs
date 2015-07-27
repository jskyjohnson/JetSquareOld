using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {
	public Text highscore;
	void Start() {
		highscore.GetComponent<Text> ().text = "Your Highscore " + PlayerPrefs.GetString ("highscore");
	}
	public void back() {
		Application.LoadLevel ("Scene1");
	}
}
