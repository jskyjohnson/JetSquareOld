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
		if ((PlayerPrefs.GetInt ("GameTime") >= 50 || PlayerPrefs.GetInt ("adIteration") >= 6) && PlayerPrefs.GetString("ads") != "false") {
			float randomNum = UnityEngine.Random.Range (0f, 100f);
			if(randomNum > 40f) {
				if(Advertisement.isReady("pictureZone")) {
					Debug.Log ("showing picture");
					Advertisement.Show ("pictureZone", new ShowOptions {
						resultCallback = ShowResult => {
							PlayerPrefs.SetInt ("GameTime", 0);
							PlayerPrefs.SetInt ("adIteration", 0);
							Application.LoadLevel ("Scene1");
						}
					});
				} else {
					if (Advertisement.IsReady (null)) {
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
			} else {
				if(Advertisement.isReady(null)) {
					Advertisement.Show (null, new ShowOptions {
						pause = true,
						resultCallback = ShowResult => {
							PlayerPrefs.SetInt ("GameTime", 0);
							PlayerPrefs.SetInt ("adIteration", 0);
							Application.LoadLevel ("Scene1");
						}
					});
				} else {
					if (Advertisement.IsReady ("pictureZone")) {
						Advertisement.Show ("pictureZone", new ShowOptions {
							pause = true,
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
			}
		}
	}
}