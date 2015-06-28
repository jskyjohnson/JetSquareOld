﻿using UnityEngine;
using System;

public class Player : MonoBehaviour {
	int score = 0;
	bool jumpLoaded;
	float cosAngle = 1.0f;
	float sinAngle = 0.0f;
	public GameObject Platform;
	public GameObject Deathblock;
	public Vector3 spawnLocation;
	public GameObject playerobject;
	public GameObject playershadow;
	public GameObject scoreGUI;
	public bool canReplicate;
	public bool right;
	public float spaceBetweenObstacles;
	public bool hasCollided;
	Quaternion platformAngle;
	void Start () {
		jumpLoaded = false;
		canReplicate = false;
		right = false;
		hasCollided = false;
		spaceBetweenObstacles = 3.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//Handles movement
		CreatePlayerShadow();
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Stationary)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2((sinAngle * 0.15f), (cosAngle * 2.8f)), ForceMode2D.Impulse);
			}
			if (touch.phase == TouchPhase.Ended) {
				jumpLoaded = false;
			}
		}
		if (Input.GetKey ("up") && jumpLoaded == true) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2((sinAngle * 0.3f), (cosAngle * 1.9f)), ForceMode2D.Impulse);
		}
		if (Input.GetKeyUp("up")) {
			jumpLoaded = false;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (!Input.GetKey("up")) {
			jumpLoaded = false;
		}
		hasCollided = false;
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		if (coll.gameObject.name == "Platform" || coll.gameObject.name == "Platform(Clone)") {
			if (!hasCollided) {
				if (jumpLoaded == false) {
					hasCollided = true;
					playerobject.GetComponent<Rigidbody2D>().isKinematic = true;
					this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				}
				Vector3 rot = coll.gameObject.transform.rotation.eulerAngles;
				cosAngle = (float)(Math.Cos (3.14f * (rot.z / 180f)));
				sinAngle = -(float)(Math.Sin (3.14f * (rot.z / 180f)));
				score += 1;
				Color platformcolor = coll.gameObject.GetComponent<SpriteRenderer> ().color;
				Color currentcolor = this.gameObject.GetComponent<SpriteRenderer> ().color;
				this.gameObject.GetComponent<SpriteRenderer> ().color = new Color ((platformcolor.r + currentcolor.r) / 2f, (platformcolor.g + currentcolor.g) / 2f, (platformcolor.b + currentcolor.b) / 2f);

				if (right == true) {
					float randomnum = UnityEngine.Random.Range (4.0F, 5.5F);
					CreatePlatform (randomnum + 1.2f, -6 + (-12 * (score + 1)), UnityEngine.Random.Range (50.0F, 70.0F), randomnum, platformcolor);
					right = false;
				} else if (right == false) {
					float randomnum = UnityEngine.Random.Range (-0F, 2F);
					CreatePlatform (randomnum - 1.2f, -6 + (-12 * (score + 1)), UnityEngine.Random.Range (290F, 310F), randomnum, platformcolor);
					right = true;
				}
				jumpLoaded = true;
				scoreGUI.GetComponent<GUIText>().text = score.ToString();
			}
		}
		if (coll.gameObject.name == "DeathBlock" || coll.gameObject.name == "DeathBlockToClone" || coll.gameObject.name == "DeathBlockToClone(Clone)") {
			Application.LoadLevel ("menu");
		}
	}
	void CreatePlatform(float locx, int locy, float angle, float randomnum, Color platformcolor) {
		spawnLocation = new Vector3 (locx, locy, 0);
		platformAngle = Quaternion.identity;
		platformAngle.eulerAngles = new Vector3(0.0f, 0.0f, angle);
		GameObject platform;
		Instantiate(Platform, spawnLocation, platformAngle);
		Debug.Log (spaceBetweenObstacles);
		if (right == true) {
			CreateObstacle (randomnum, (int)((score + 1) * (-12.0f)), spaceBetweenObstacles, platformcolor);
		} else {
			CreateObstacle(randomnum, (int)((score + 1) * (-12.0f)), spaceBetweenObstacles, platformcolor);
		}
	}
	void CreateObstacle(float spaceloc, int locy, float spacelen, Color platformcolor) {
		spawnLocation = new Vector3 ((spaceloc - (0.5f * spacelen) - 7.5f), locy, 0);
		GameObject obstacle;
		obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
		obstacle.GetComponent<SpriteRenderer> ().color = platformcolor;
		spawnLocation = new Vector3 ((spaceloc + (0.5f * spacelen) + 7.5f), locy, 0);
		obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
		obstacle.GetComponent<SpriteRenderer> ().color = platformcolor;
	}
	void CreatePlayerShadow() {
		spawnLocation = new Vector3 (this.transform.position.x, this.transform.position.y, 0);
		Instantiate(playershadow, spawnLocation, Quaternion.identity);
	}
}
