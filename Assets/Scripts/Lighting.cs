using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {
	public Light thislight;
	public Light light1;
	public Light light2;
	public Light light3;
	public Light light4;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		light1.color = thislight.color;
		light2.color = thislight.color;
		light3.color = thislight.color;
		light4.color = thislight.color;
	}
}
