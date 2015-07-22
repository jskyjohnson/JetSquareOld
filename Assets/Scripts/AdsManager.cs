using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
	public static int adIteration;
	public static bool notAdIteration;
	void Awake() {
		if (Advertisement.isSupported) {
			Advertisement.Initialize ("56206", true);
		} else {
			Debug.Log("Platform not supported");
		}
	}
	void Start() {
		notAdIteration = true;
	}
	public static void loadAd() {
		PlayerPrefs.SetInt ("adIteration", PlayerPrefs.GetInt ("adIteration") + 1);
		notAdIteration = true;
		Debug.Log (PlayerPrefs.GetInt ("adIteration"));
		if (PlayerPrefs.GetInt ("adIteration") >= 3) {
			notAdIteration = false;
			if (Advertisement.IsReady (null)) {
				Advertisement.Show (null, new ShowOptions {
					pause = true,
					resultCallback = ShowResult => {
						Application.LoadLevel ("menu");
						PlayerPrefs.SetInt ("adIteration", 0);
					}
				});
			} else {
				Application.LoadLevel ("menu");
				PlayerPrefs.SetInt ("adIteration", 0);
			}
		}
	}
}