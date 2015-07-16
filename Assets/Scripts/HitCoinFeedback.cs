using UnityEngine;
using System.Collections;

public class HitCoinFeedback : MonoBehaviour {
	public int timer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 10) {
			Destroy (this);
		} else {
			timer++;
		}
	}
}
