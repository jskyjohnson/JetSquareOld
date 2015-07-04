using UnityEngine;
using System;

public class Player : MonoBehaviour {
	public int score = 0;
	public int coins;
	public int initcoins = 0;

	bool jumpLoaded;
	float cosAngle = 1.0f;
	float sinAngle = 0.0f;
	public GameObject Platform;
	public GameObject Deathblock;
	public Vector3 spawnLocation;
	public GameObject playerobject;
	public GameObject playershadow;
	public GameObject scoreGUI;
	public GameObject coinGUI;
	public bool canReplicate;
	public bool right;
	public float spaceBetweenObstacles;
	public bool hasCollided;
	public Color levelBasedColor;
	public float scale;
	public GameObject Coin;
	public Camera maincamera;
	Quaternion platformAngle;
	void Start () {
		jumpLoaded = false;
		canReplicate = false;
		right = false;
		hasCollided = false;
		spaceBetweenObstacles = 3.5f;
		scale = 2.5f;
		coins = PlayerPrefs.GetInt ("coins", 0);
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
			GetComponent<Rigidbody2D>().AddForce(new Vector2((sinAngle * 0.30f), (cosAngle * 1.8f)), ForceMode2D.Impulse);
		}
		if (Input.GetKey ("up")) {
			//
			//this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
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

	public void OnTriggerEnter2D(Collider2D coll){ //This is called when an object collides with something but goes through
		Debug.Log ("trigged m8");
		if (coll.gameObject.name == "Coin" || coll.gameObject.name == "Coin(Clone)") {
			Debug.Log ("omg munie");
			coins += 1;
			initcoins += 1;
			Debug.Log ("+1 munie lol");
			Destroy(coll.gameObject);
			Debug.Log ("rip");
			coinGUI.GetComponent<GUIText>().text = initcoins.ToString();
		}
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		if (score < 10) {
			spaceBetweenObstacles = 3.5f;
			scale = 1.9f;
			levelBasedColor = new Color(0.46666f, 0.88235f, 0.866666f); //baby blue/green
			maincamera.backgroundColor = levelBasedColor;
		} else if (score < 20 && score >= 10) {
			scale = 1.8f;
			spaceBetweenObstacles = 3.2f;
			levelBasedColor = new Color(0.8823f, 0.4666f, 0.4666f); //red
			maincamera.backgroundColor = levelBasedColor;
		} else if (score < 30 && score >= 20) {
			scale = 1.7f;
			spaceBetweenObstacles = 3.0f;
			levelBasedColor = new Color(0.88235f, 0.4666f, 0.83529f);//pink
		} else if (score < 40 && score >= 30) {
			scale = 1.6f;
			spaceBetweenObstacles = 2.8f;
			levelBasedColor = new Color(1f, 0.6f, 0.6f);
		} else if (score < 50 && score >= 40) {
			scale = 1.5f;
			spaceBetweenObstacles = 2.6f;
			levelBasedColor = new Color(1f, 0.6f, 1f);
		} else {
			scale = 1.4f;
			spaceBetweenObstacles = 2.3f;
			levelBasedColor = new Color(0.6f, 1f, 1f);
		}

		if (coll.gameObject.name == "Platform" || coll.gameObject.name == "Platform(Clone)") {
			if (!hasCollided) {
				if (jumpLoaded == false) {
					hasCollided = true;
					//playerobject.GetComponent<Rigidbody2D>().isKinematic = true;
					playerobject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
					playerobject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				}
				Vector3 rot = coll.gameObject.transform.rotation.eulerAngles;
				cosAngle = (float)(Math.Cos (3.14f * (rot.z / 180f)));
				sinAngle = -(float)(Math.Sin (3.14f * (rot.z / 180f)));
				if(coll.gameObject.GetComponent<PlatformScript>().givenScore == false) {
					score += 1;
					coll.gameObject.GetComponent<PlatformScript>().givenScore = true;
					//Color platformcolor = coll.gameObject.GetComponent<SpriteRenderer> ().color;
					//Color currentcolor = this.gameObject.GetComponent<SpriteRenderer> ().color;
					//this.gameObject.GetComponent<SpriteRenderer> ().color = new Color ((platformcolor.r + currentcolor.r) / 2f, (platformcolor.g + currentcolor.g) / 2f, (platformcolor.b + currentcolor.b) / 2f);
					
					if (right == true) {
						float randomnum = UnityEngine.Random.Range (4.0F, 5.5F);
						CreatePlatform (randomnum + 0.7f, -5 + (-10 * (score + 1)), UnityEngine.Random.Range (50.0F, 65.0F), randomnum, levelBasedColor, scale);
						right = false;
					} else if (right == false) {
						float randomnum = UnityEngine.Random.Range (0.6F, 2F);
						CreatePlatform (randomnum - 0.7f, -5 + (-10 * (score + 1)), UnityEngine.Random.Range (295F, 310F), randomnum, levelBasedColor, scale);
						right = true;
					}
				}
				jumpLoaded = true;
				scoreGUI.GetComponent<GUIText>().text = score.ToString();
			}
		}
		if (coll.gameObject.name == "DeathBlock" || coll.gameObject.name == "DeathBlockToClone" || coll.gameObject.name == "DeathBlockToClone(Clone)") {
			StoreValues(score, coins);
			Application.LoadLevel ("menu");
		}
	}
	void CreatePlatform(float locx, int locy, float angle, float randomnum, Color platformcolor, float scale) {
		spawnLocation = new Vector3 (locx, locy, 0);
		platformAngle = Quaternion.identity;
		platformAngle.eulerAngles = new Vector3(0.0f, 0.0f, angle);
		GameObject platform;
		platform = (GameObject)Instantiate(Platform, spawnLocation, platformAngle);
		//platform.GetComponent<SpriteRenderer> ().color = platformcolor;
		Vector3 platformScale = platform.transform.localScale;
		platformScale.x = scale;
		platform.transform.localScale = platformScale;
		if (right == true) {
			CreateObstacle (randomnum, (int)((score + 1) * (-10.0f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (-0.5f + randomnum + UnityEngine.Random.Range (-spaceBetweenObstacles/2f, spaceBetweenObstacles/2f), UnityEngine.Random.Range (-3.0f, 3.0f) + (float)(-10 * (score + 1)));
		} else {
			CreateObstacle(randomnum, (int)((score + 1) * (-10.0f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (0.5f + randomnum + UnityEngine.Random.Range (-spaceBetweenObstacles/2f, spaceBetweenObstacles/2f), UnityEngine.Random.Range (-3.0f, 3.0f) + (float)(-10 * (score + 1)));
		}
	}
	void CreateObstacle(float spaceloc, int locy, float spacelen, Color platformcolor) {
		spawnLocation = new Vector3 ((spaceloc - (0.5f * spacelen) - 7.5f), locy, 0);
		GameObject obstacle;
		obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
		//obstacle.GetComponent<SpriteRenderer> ().color = platformcolor;
		spawnLocation = new Vector3 ((spaceloc + (0.5f * spacelen) + 7.5f), locy, 0);
		obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
		//obstacle.GetComponent<SpriteRenderer> ().color = platformcolor;
	}
	void CreatePlayerShadow() {
		spawnLocation = new Vector3 (this.transform.position.x, this.transform.position.y, 0);
		Instantiate(playershadow, spawnLocation, Quaternion.identity);
	}
	void StoreValues(int newHighscore, int newCoins)
	{
		int oldHighscore = PlayerPrefs.GetInt("highscore", 0);    
		if(newHighscore > oldHighscore)
			PlayerPrefs.SetInt("highscore", newHighscore);
		PlayerPrefs.SetInt("coins", newCoins);
		PlayerPrefs.Save ();
	}
	void CreateCoin(float locx, float locy) {
		spawnLocation = new Vector3 (locx, locy, 0);
		Instantiate(Coin, spawnLocation, Quaternion.identity);
	}
}

/*
 * 1-10 Easy 10-20 Medium 20-30 Hard 30-40 Very hard 40-50 Elite 50+ yolo mode
*/
