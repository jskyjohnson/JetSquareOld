using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public int score = 0;
	public int coins;
	public int initcoins = 0;

	public int jumpsLoaded;
	float cosAngle = 1.0f;
	float sinAngle = 0.0f;
	//Game Objects
	public GameObject Platform;
	public GameObject Deathblock;
	public GameObject playerobject;
	public GameObject playershadow;
	public GameObject scoreGUI;
	public GameObject coinGUI;
	public GameObject Coin;
	private GameObject obstacle;
	public Camera maincamera;

	public GameObject deathAnimation;
	//Logic Variables
	public bool canReplicate;
	public bool right;
	public float spaceBetweenObstacles;
	public float scale;
	public Color levelBasedColor;
	public Color coinColor;
	public int coloralternation = 1;
	public int coinValue;
	public Vector3 spawnLocation;
	public float lastPoint;
	public bool generated;
	public int generatedNumber;
	public float jumpPowerInTime;
	//powerup controller variables
	public bool infiniteJumpAllowed;
	public bool coinMagnet;
	public bool passObstacles;
	public float areaSection;
	//sprites
	public static string currentSpriteName;
	public Sprite Default;
	public Sprite CirclePlayer;
	public Sprite TwoSquarePlayer;
	public Sprite DogePlayer;
	//shadows
	public Sprite CircleShadow;
	public FeedBackManager feedbackmanager;

	//audio
	public AudioSource fxSource;
	public AudioSource musicSource;
	public AudioSource coinSource;

	public AudioClip music;
	public AudioClip HitPlatform;
	public AudioClip HitCoin;
	public AudioClip HitDeathBlock;

	public float lowPitch = .95f;
	public float highPitch = 1.05f;

	private bool dieing; 
	private int dieingwaiter;
	Quaternion platformAngle;
	void Start () {
		musicSource.clip = music;
		musicSource.Play ();
		dieingwaiter = 0;
		dieing = false;
		jumpsLoaded = 0;
		infiniteJumpAllowed = false;
		coinMagnet = false;
		canReplicate = false;
		right = false;
		spaceBetweenObstacles = 3.5f;
		scale = 2.5f;
		coins = PlayerPrefs.GetInt ("coins", 0);
		Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
		playershadowcolor = new Color(1f, 1f, 1f);
		playershadowcolor.a = 0.2f;
		playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
		currentSpriteName = PlayerPrefs.GetString ("currentskin");
		loadSkin ();
		lastPoint = playerobject.transform.position.y;
		generated = false;
		generatedNumber = 2;
		passObstacles = false;
		areaSection = 0f;
		jumpPowerInTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		//Handles movement
		if ((playerobject.transform.position.y < lastPoint - 4.0f) && generated == false) {
			generated = true;
			areaSection += 1f;
			if (right == true) {
				float randomnum = UnityEngine.Random.Range (4.4F, 7.8F);
				CreatePlatform (randomnum + 0.8f + 3.0f, -6 + (-8 * (generatedNumber)), UnityEngine.Random.Range (42.0F, 62.0F), randomnum, levelBasedColor, scale);
				right = false;
			} else if (right == false) {
				float randomnum = UnityEngine.Random.Range (-2.0F, 1.5F);
				CreatePlatform (randomnum - 0.8f - 3.0f, -6 + (-8 * (generatedNumber)), UnityEngine.Random.Range (298F, 318F), randomnum, levelBasedColor, scale);
				right = true;
			}
		}
		if ((playerobject.transform.position.y < lastPoint - 8.0f)) {
			generated = false;
			lastPoint = playerobject.transform.position.y;
			generatedNumber += 1;
		}
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

			if (Input.GetKey ("up") && jumpsLoaded == 1) {
				//GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((sinAngle * 0.30f), (cosAngle * 1.8f)), ForceMode2D.Impulse);
				jumpPowerInTime += Time.deltaTime;
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
					//playershadowcolor = new Color (1f, 0.54f, 0.54f);
				playershadowcolor = new Color ((0.929f + jumpPowerInTime * 0.056f), (0.54f - jumpPowerInTime * 0.52f), 0.54f - jumpPowerInTime * 0.52f);
				playershadowcolor.a = 0.2f + jumpPowerInTime * 2f;
				Vector3 playershadowscale = playershadow.transform.localScale;
				playershadowscale.x = 0.35f;
				playershadowscale.y = 0.35f;
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			Debug.Log (playershadow.GetComponent<SpriteRenderer> ().color);	
			//this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			}
			if (Input.GetKeyUp ("up")) {
				if (!infiniteJumpAllowed) {
					jumpsLoaded = 0;
				}
				Debug.Log ("Adding " + jumpPowerInTime + " force to playerobject");
				playerobject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((sinAngle * 25f * jumpPowerInTime), (cosAngle * 69f) * jumpPowerInTime), ForceMode2D.Impulse);
				jumpPowerInTime = 0f;
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				playershadowcolor = new Color (1f, 1f, 1f);
				playershadowcolor.a = 0.2f;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
				Vector3 playershadowscale = playershadow.transform.localScale;
				playershadowscale.x = 0.3f;
				playershadowscale.y = 0.3f;
				playershadow.transform.localScale = playershadowscale;
			}
		if (passObstacles == true) {
			foreach (GameObject deathblock in GameObject.FindGameObjectsWithTag("DeathBlock")) {
				deathblock.GetComponent<Collider2D> ().isTrigger = true;
				Color deathblockcolor = deathblock.GetComponent<SpriteRenderer>().color;
				deathblockcolor.a = 0.5f;
				deathblock.GetComponent<SpriteRenderer>().color = deathblockcolor;
			}
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
	}

	public void OnTriggerEnter2D(Collider2D coll){ //This is called when an object collides with something but goes through
		if (coll.gameObject.name == "Coin" || coll.gameObject.name == "Coin(Clone)") {
			feedbackmanager.hitCoin(this);
			coinSource.clip=HitCoin;
			coinSource.Play ();

			coins += coinValue;
			initcoins += coinValue;
			Destroy(coll.gameObject);
			coinGUI.GetComponent<GUIText>().text = initcoins.ToString();
		}
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		if (score < 10) {
			coinValue = 1;
			spaceBetweenObstacles = 4f;
			scale = 1.3f;
			coinColor = new Color(1f, 1f, 1f);
			levelBasedColor = new Color(0.46666f, 0.88235f, 0.866666f); //baby blue/green
		} else if (score < 25 && score >= 10) {
			coinValue = 2;
			scale = 1.2f;
			spaceBetweenObstacles = 3.6f;
			coinColor = new Color(0.847f, 0.255f, 0.255f);
			//previousColor = levelBasedColor;
			levelBasedColor = new Color(0.50196f, 0.8313f, 0.61176f); //green
		} else if (score < 45 && score >= 25) {
			coinValue = 2;
			scale = 0.9f;
			spaceBetweenObstacles = 3.2f;
			levelBasedColor = new Color(0.8823f, 0.8705f, 0.6039f);//yellow
		} else if (score < 70 && score >= 45) {
			coinValue = 3;
			scale = 0.8f;
			coinColor = new Color(0.0705f, 0.2902f, 0.941f);
			spaceBetweenObstacles = 2.9f;
			levelBasedColor = new Color(0.5137f, 0.50196f, 0.83137f); //purple
		} else if (score < 100 && score >= 70) {
			coinValue = 3;
			scale = 0.6f;
			spaceBetweenObstacles = 2.7f;
			levelBasedColor = new Color(0.8823f, 0.4666f, 0.4666f); //red
		} else {
			coinValue = 4;
			scale = 0.6f;
			coinColor = new Color(0.941f, 0.0705f, 0.7725f);
			spaceBetweenObstacles = 2.5f;
			levelBasedColor = new Color(0.6f, 1f, 1f);
			levelBasedColor = new Color(0.88235f, 0.4666f, 0.83529f);//pink
		}

		if (coll.gameObject.name == "Platform" || coll.gameObject.name == "Platform(Clone)") {
			feedbackmanager.hitPlatform(this);
			if (!coll.gameObject.GetComponent<PlatformScript>().hasCollided) {
				RandomiseAudio(HitPlatform);
				playerobject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				playerobject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				if (jumpsLoaded == 0) {
					coll.gameObject.GetComponent<PlatformScript>().hasCollided = true;
					//playerobject.GetComponent<Rigidbody2D>().isKinematic = true;
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
				}
				jumpsLoaded = 1;
				scoreGUI.GetComponent<GUIText>().text = score.ToString();
			}
		}
		if (coll.gameObject.name == "DeathBlock" || coll.gameObject.name == "DeathBlockToClone" || coll.gameObject.name == "DeathBlockToClone(Clone)") {
			Instantiate(deathAnimation,this.transform.position,this.transform.rotation);
			StoreValues (score, coins);
			gameObject.active = false;
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
			CreateObstacle (randomnum, (int)((generatedNumber) * (-8.0f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (randomnum + UnityEngine.Random.Range ((-spaceBetweenObstacles/2f) + 0.5f, (spaceBetweenObstacles/2f) - 0.5f), UnityEngine.Random.Range (-2.0f, 2.0f) + (float)(-8 * (generatedNumber)));
		} else {
			CreateObstacle(randomnum, (int)((generatedNumber) * (-8.0f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (randomnum + UnityEngine.Random.Range ((-spaceBetweenObstacles/2f) + 0.5f, (spaceBetweenObstacles/2f) - 0.5f), UnityEngine.Random.Range (-2.0f, 2.0f) + (float)(-8 * (generatedNumber)));
		}
	}
	void CreateObstacle(float spaceloc, int locy, float spacelen, Color platformcolor) {
		spawnLocation = new Vector3 ((spaceloc - (0.5f * spacelen) - 6.1f), locy, 0);
		obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
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
		GameObject newCoin = (GameObject)Instantiate(Coin, spawnLocation, Quaternion.identity);
		newCoin.GetComponent<SpriteRenderer> ().color = coinColor;
	}
	//loads the proper skin depending on the current configuration.
	public void loadSkin() {
		Debug.Log (currentSpriteName);
		Vector3 playerscale = playerobject.transform.localScale;
		switch (currentSpriteName) {
			//cases have to be hardcoded because each case differs in some special way.
			case "Default":
				playerobject.GetComponent<SpriteRenderer>().sprite = Default;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
			break;
			case "CirclePlayer":
				playerobject.GetComponent<SpriteRenderer>().sprite = CirclePlayer;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.85f;
				playerscale.y = 0.85f;
				playerobject.transform.localScale = playerscale;
			break;
			case "2SquarePlayer":
				playerobject.GetComponent<SpriteRenderer>().sprite = TwoSquarePlayer;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<PolygonCollider2D>();
				playerscale.x = 0.85f;
				playerscale.y = 0.85f;
				playerobject.transform.localScale = playerscale;
			break;
			case "DogePlayer":
				playerobject.GetComponent<SpriteRenderer>().sprite = DogePlayer;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.90f;
				playerscale.y = 0.90f;
				playerobject.transform.localScale = playerscale;
			break;
		}
	}

	//Audio Methods
	public void PlaySingle(AudioClip clip){
		fxSource.clip = clip;
		fxSource.Play ();
	}
	public void RandomiseAudio(AudioClip clip){
		float randomPitch = UnityEngine.Random.Range (lowPitch, highPitch);

		fxSource.pitch = randomPitch;
		PlaySingle (clip);
	}
}

/*
 * 1-10 Easy 10-20 Medium 20-30 Hard 30-40 Very hard 40-50 Elite 50+ yolo mode
*/
