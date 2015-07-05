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
	public int coloralternation = 1;

	//sprites
	public static string currentSpriteName;
	public Sprite Default;
	public Sprite CirclePlayer;


	Quaternion platformAngle;
	void Start () {
		jumpLoaded = false;
		canReplicate = false;
		right = false;
		hasCollided = false;
		spaceBetweenObstacles = 3.5f;
		scale = 2.5f;
		coins = PlayerPrefs.GetInt ("coins", 0);
		Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
		playershadowcolor = new Color(1f, 1f, 1f);
		playershadowcolor.a = 0.2f;
		playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
		currentSpriteName = PlayerPrefs.GetString ("currentskin");
		loadSkin ();
	}
	
	// Update is called once per frame
	void Update () {
		//Handles movement
		CreatePlayerShadow();
		/*
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Stationary && jumpLoaded == true)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2((sinAngle * 0.30f), (cosAngle * 1.8f)), ForceMode2D.Impulse);
			}
			if (touch.phase == TouchPhase.Stationary) {
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(coloralternation >= 1) {
					playershadowcolor = new Color(1f, 0.54f, 0.54f);
					coloralternation += 1;
				} else if(coloralternation == 8) {
					playershadowcolor = new Color(0.949f, 0.572f, 0.286f);
					coloralternation = 1;
				}
				playershadowcolor.a = 0.7f;
				Vector3 playershadowscale = playershadow.transform.localScale;
				playershadowscale.x = 0.35f;
				playershadowscale.y = 0.35f;
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			}
			if (touch.phase == TouchPhase.Ended) {
				jumpLoaded = false;
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				playershadowcolor = new Color(1f, 1f, 1f);
				playershadowcolor.a = 0.2f;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
				Vector3 playershadowscale = playershadow.transform.localScale;
				playershadowscale.x = 0.3f;
				playershadowscale.y = 0.3f;
				playershadow.transform.localScale = playershadowscale;
			}
		}*/
		if (Input.GetKey ("up") && jumpLoaded == true) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2((sinAngle * 0.30f), (cosAngle * 1.8f)), ForceMode2D.Impulse);
		}
		if (Input.GetKey ("up")) {
			Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
			if(coloralternation >= 1) {
				playershadowcolor = new Color(1f, 0.54f, 0.54f);
				coloralternation += 1;
			} else if(coloralternation == 8) {
				playershadowcolor = new Color(0.949f, 0.572f, 0.286f);
				coloralternation = 1;
			}
			playershadowcolor.a = 0.7f;
			Vector3 playershadowscale = playershadow.transform.localScale;
			playershadowscale.x = 0.35f;
			playershadowscale.y = 0.35f;
			playershadow.transform.localScale = playershadowscale;
			playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			//
			//this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		}
		if (Input.GetKeyUp("up")) {
			jumpLoaded = false;
			Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
			playershadowcolor = new Color(1f, 1f, 1f);
			playershadowcolor.a = 0.2f;
			playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			Vector3 playershadowscale = playershadow.transform.localScale;
			playershadowscale.x = 0.3f;
			playershadowscale.y = 0.3f;
			playershadow.transform.localScale = playershadowscale;
		}
		maincamera.backgroundColor = Color.Lerp(maincamera.backgroundColor, levelBasedColor, Time.deltaTime);
	}

	void OnCollisionExit2D(Collision2D coll) {
		/*foreach (Touch touch in Input.touches)
		{
			if (!(touch.phase == TouchPhase.Stationary))
			{
				jumpLoaded = false;
			}
		}*/
		if (!Input.GetKey("up")) { //uncomment this on mobile
			jumpLoaded = false;
		}
		hasCollided = false;
	}

	public void OnTriggerEnter2D(Collider2D coll){ //This is called when an object collides with something but goes through
		if (coll.gameObject.name == "Coin" || coll.gameObject.name == "Coin(Clone)") {
			coins += 1;
			initcoins += 1;
			Destroy(coll.gameObject);
			coinGUI.GetComponent<GUIText>().text = initcoins.ToString();
		}
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		if (score < 10) {
			spaceBetweenObstacles = 3.5f;
			scale = 1.1f;
			levelBasedColor = new Color(0.46666f, 0.88235f, 0.866666f); //baby blue/green
		} else if (score < 25 && score >= 10) {
			scale = 1.0f;
			spaceBetweenObstacles = 3.2f;
			//previousColor = levelBasedColor;
			levelBasedColor = new Color(0.50196f, 0.8313f, 0.61176f); //green
		} else if (score < 45 && score >= 25) {
			scale = 0.9f;
			spaceBetweenObstacles = 3.0f;
			levelBasedColor = new Color(0.8823f, 0.8705f, 0.6039f);//yellow
		} else if (score < 70 && score >= 45) {
			scale = 0.8f;
			spaceBetweenObstacles = 2.6f;
			levelBasedColor = new Color(0.5137f, 0.50196f, 0.83137f); //purple
		} else if (score < 100 && score >= 70) {
			scale = 0.6f;
			spaceBetweenObstacles = 2.4f;
			levelBasedColor = new Color(0.8823f, 0.4666f, 0.4666f); //red
		} else {
			scale = 0.6f;
			spaceBetweenObstacles = 2.0f;
			levelBasedColor = new Color(0.6f, 1f, 1f);
			levelBasedColor = new Color(0.88235f, 0.4666f, 0.83529f);//pink
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
						float randomnum = UnityEngine.Random.Range (4.6F, 5.9F);
						CreatePlatform (randomnum + 0.8f + 3.0f, -7 + (-10 * (score + 1)), UnityEngine.Random.Range (50.0F, 60.0F), randomnum, levelBasedColor, scale);
						right = false;
					} else if (right == false) {
						float randomnum = UnityEngine.Random.Range (0F, 1.3F);
						CreatePlatform (randomnum - 0.8f - 3.0f, -7 + (-10 * (score + 1)), UnityEngine.Random.Range (300F, 310F), randomnum, levelBasedColor, scale);
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
			CreateCoin (randomnum + UnityEngine.Random.Range ((-spaceBetweenObstacles/2f) + 0.5f, (spaceBetweenObstacles/2f) - 0.5f), UnityEngine.Random.Range (-3.0f, 3.0f) + (float)(-10 * (score + 1)));
		} else {
			CreateObstacle(randomnum, (int)((score + 1) * (-10.0f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (randomnum + UnityEngine.Random.Range ((-spaceBetweenObstacles/2f) + 0.5f, (spaceBetweenObstacles/2f) - 0.5f), UnityEngine.Random.Range (-3.0f, 3.0f) + (float)(-10 * (score + 1)));
		}
	}
	void CreateObstacle(float spaceloc, int locy, float spacelen, Color platformcolor) {
		spawnLocation = new Vector3 ((spaceloc - (0.5f * spacelen) - 6.1f), locy, 0);
		GameObject obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
		//obstacle.GetComponent<SpriteRenderer> ().color = platformcolor;
		spawnLocation = new Vector3 ((spaceloc + (0.5f * spacelen) + 6.1f), locy, 0);
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
	//loads the proper skin depending on the current configuration.
	public void loadSkin() {
		Debug.Log (currentSpriteName);
		switch (currentSpriteName) {
			//cases have to be hardcoded because each case differs in some special way.
			case "Default":
				Debug.Log ("called");
				playerobject.GetComponent<SpriteRenderer>().sprite = Default;
			break;
			case "CirclePlayer":
				playerobject.GetComponent<SpriteRenderer>().sprite = CirclePlayer;
			break;
		}
	}
}

/*
 * 1-10 Easy 10-20 Medium 20-30 Hard 30-40 Very hard 40-50 Elite 50+ yolo mode
*/
