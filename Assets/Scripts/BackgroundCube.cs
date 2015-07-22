using UnityEngine;
using System.Collections;

public class BackgroundCube : MonoBehaviour {
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void randTor(){
		Vector3 randVec = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
		rb.AddTorque (randVec*Random.Range (0, 20), ForceMode.Acceleration);
	}
}
