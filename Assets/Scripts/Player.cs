using UnityEngine;
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
	public bool canReplicate;
	public bool right;
	public float spaceBetweenObstacles = 3.0f;
	Quaternion platformAngle;
	void Start () {
		jumpLoaded = false;
		canReplicate = false;
		right = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Handles movement
		CreatePlayerShadow();
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Stationary)
			{
				rigidbody2D.AddForce(new Vector2((sinAngle * 0.1f), (cosAngle * 2.8f)), ForceMode2D.Impulse);
			}
			if (touch.phase == TouchPhase.Ended) {
				jumpLoaded = false;
			}
		}
		Debug.Log (jumpLoaded);
		if (Input.GetKey ("up") && jumpLoaded == true) {
			rigidbody2D.AddForce(new Vector2((sinAngle * 0.3f), (cosAngle * 1.7f)), ForceMode2D.Impulse);
		}
		if (Input.GetKeyUp("up")) {
			jumpLoaded = false;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (!Input.GetKey("up")) {
			jumpLoaded = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		if (coll.gameObject.name == "Platform" || coll.gameObject.name == "Platform(Clone)") {
			Vector3 rot = coll.gameObject.transform.rotation.eulerAngles;
			cosAngle = (float)(Math.Cos (3.14f * (rot.z / 180f)));
			sinAngle = -(float)(Math.Sin (3.14f * (rot.z / 180f)));
			if (jumpLoaded == false) {
				this.gameObject.rigidbody2D.isKinematic = true;
				this.gameObject.rigidbody2D.isKinematic = false;
			}
			score += 1;
			Color platformcolor = coll.gameObject.GetComponent<SpriteRenderer> ().color;
			Color currentcolor = this.gameObject.GetComponent<SpriteRenderer> ().color;
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color((platformcolor.r + currentcolor.r)/2f, (platformcolor.g + currentcolor.g)/2f, (platformcolor.b + currentcolor.b)/2f);

			if (right == true) {
				float randomnum = UnityEngine.Random.Range (4.0F, 5.5F);
				CreatePlatform (randomnum + 1.2f, -6 + (-12 * (score + 1)), UnityEngine.Random.Range(50.0F, 60.0F), randomnum, platformcolor);
				right = false;
				Debug.Log ("Generated one on left");
			} else if (right == false) {
				float randomnum = UnityEngine.Random.Range (-0F, 2F);
				CreatePlatform (randomnum -1.2f, -6 + (-12 * (score + 1)), UnityEngine.Random.Range(300F, 310F), randomnum, platformcolor);
				right = true;
				Debug.Log ("Generated one on right");
			}
			jumpLoaded = true;
		}
		if (coll.gameObject.name == "DeathBlock" || coll.gameObject.name == "DeathBlockToClone" || coll.gameObject.name == "DeathBlockToClone(Clone)") {
			//if(coll.gameObject.transform.position.y < ((10 * (-score + 1)) - 5) && coll.gameObject.transform.position.y > (10 * (-score) - 5)) {
			Application.LoadLevel (Application.loadedLevel);
			//}
		}
	}
	void CreatePlatform(float locx, int locy, float angle, float randomnum, Color platformcolor) {
		spawnLocation = new Vector3 (locx, locy, 0);
		platformAngle = Quaternion.identity;
		platformAngle.eulerAngles = new Vector3(0.0f, 0.0f, angle);
		GameObject platform;
		Instantiate(Platform, spawnLocation, platformAngle);
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
