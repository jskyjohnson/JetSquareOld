using UnityEngine;
using System.Collections;

public class HitPlatformFeedback : MonoBehaviour {
	int timer = 0;
	// Update is called once per frame
	void Update () {
		if (timer > 200) {
			Destroy (this);
		} else {
			timer++;
		}
	}
}
