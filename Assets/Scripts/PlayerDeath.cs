using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	private float counter;
	public AudioSource deathsound;
	public static PlayerDeath instance;
	public Camera maincamera;
	void Start () {
		counter = 0f;
		//deathsound.Play ();


	}
	
	// Update is called once per frame
	void Update () {

		counter += Time.deltaTime;
		if (counter > 0.3f && AdsManager.notAdIteration) {
			Application.LoadLevel ("menu");
		}
	}
}
