using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
	public bool givenScore;
	public bool hasCollided;
	public Vector3 contactpoint;
	public GameObject player;
	// Use this for initialization
	void Start () {
		givenScore = false;
		hasCollided = false;
	}
}
