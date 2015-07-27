using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	private float counter;
	public AudioSource deathsound;
	public static PlayerDeath instance;
	void Start () {
		counter = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter > 0.3f) {
			if(!Player.guardianAngel) {
				Debug.Log (PlayerPrefs.GetInt ("adIteration"));
				PlayerPrefs.SetInt ("adIteration", PlayerPrefs.GetInt ("adIteration") + 1);
				if(PlayerPrefs.GetInt ("GameTime") < 40 && PlayerPrefs.GetInt ("adIteration") < 6 || PlayerPrefs.GetString("ads") == "false") {
					Application.LoadLevel ("Scene1");
				}
				AdsManager.loadAd();
			}
			Destroy (this);
		}
	}
}
