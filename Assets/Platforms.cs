using UnityEngine;
using System;

public class Platforms : MonoBehaviour {
	public GameObject Platform;
	public Vector3 spawnLocation;
	public GameObject player;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	} 

	void CreatePlatform() {
		spawnLocation = new Vector3 (0, 0, 0);//Random.value * 10, 0);
		Instantiate(Platform, spawnLocation, Quaternion.identity);
	}
}
