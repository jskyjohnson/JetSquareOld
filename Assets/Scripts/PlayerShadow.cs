using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (this.name == "playershadow(Clone)") {
			Destroy (this.gameObject, 0.08f);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
