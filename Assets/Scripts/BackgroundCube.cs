using UnityEngine;
using System.Collections;

public class BackgroundCube : MonoBehaviour {
	public Rigidbody rb;
	public Light backlight;
	public int timer;
	public float torquetimeout = 0.42857142857f;
	public Camera mainCam;
	public bool isScene1;
	private float timeremaining;
	// Use this for initialization
	void Start () {
		timeremaining = torquetimeout;
		rb = GetComponent<Rigidbody> ();
		timer = 0;
		setColor (mainCam.backgroundColor);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isScene1) {
			if (timeremaining < 0f) {
				Vector3 randVec = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));
				rb.AddTorque (randVec*30f, ForceMode.Acceleration);
				timeremaining = torquetimeout;
			} else {
				timeremaining -= Time.deltaTime;
			}
		}
	}
	public void randTorHit(){
		Vector3 randVec = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		rb.AddTorque (randVec*30f, ForceMode.Acceleration);
	}
	public void randTorHitCoin(){
		Vector3 randVec = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		rb.AddTorque (randVec*10f, ForceMode.Acceleration);
	}
	public void setColor(Color color){
		backlight.color = color;
	}
}
