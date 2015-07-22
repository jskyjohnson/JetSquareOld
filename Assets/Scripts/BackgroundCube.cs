using UnityEngine;
using System.Collections;

public class BackgroundCube : MonoBehaviour {
	public Rigidbody rb;
	public Light backlight;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void randTorHit(){
		Vector3 randVec = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
		rb.AddTorque (randVec*10, ForceMode.Acceleration);
	}
	public void randTorHitCoin(){
		Vector3 randVec = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
		rb.AddTorque (randVec*20, ForceMode.Acceleration);
	}
	public void setColor(Color color){
		backlight.color = color;
	}
}
