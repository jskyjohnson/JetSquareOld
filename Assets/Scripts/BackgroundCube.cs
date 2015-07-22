using UnityEngine;
using System.Collections;

public class BackgroundCube : MonoBehaviour {
	public Rigidbody rb;
	public Light backlight;
	public int timer;
	public float torquetimeout = 0.42857142857f;

	private float timeremaining;
	// Use this for initialization
	void Start () {
		timeremaining = torquetimeout;
		rb = GetComponent<Rigidbody> ();
		timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {



		if (timeremaining < 0f) {
			Debug.Log ("asdfasdf");
			Vector3 randVec = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1) * Time.deltaTime);
			rb.AddTorque (randVec * 2, ForceMode.VelocityChange);
			timeremaining = torquetimeout;
		} else {
			timeremaining -= Time.deltaTime;
		}
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
