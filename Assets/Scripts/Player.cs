using UnityEngine;
using System;

public class Player : MonoBehaviour {
	public int score = 0;
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
	public Color levelBasedColor;
	public float scale;
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
		if (score < 10) {
			spaceBetweenObstacles = 4.0f;
			scale = 2.3f;
			levelBasedColor = new Color(1f, 1f, 1f);
		} else if (score < 20 && score >= 10) {
			scale = 2.0f;
			spaceBetweenObstacles = 3.5f;
			levelBasedColor = new Color(0.6f, 0.6f, 1f);
		} else if (score < 30 && score >= 20) {
			scale = 1.8f;
			spaceBetweenObstacles = 3.0f;
			levelBasedColor = new Color(0.6f, 1f, 1f);
		} else if (score < 40 && score >= 30) {
			scale = 1.6f;
			spaceBetweenObstacles = 2.5f;
			levelBasedColor = new Color(1f, 0.6f, 0.6f);
		} else if (score < 50 && score >= 40) {
			scale = 1.4f;
			spaceBetweenObstacles = 2.25f;
			levelBasedColor = new Color(1f, 0.6f, 1f);
		} else {
			scale = 1.2f;
			spaceBetweenObstacles = 2.0f;
			levelBasedColor = new Color(0.6f, 1f, 1f);
		}
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
					CreatePlatform (randomnum + 1.1f, -6 + (-12 * (score + 1)), UnityEngine.Random.Range (40.0F, 60.0F), randomnum, levelBasedColor, scale);
					right = false;
				} else if (right == false) {
					float randomnum = UnityEngine.Random.Range (-0F, 2F);
					CreatePlatform (randomnum - 1.1f, -6 + (-12 * (score + 1)), UnityEngine.Random.Range (300F, 320F), randomnum, levelBasedColor, scale);
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
	void CreatePlatform(float locx, int locy, float angle, float randomnum, Color platformcolor, float scale) {
		spawnLocation = new Vector3 (locx, locy, 0);
		platformAngle = Quaternion.identity;
		platformAngle.eulerAngles = new Vector3(0.0f, 0.0f, angle);
		GameObject platform;
		platform = (GameObject)Instantiate(Platform, spawnLocation, platformAngle);
		platform.GetComponent<SpriteRenderer> ().color = platformcolor;
		Vector3 platformScale = platform.transform.localScale;
		platformScale.x = scale;
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

/*
 * 1-10 Easy 10-20 Medium 20-30 Hard 30-40 Very hard 40-50 Elite 50+ yolo mode
*/
