using System;
using UnityEngine;
using ChartboostSDK;

public class AdsManager : MonoBehaviour {
	public static int adIteration;
	public static bool notAdIteration;
	public GameObject chartboostObject;
	void Start() {
		notAdIteration = true;
		Chartboost.cacheInterstitial (CBLocation.Default);
	}
	public static void loadAd() {
		if ((PlayerPrefs.GetInt ("GameTime") >= 100 || PlayerPrefs.GetInt ("adIteration") >= 5)){ //&& PlayerPrefs.GetString("ads") != "false") {
			float randomNum = UnityEngine.Random.Range (0f, 100f);
			ChartboostExample.runAd();
			PlayerPrefs.SetInt ("adIteration", 0);
			PlayerPrefs.SetInt ("GameTime", 0);
			Application.LoadLevel ("Scene1");
		}
	}
}