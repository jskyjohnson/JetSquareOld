﻿using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	private float counter;
	public AudioSource deathsound;
	public static PlayerDeath instance;
	void Start () {
		counter = 0f;
		//deathsound.Play ();


	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter > 0.3f) {
			Debug.Log (PlayerPrefs.GetInt ("adIteration"));
			if(PlayerPrefs.GetInt ("adIteration") < 4 || PlayerPrefs.GetString("ads") == "false") {
				Application.LoadLevel ("Scene1");
			}
			AdsManager.loadAd();
			Destroy (this);
		}
	}
}
