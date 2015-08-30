using UnityEngine;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	public GameObject playershadow;
	public static bool isTrail;
	public static bool destroyable;
	// Use this for initialization
	void Start () {
		if (this.name == "playershadow(Clone)") {
			if(!isTrail) {
				playershadow.GetComponent<Rigidbody2D>().velocity = new Vector3 (UnityEngine.Random.Range (-2.0f, 2.0f), UnityEngine.Random.Range (-2.0f, 2.0f), UnityEngine.Random.Range (-2.0f, 2.0f));
			}
			if(destroyable)
				Destroy (this.gameObject, 0.37f);
		}
	}
}
