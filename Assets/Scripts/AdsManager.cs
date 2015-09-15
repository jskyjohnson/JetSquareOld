using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
	public static int adIteration;
	public static bool notAdIteration;
	void Awake() {
	}
	void Start() {
		notAdIteration = true;
	}
	public static void loadAd() {
		if ((PlayerPrefs.GetInt ("GameTime") >= 210 || PlayerPrefs.GetInt ("adIteration") >= 12)){ //&& PlayerPrefs.GetString("ads") != "false") {
			float randomNum = UnityEngine.Random.Range (0f, 100f);
			if (Advertisement.IsReady ()) {
			Advertisement.Show (null, new ShowOptions {
				resultCallback = ShowResult => {
					PlayerPrefs.SetInt ("GameTime", 0);
					PlayerPrefs.SetInt ("adIteration", 0);
					Application.LoadLevel ("Scene1");
					}
				});
			} else {
				Application.LoadLevel ("Scene1");
			}
		}
		else {
		}
	}
}