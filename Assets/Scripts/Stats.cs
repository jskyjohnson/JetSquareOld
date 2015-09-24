using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Stats : MonoBehaviour {
	public Text totalDeaths;
	public Text totalTime;
	public Text averageTimePerGame;
	public Text totalScore;
	public Text averageScorePerGame;
	public Text totalYellowCoins;
	public Text totalRedCoins;
	public Text totalBlueCoins;
	public Text totalPinkCoins;
	public Button back;
	// Use this for initialization
	void Start () {
		totalDeaths.text = "Total Deaths: " + PlayerPrefs.GetInt ("TotalDeaths").ToString();
		totalTime.text = "Total Time Played: " + PlayerPrefs.GetInt ("TotalTimePlayed").ToString() + " seconds.";
		averageTimePerGame.text = "Average Time: " + Mathf.RoundToInt((float)PlayerPrefs.GetInt ("TotalTimePlayed")/(float)PlayerPrefs.GetInt ("TotalDeaths")).ToString() + " seconds.";
		totalScore.text = "Total Score: " + PlayerPrefs.GetInt ("TotalScore").ToString();
		averageScorePerGame.text = "Average Score: " + Mathf.RoundToInt((float)PlayerPrefs.GetInt ("TotalScore")/(float)PlayerPrefs.GetInt ("TotalDeaths")).ToString() + " per game.";
		totalYellowCoins.text = "Total Yellow Coins: " + PlayerPrefs.GetInt ("TotalYellowCoins").ToString();
		totalRedCoins.text = "Total Red Coins: " + PlayerPrefs.GetInt ("TotalRedCoins").ToString();
		totalBlueCoins.text = "Total Blue Coins: " + PlayerPrefs.GetInt ("TotalBlueCoins").ToString();
		totalPinkCoins.text = "Total Pink Coins: " + PlayerPrefs.GetInt ("TotalPinkCoins").ToString();
	}

	public void Back() {
		Application.LoadLevel("Scene1");
	}
}
