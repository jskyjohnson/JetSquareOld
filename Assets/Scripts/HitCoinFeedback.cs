using UnityEngine;
using System.Collections;

public class HitCoinFeedback : MonoBehaviour {
	public int timer = 0;
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody2D>().velocity = new Vector3 (UnityEngine.Random.Range (-5.0f, 5.0f), UnityEngine.Random.Range (-5.0f, 5.0f), UnityEngine.Random.Range (-5.0f, 5.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 100) {
			Destroy (this);
		} else {
			timer++;
		}
	}
}
