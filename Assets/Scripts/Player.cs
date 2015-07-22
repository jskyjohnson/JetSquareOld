using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public int score = 0;
	public int coins;
	public int initcoins = 0;
	
	float cosAngle = 1.0f;
	float sinAngle = 0.0f;
	//Game Objects
	public GameObject Platform;
	public GameObject Deathblock;
	public GameObject playerobject;
	public GameObject playershadow;
	public Text scoreGUI;
	public Text coinGUI;
	public GameObject Coin;
	private GameObject obstacle;
	public Camera maincamera;
	public BackgroundCube backgroundCube;

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
	public float createShadowTime;
	//powerup controller variables
	public bool coinMagnet;
	public bool passObstacles;
	public float areaSection;
	//sprites
	public static string currentSpriteName;
	public Sprite Default;
	public Sprite CirclePlayer;
	public Sprite DogePlayer;
	public Sprite Minecraft;
	public Sprite Meatboy;
	public Sprite Moon;
	public Sprite Netflix;
	public Sprite Superman;
	public Sprite Pink;
	public Sprite Purple;
	public Sprite Teal;
	public Sprite Green;
	//customization variables
	public Color shadowcolor;
	public Color fireshadowcolor;
	public Vector3 shadowscale;
	public Vector3 fireshadowscale;

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
	
	Quaternion platformAngle;
	void Start () {
		musicSource.clip = music;
		musicSource.Play ();
		coinMagnet = false;
		canReplicate = false;
		right = false;
		spaceBetweenObstacles = 3.5f;
		scale = 2.5f;
		coins = PlayerPrefs.GetInt ("coins", 0);
		currentSpriteName = PlayerPrefs.GetString ("currentskin");
		loadSkin ();
		lastPoint = playerobject.transform.position.y;
		generated = false;
		generatedNumber = 2;
		passObstacles = false;
		areaSection = 0f;
		jumpPowerInTime = 0f;
		createShadowTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		//Handles movement
		if ((playerobject.transform.position.y < lastPoint - 3.0f) && generated == false) {
			generated = true;
			areaSection += 1f;
			if (right == true) {
				float randomnum = UnityEngine.Random.Range (4.4F, 7.8F);
				CreatePlatform (randomnum + 0.8f + 3.0f, -5.5f + (-8f * (float)(generatedNumber)), UnityEngine.Random.Range (42.0F, 62.0F), randomnum, levelBasedColor, scale);
				right = false;
			} else if (right == false) {
				float randomnum = UnityEngine.Random.Range (-2.0F, 1.5F);
				CreatePlatform (randomnum - 0.8f - 3.0f, -5.5f + (-8f * (float)(generatedNumber)), UnityEngine.Random.Range (298F, 318F), randomnum, levelBasedColor, scale);
				right = true;
			}
		}
		if ((playerobject.transform.position.y < lastPoint - 6.0f)) {
			generated = false;
			lastPoint = playerobject.transform.position.y;
			generatedNumber += 1;
		}
		createShadowTime += Time.deltaTime;
		if (createShadowTime > 0.05f) {
			CreatePlayerShadow ();
		}
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
			{
				jumpPowerInTime += Time.deltaTime;
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(playershadowcolor.r < fireshadowcolor.r) {
					playershadowcolor.r += 5f * Time.deltaTime;
				} else if(playershadowcolor.r > fireshadowcolor.r) {
					playershadowcolor.r -= 5f * Time.deltaTime;
				}
				if(playershadowcolor.g < fireshadowcolor.g) {
					playershadowcolor.g += 5f * Time.deltaTime;
				} else if(playershadowcolor.g > fireshadowcolor.g) {
					playershadowcolor.g -= 5f * Time.deltaTime;
				}
				if(playershadowcolor.b < fireshadowcolor.b) {
					playershadowcolor.b += 5f * Time.deltaTime;
				} else if(playershadowcolor.b > fireshadowcolor.b) {
					playershadowcolor.b -=  5f * Time.deltaTime;
				}
				if(playershadowcolor.a < fireshadowcolor.a) {
					playershadowcolor.a += 4f * Time.deltaTime;
				} else if(playershadowcolor.a > fireshadowcolor.a) {
					playershadowcolor.a -= 4f * Time.deltaTime;
				}
				Vector3 playershadowscale = playershadow.transform.localScale;
				if(playershadowscale.x > fireshadowscale.x) {
					playershadowscale.x -= 1.6f * Time.deltaTime;
				} else if(playershadowscale.x < fireshadowscale.x) {
					playershadowscale.x += 1.6f * Time.deltaTime;
				}
				if(playershadowscale.y > fireshadowscale.y) {
					playershadowscale.y -= 1.6f * Time.deltaTime;
				} else if(playershadowscale.y < fireshadowscale.y) {
					playershadowscale.y += 1.6f * Time.deltaTime;
				}
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			} else {
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(playershadowcolor.r < shadowcolor.r) {
					playershadowcolor.r += 4f * Time.deltaTime;
				} else if(playershadowcolor.r > shadowcolor.r) {
					playershadowcolor.r -= 4f * Time.deltaTime;
				}
				if(playershadowcolor.g < shadowcolor.g) {
					playershadowcolor.g += 4f * Time.deltaTime;
				} else if(playershadowcolor.g > shadowcolor.g) {
					playershadowcolor.g -= 4f * Time.deltaTime;
				}
				if(playershadowcolor.b < shadowcolor.b) {
					playershadowcolor.b += 4f * Time.deltaTime;
				} else if(playershadowcolor.b > shadowcolor.b) {
					playershadowcolor.b -= 4f * Time.deltaTime;
				}
				if(playershadowcolor.a < shadowcolor.a) {
					playershadowcolor.a += 2f * Time.deltaTime;
				} else if(playershadowcolor.a > shadowcolor.a) {
					playershadowcolor.a -= 2f * Time.deltaTime;
				}
				Vector3 playershadowscale = playershadow.transform.localScale;
				if(playershadowscale.x > shadowscale.x) {
					playershadowscale.x -= 0.6f * Time.deltaTime;
				} else if(playershadowscale.x < shadowscale.x) {
					playershadowscale.x += 0.6f * Time.deltaTime;
				}
				if(playershadowscale.y > shadowscale.y) {
					playershadowscale.y -= 0.6f * Time.deltaTime;
				} else if(playershadowscale.y < shadowscale.y) {
					playershadowscale.y += 0.6f * Time.deltaTime;
				}
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			}
			if (touch.phase == TouchPhase.Ended) {
				playerobject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((sinAngle * 25f * jumpPowerInTime), (cosAngle * 69f) * jumpPowerInTime), ForceMode2D.Impulse);
				jumpPowerInTime = 0f;
			}
		}

			if (Input.GetKey ("up")) {
				jumpPowerInTime += Time.deltaTime;
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(playershadowcolor.r < fireshadowcolor.r) {
					playershadowcolor.r += 2f * Time.deltaTime;
				} else if(playershadowcolor.r > fireshadowcolor.r) {
					playershadowcolor.r -= 2f * Time.deltaTime;
				}
				if(playershadowcolor.g < fireshadowcolor.g) {
					playershadowcolor.g += 2f * Time.deltaTime;
				} else if(playershadowcolor.g > fireshadowcolor.g) {
					playershadowcolor.g -= 2f * Time.deltaTime;
				}
				if(playershadowcolor.b < fireshadowcolor.b) {
					playershadowcolor.b += 2f * Time.deltaTime;
				} else if(playershadowcolor.b > fireshadowcolor.b) {
					playershadowcolor.b -=  2f * Time.deltaTime;
				}
				if(playershadowcolor.a < fireshadowcolor.a) {
					playershadowcolor.a += 4f * Time.deltaTime;
				} else if(playershadowcolor.a > fireshadowcolor.a) {
					playershadowcolor.a -= 4f * Time.deltaTime;
				}
				Vector3 playershadowscale = playershadow.transform.localScale;
				if(playershadowscale.x > fireshadowscale.x) {
					playershadowscale.x -= 0.7f * Time.deltaTime;
				} else if(playershadowscale.x < fireshadowscale.x) {
					playershadowscale.x += 0.7f * Time.deltaTime;
				}
				if(playershadowscale.y > fireshadowscale.y) {
					playershadowscale.y -= 0.7f * Time.deltaTime;
				} else if(playershadowscale.y < fireshadowscale.y) {
					playershadowscale.y += 0.7f * Time.deltaTime;
				}
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			} else {
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(playershadowcolor.r < shadowcolor.r) {
					playershadowcolor.r += 1.6f * Time.deltaTime;
				} else if(playershadowcolor.r > shadowcolor.r) {
					playershadowcolor.r -= 1.6f * Time.deltaTime;
				}
				if(playershadowcolor.g < shadowcolor.g) {
					playershadowcolor.g += 1.6f * Time.deltaTime;
				} else if(playershadowcolor.g > shadowcolor.g) {
					playershadowcolor.g -= 1.6f * Time.deltaTime;
				}
				if(playershadowcolor.b < shadowcolor.b) {
					playershadowcolor.b += 1.6f * Time.deltaTime;
				} else if(playershadowcolor.b > shadowcolor.b) {
					playershadowcolor.b -= 1.6f * Time.deltaTime;
				}
				if(playershadowcolor.a < shadowcolor.a) {
					playershadowcolor.a += 2f * Time.deltaTime;
				} else if(playershadowcolor.a > shadowcolor.a) {
					playershadowcolor.a -= 2f * Time.deltaTime;
				}
				Vector3 playershadowscale = playershadow.transform.localScale;
				if(playershadowscale.x > shadowscale.x) {
					playershadowscale.x -= 0.6f * Time.deltaTime;
				} else if(playershadowscale.x < shadowscale.x) {
					playershadowscale.x += 0.6f * Time.deltaTime;
				}
				if(playershadowscale.y > shadowscale.y) {
					playershadowscale.y -= 0.6f * Time.deltaTime;
				} else if(playershadowscale.y < shadowscale.y) {
					playershadowscale.y += 0.6f * Time.deltaTime;
				}
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			}
			if (Input.GetKeyUp ("up")) {
				playerobject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((sinAngle * 25f * jumpPowerInTime), (cosAngle * 69f) * jumpPowerInTime), ForceMode2D.Impulse);
				jumpPowerInTime = 0f;
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
			feedbackmanager.hitCoin(this, coll);
			backgroundCube.randTorHitCoin();
			coinSource.clip=HitCoin;
			coinSource.Play ();

			coins += coinValue;
			initcoins += coinValue;
			Destroy(coll.gameObject);
			coinGUI.GetComponent<Text>().text = initcoins.ToString();
		}
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		//backgroundCube.GetComponent<Rigidbody> ().AddTorque (new Vector3 (UnityEngine.Random.Range (-1, 1), UnityEngine.Random.Range (-1, 1), UnityEngine.Random.Range (-1, 1))*UnityEngine.Random.Range(10,50));
		if (score < 5) {
			coinValue = 1;
			spaceBetweenObstacles = 4f;
			scale = 1.2f;
			coinColor = new Color(1f, 1f, 1f);
			levelBasedColor = new Color(0.46666f, 0.88235f, 0.866666f); //baby blue/green
		} else if (score < 10 && score >= 5) {
			coinValue = 2;
			scale = 1.1f;
			spaceBetweenObstacles = 3.8f;
			coinColor = new Color(0.847f, 0.255f, 0.255f);
			//previousColor = levelBasedColor;
			levelBasedColor = new Color(0.50196f, 0.8313f, 0.61176f); //green
		} else if (score < 20 && score >= 10) {
			coinValue = 2;
			scale = 1.0f;
			spaceBetweenObstacles = 3.6f;
			levelBasedColor = new Color(0.8823f, 0.8705f, 0.6039f);//yellow
		} else if (score < 30 && score >= 20) {
			coinValue = 3;
			scale = 0.9f;
			coinColor = new Color(0.0705f, 0.2902f, 0.941f);
			spaceBetweenObstacles = 3.4f;
			levelBasedColor = new Color(0.5137f, 0.50196f, 0.83137f); //purple
		} else if (score < 50 && score >= 30) {
			coinValue = 3;
			scale = 0.8f;
			spaceBetweenObstacles = 3.2f;
			levelBasedColor = new Color(0.8823f, 0.4666f, 0.4666f); //red
		} else {
			coinValue = 4;
			scale = 0.7f;
			coinColor = new Color(0.941f, 0.0705f, 0.7725f);
			spaceBetweenObstacles = 3.0f;
			levelBasedColor = new Color(0.6f, 1f, 1f);
			levelBasedColor = new Color(0.88235f, 0.4666f, 0.83529f);//pink
		}
		backgroundCube.setColor (levelBasedColor);
		if (coll.gameObject.name == "Platform" || coll.gameObject.name == "Platform(Clone)") {
			backgroundCube.randTorHit();
			feedbackmanager.hitPlatform(this);
			if (!coll.gameObject.GetComponent<PlatformScript>().hasCollided) {
				RandomiseAudio(HitPlatform);
				playerobject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				playerobject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				coll.gameObject.GetComponent<PlatformScript>().hasCollided = true;
				Vector3 rot = coll.gameObject.transform.rotation.eulerAngles;
				cosAngle = (float)(Math.Cos (3.14f * (rot.z / 180f)));
				sinAngle = -(float)(Math.Sin (3.14f * (rot.z / 180f)));
				if(coll.gameObject.GetComponent<PlatformScript>().givenScore == false) {
					score += 1;
					coll.gameObject.GetComponent<PlatformScript>().givenScore = true;
				}
				scoreGUI.GetComponent<Text>().text = score.ToString();
			}
		}
		if (coll.gameObject.name == "DeathBlock" || coll.gameObject.name == "DeathBlockToClone" || coll.gameObject.name == "DeathBlockToClone(Clone)") {
			Instantiate(deathAnimation,this.transform.position,this.transform.rotation);
			StoreValues (score, coins);
			AdsManager.loadAd ();
			gameObject.active = false;
		}
	}
	void CreatePlatform(float locx, float locy, float angle, float randomnum, Color platformcolor, float scale) {
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
		createShadowTime = 0.0f;
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
		//Debug.Log (currentSpriteName);
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
				
				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(1f, 0.54f, 0.54f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
			break;
			case "Meatboy":
				playerobject.GetComponent<SpriteRenderer>().sprite = Meatboy;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				break;
			case "Netflix":
				playerobject.GetComponent<SpriteRenderer>().sprite = Netflix;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				break;
			case "Superman":
				playerobject.GetComponent<SpriteRenderer>().sprite = Superman;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				break;
			case "Purple":
				playerobject.GetComponent<SpriteRenderer>().sprite = Purple;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;

				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0.239f, 0.765f, 1f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Pink":
				playerobject.GetComponent<SpriteRenderer>().sprite = Pink;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;

				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(1f, 0.427f, 0.984f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Teal":
				playerobject.GetComponent<SpriteRenderer>().sprite = Teal;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;

				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0.058f, 0.706f, 0.737f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Green":
				playerobject.GetComponent<SpriteRenderer>().sprite = Green;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;

				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0.043f, 0.722f, 0.498f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Moon":
			playerobject.GetComponent<SpriteRenderer>().sprite = Moon;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.75f;
				playerscale.y = 0.75f;
				playerobject.transform.localScale = playerscale;
			break;
			case "Minecraft":
				playerobject.GetComponent<SpriteRenderer>().sprite = Minecraft;
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
