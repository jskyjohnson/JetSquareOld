using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	public GameObject playershadow;
	// Use this for initialization
	void Start () {
		playershadow.GetComponent<Rigidbody2D>().velocity = new Vector3 (UnityEngine.Random.Range (-5.0f, 5.0f), UnityEngine.Random.Range (-5.0f, 5.0f), UnityEngine.Random.Range (-5.0f, 5.0f));
		if (this.name == "playershadow(Clone)") {
			Destroy (this.gameObject, 0.3f);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
