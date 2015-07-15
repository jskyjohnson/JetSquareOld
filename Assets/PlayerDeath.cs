using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	private int counter;

	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		counter++;
		if (counter > 150) {

			Application.LoadLevel ("menu");
		}
	}
}
