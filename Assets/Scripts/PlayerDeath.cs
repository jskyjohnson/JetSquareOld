using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	private int counter;
	public AudioSource deathsound;
	void Start () {
		counter = 0;
		deathsound.Play ();
	}
	
	// Update is called once per frame
	void Update () {

		counter++;
		if (counter > 75) {

			Application.LoadLevel ("menu");
		}
	}
}
