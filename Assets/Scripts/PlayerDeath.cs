using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	private int counter;
	public AudioSource deathsound;
	public static PlayerDeath instance;
	public Camera maincamera;
	void Start () {
		counter = 0;
		deathsound.Play ();


	}
	
	// Update is called once per frame
	void Update () {

		counter++;
		if (counter > 75 && AdsManager.notAdIteration) {
			Application.LoadLevel ("menu");
		}
	}
}
