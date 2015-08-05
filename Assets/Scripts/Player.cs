using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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

	public GameObject thisDeath;//
	public GameObject DefaultDeath;//
	public GameObject CirclePlayerDeath;//
	public GameObject DogePlayerDeath;
	public GameObject MinecraftDeath;
	public GameObject MeatboyDeath;
	public GameObject MoonDeath;//
	public GameObject PinkDeath;//
	public GameObject PurpleDeath;//
	public GameObject TealDeath;//
	public GameObject GreenDeath;//
	public GameObject MellonDeath;
	public GameObject eightBallDeath;
	public GameObject TennisBallDeath;
	public GameObject ToungeFaceDeath;
	public GameObject CatDeath;
	public GameObject CarotFaceDeath;
	public GameObject xfaceDeath;
	public GameObject BatDeath;
	public GameObject BatManDeath;
	public GameObject IronManDeath;
	public GameObject CaptainADeath;
	public GameObject SpiderManDeath;
	public GameObject NyancoDeath;
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
	public float jumpPowerInTime;
	public float createShadowTime;
	public int jumpsCount;
	public float leftBoundary;
	public float rightBoundary;
	public float timePassed;
	//powerup controller variables
	public bool coinMagnet;
	public bool hasDied;
	public static bool guardianAngel = false;
	//sprites
	public static string currentSpriteName;
	public Sprite Default;//
	public Sprite CirclePlayer;//
	public Sprite Moon;//
	public Sprite Pink;//
	public Sprite Purple;//
	public Sprite Teal;//
	public Sprite Green;//
	public Sprite Mellon;
	public Sprite eightBall;
	public Sprite TennisBall;
	public Sprite ToungeFace;
	public Sprite Cat;
	public Sprite CarotFace;
	public Sprite xface;
	public Sprite Bat;
	public Sprite BatMan;
	public Sprite IronMan;
	public Sprite CaptainA;
	public Sprite SpiderMan;
	public Sprite Nyanco;
	public Sprite CircleShadow;
	public Sprite meowShadow;
	public Sprite BatShadow;
	public Sprite kawaishadow;
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
	public float shadowTimeIntervalCreation;
	void Awake() {
		Application.targetFrameRate = 300;
	}

	void Start () {
		timePassed = 0f;
		musicSource.clip = music;
		musicSource.Play ();
		coinMagnet = false;
		canReplicate = false;
		right = false;
		spaceBetweenObstacles = 3.5f;
		coins = PlayerPrefs.GetInt ("coins", 0);
		currentSpriteName = PlayerPrefs.GetString ("currentskin");
		loadSkin ();
		scale = 1.2f;
		jumpPowerInTime = 0f;
		createShadowTime = 0f;
		playerobject.GetComponent<Rigidbody2D> ().isKinematic = true;
		jumpsCount = 2;
		leftBoundary = -(maincamera.orthographicSize * maincamera.aspect) + 2.8593f;
		rightBoundary = maincamera.orthographicSize * maincamera.aspect + 2.8593f;
		coinValue = 1;
	}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		//if you go outside the sides
		if (playerobject.transform.position.x > rightBoundary || playerobject.transform.position.x < leftBoundary) {
			die ();
		}
		//Handles movement and shadow
		createShadowTime += Time.deltaTime;
		if (shadowTimeIntervalCreation == 0 || shadowTimeIntervalCreation == null) {
			if (createShadowTime > 0.05f) {
				CreatePlayerShadow ();
			}
		} else {
			if (createShadowTime > shadowTimeIntervalCreation) {
				CreatePlayerShadow ();
			}
		}
		foreach (Touch touch in Input.touches)
		{
			if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && jumpsCount < 2)
			{
				jumpPowerInTime += Time.deltaTime;
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(playershadowcolor.r < fireshadowcolor.r) {
					playershadowcolor.r += 4f * Time.deltaTime;
				} else if(playershadowcolor.r > fireshadowcolor.r) {
					playershadowcolor.r -= 4f * Time.deltaTime;
				}
				if(playershadowcolor.g < fireshadowcolor.g) {
					playershadowcolor.g += 4f * Time.deltaTime;
				} else if(playershadowcolor.g > fireshadowcolor.g) {
					playershadowcolor.g -= 4f * Time.deltaTime;
				}
				if(playershadowcolor.b < fireshadowcolor.b) {
					playershadowcolor.b += 4f * Time.deltaTime;
				} else if(playershadowcolor.b > fireshadowcolor.b) {
					playershadowcolor.b -= 4f * Time.deltaTime;
				}
				if(playershadowcolor.a < fireshadowcolor.a) {
					playershadowcolor.a += 8f * Time.deltaTime;
				} else if(playershadowcolor.a > fireshadowcolor.a) {
					playershadowcolor.a -= 8f * Time.deltaTime;
				}
				Vector3 playershadowscale = playershadow.transform.localScale;
				if(playershadowscale.x > fireshadowscale.x) {
					playershadowscale.x -= 1.4f * Time.deltaTime;
				} else if(playershadowscale.x < fireshadowscale.x) {
					playershadowscale.x += 1.4f * Time.deltaTime;
				}
				if(playershadowscale.y > fireshadowscale.y) {
					playershadowscale.y -= 1.4f * Time.deltaTime;
				} else if(playershadowscale.y < fireshadowscale.y) {
					playershadowscale.y += 1.4f * Time.deltaTime;
				}
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			} else {
				Color playershadowcolor = playershadow.GetComponent<SpriteRenderer> ().color;
				if(playershadowcolor.r < shadowcolor.r) {
					playershadowcolor.r += 2.8f * Time.deltaTime;
				} else if(playershadowcolor.r > shadowcolor.r) {
					playershadowcolor.r -= 2.8f * Time.deltaTime;
				}
				if(playershadowcolor.g < shadowcolor.g) {
					playershadowcolor.g += 2.8f * Time.deltaTime;
				} else if(playershadowcolor.g > shadowcolor.g) {
					playershadowcolor.g -= 2.8f * Time.deltaTime;
				}
				if(playershadowcolor.b < shadowcolor.b) {
					playershadowcolor.b += 2.8f * Time.deltaTime;
				} else if(playershadowcolor.b > shadowcolor.b) {
					playershadowcolor.b -= 2.8f * Time.deltaTime;
				}
				if(playershadowcolor.a < shadowcolor.a) {
					playershadowcolor.a += 3f * Time.deltaTime;
				} else if(playershadowcolor.a > shadowcolor.a) {
					playershadowcolor.a -= 3f * Time.deltaTime;
				}
				Vector3 playershadowscale = playershadow.transform.localScale;
				if(playershadowscale.x > shadowscale.x) {
					playershadowscale.x -= 1f * Time.deltaTime;
				} else if(playershadowscale.x < shadowscale.x) {
					playershadowscale.x += 1f * Time.deltaTime;
				}
				if(playershadowscale.y > shadowscale.y) {
					playershadowscale.y -= 1f * Time.deltaTime;
				} else if(playershadowscale.y < shadowscale.y) {
					playershadowscale.y += 1f * Time.deltaTime;
				}
				playershadow.transform.localScale = playershadowscale;
				playershadow.GetComponent<SpriteRenderer> ().color = playershadowcolor;
			}
			if (touch.phase == TouchPhase.Ended) {
				jumpsCount += 1;
				playerobject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((sinAngle * 25f * jumpPowerInTime), (cosAngle * 150f) * jumpPowerInTime), ForceMode2D.Impulse);
				jumpPowerInTime = 0f;
				if(right && jumpsCount == 1) {
					playerobject.GetComponent<Rigidbody2D>().AddTorque (-15f);
				} else if (jumpsCount == 1) {
					playerobject.GetComponent<Rigidbody2D>().AddTorque (15f);
				}
			}
		}

		if (Input.GetKey ("up") && jumpsCount < 2) {
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
				jumpsCount += 1;
				playerobject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((sinAngle * 25f * jumpPowerInTime), (cosAngle * 150f) * jumpPowerInTime), ForceMode2D.Impulse);
				jumpPowerInTime = 0f;
				if(right && jumpsCount == 1) {
					playerobject.GetComponent<Rigidbody2D>().AddTorque (-15f);
				} else if (jumpsCount == 1) {
					playerobject.GetComponent<Rigidbody2D>().AddTorque (15f);
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
			feedbackmanager.hitCoin(this, coll, coll.gameObject.GetComponent<SpriteRenderer>().color);
			backgroundCube.randTorHitCoin();
			coinSource.clip=HitCoin;
			coinSource.Play ();

			coins += coll.gameObject.GetComponent<CoinController> ().coinValue;
			initcoins += coll.gameObject.GetComponent<CoinController> ().coinValue;
			Destroy(coll.gameObject);
			coinGUI.GetComponent<Text>().text = initcoins.ToString();
		}
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		//handles change in rotation.
		//backgroundCube.GetComponent<Rigidbody> ().AddTorque (new Vector3 (UnityEngine.Random.Range (-1, 1), UnityEngine.Random.Range (-1, 1), UnityEngine.Random.Range (-1, 1))*UnityEngine.Random.Range(10,50));
		if (score < 5) {
			coinValue = 1;
			spaceBetweenObstacles = 4.5f;
			scale = 1.2f;
			coinColor = new Color(1f, 1f, 1f);
			levelBasedColor = new Color(0.46666f, 0.88235f, 0.866666f); //baby blue/green
		} else if (score < 10 && score >= 5) {
			coinValue = 1;
			scale = 1.1f;
			spaceBetweenObstacles = 4.1f;
			//previousColor = levelBasedColor;
			levelBasedColor = new Color(0.50196f, 0.8313f, 0.61176f); //green
		} else if (score < 20 && score >= 10) {
			coinValue = 2;
			scale = 1.0f;
			spaceBetweenObstacles = 3.7f;
			coinColor = new Color(0.847f, 0.255f, 0.255f);
			levelBasedColor = new Color(1.0f, 0.6666f, 0.4117f);//orange
		} else if (score < 30 && score >= 20) {
			coinValue = 2;
			scale = 0.9f;
			spaceBetweenObstacles = 3.4f;
			levelBasedColor = new Color(0.5137f, 0.50196f, 0.83137f); //purple
		} else if (score < 50 && score >= 30) {
			coinValue = 3;
			scale = 0.8f;
			coinColor = new Color(0.0705f, 0.2902f, 0.941f);
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
			jumpsCount = 0;
			backgroundCube.randTorHit();
			feedbackmanager.hitPlatform(this);
			if((!coll.gameObject.GetComponent<PlatformScript>().hasCollided) || hasDied) {
				RandomiseAudio(HitPlatform);
				playerobject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				playerobject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				hasDied = false;
			}
			if (!coll.gameObject.GetComponent<PlatformScript>().hasCollided) {
				coll.gameObject.GetComponent<PlatformScript>().contactpoint = playerobject.transform.position;
				if (right == true) {
					float randomnum = UnityEngine.Random.Range (12.8F, rightBoundary + (4.8f - scale * 1.2f));
					CreatePlatform (randomnum, -8.6f + (-9.5f * (float)(score + 2f)), UnityEngine.Random.Range (42.0F, 62.0F), randomnum - 6.6f, levelBasedColor, scale);
					right = false;
				} else if (right == false) {
					float randomnum = UnityEngine.Random.Range (leftBoundary - (4.8f - scale * 1.2f), -5.3F);
					CreatePlatform (randomnum, -8.6f + (-9.5f * (float)(score + 2f)), UnityEngine.Random.Range (298F, 318F), randomnum + 6.6f, levelBasedColor, scale);
					right = true;
				}
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
			die ();
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
			CreateObstacle (randomnum, (int)((score + 2f) * (-9.5f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (randomnum + UnityEngine.Random.Range ((-spaceBetweenObstacles/2f) + 0.5f, (spaceBetweenObstacles/2f) - 0.5f), UnityEngine.Random.Range (-2.0f, 2.0f) + (float)(-9.5 * (score + 2f)));
		} else {
			CreateObstacle(randomnum, (int)((score + 2f) * (-9.5f)), spaceBetweenObstacles, platformcolor);
			CreateCoin (randomnum + UnityEngine.Random.Range ((-spaceBetweenObstacles/2f) + 0.5f, (spaceBetweenObstacles/2f) - 0.5f), UnityEngine.Random.Range (-2.0f, 2.0f) + (float)(-9.5 * (score + 2f)));
		}
	}
	void CreateObstacle(float spaceloc, int locy, float spacelen, Color platformcolor) {
		spawnLocation = new Vector3 ((spaceloc - (0.5f * spacelen) - 9.0f), locy, 0);
		obstacle = (GameObject)Instantiate(Deathblock, spawnLocation, Quaternion.identity);
		//obstacle.GetComponent<SpriteRenderer> ().color = platformcolor;
		spawnLocation = new Vector3 ((spaceloc + (0.5f * spacelen) + 9.0f), locy, 0);
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
		newCoin.GetComponent<CoinController> ().coinValue = coinValue;
	}
	//loads the proper skin depending on the current configuration.
	public void loadSkin() {
		//Debug.Log (currentSpriteName);
		Vector3 playerscale = playerobject.transform.localScale;
		switch (currentSpriteName) {
			//cases have to be hardcoded because each case differs in some special way.
			case "Default":
			case "":
				PlayerShadow.isTrail = false;
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
				thisDeath = DefaultDeath;
			break;
			case "Purple":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Purple;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = PurpleDeath;
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
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Pink;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = PinkDeath;
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
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Teal;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = TealDeath;
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
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Green;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = GreenDeath;
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
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Moon;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.76f;
				playerscale.y = 0.76f;
				playerobject.transform.localScale = playerscale;
				thisDeath = MoonDeath;

				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0.3f, 0.3f, 0.3f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
			break;
			case "CirclePlayer":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = CirclePlayer;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.76f;
				playerscale.y = 0.76f;
				playerobject.transform.localScale = playerscale;
				thisDeath = CirclePlayerDeath;
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(1f, 0.54f, 0.54f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
			break;
			case "8Ball":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = eightBall;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.76f;
				playerscale.y = 0.76f;
				playerobject.transform.localScale = playerscale;
				thisDeath = eightBallDeath;

				shadowcolor = new Color(0.5f, 0.5f, 0.5f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0f, 0f, 0f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "TennisBall":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = TennisBall;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<CircleCollider2D>();
				playerscale.x = 0.76f;
				playerscale.y = 0.76f;
				playerobject.transform.localScale = playerscale;
				thisDeath = TennisBallDeath;

				shadowcolor = new Color(0.961f, 1f, 0.867f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0.682f, 1f, 0.1176f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Mellon":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Mellon;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = MellonDeath;

				//shadow customization
				shadowcolor = new Color(0.9254f, 0.635f, 0.961f);
				shadowcolor.a = 0.3f;
				shadowscale = new Vector3(0.5f, 0.5f, 1f);
				fireshadowcolor = new Color(1f, 0.427f, 0.984f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().sprite = CircleShadow;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Cat":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Cat;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = CatDeath;

				//shadow customization
				shadowcolor = new Color(0.4f, 0.4f, 0.4f);
				shadowcolor.a = 0f;
				shadowscale = new Vector3(2f, 2f, 2f);
				fireshadowcolor = new Color(0f, 0f, 0f);
				fireshadowscale = new Vector3(6f, 6f, 6f);
				shadowTimeIntervalCreation = 0.2f;
				playershadow.GetComponent<SpriteRenderer>().sprite = meowShadow;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "toungeface":
				playerobject.GetComponent<SpriteRenderer>().sprite = ToungeFace;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = ToungeFaceDeath;

				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.6f, 0.6f, 1f);
				fireshadowcolor = new Color(0.9f, 0.9f, 0.9f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				fireshadowcolor.a = 0.6f;
				shadowTimeIntervalCreation = 0.03f;
				PlayerShadow.isTrail = true;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.GetComponent<SpriteRenderer>().sprite = ToungeFace;
				playershadow.transform.localScale = shadowscale;
				break;
			case "carotface":
				PlayerShadow.isTrail = true;
				playerobject.GetComponent<SpriteRenderer>().sprite = CarotFace;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;

				thisDeath = CarotFaceDeath;
				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.6f, 0.6f, 1f);
				fireshadowcolor = new Color(0.9f, 0.9f, 0.9f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				fireshadowcolor.a = 0.6f;
				shadowTimeIntervalCreation = 0.03f;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.GetComponent<SpriteRenderer>().sprite = CarotFace;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Bat":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = Bat;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = BatDeath;

				//shadow customization
				shadowcolor = new Color(0f, 0f, 0f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0.137f, 0.0666f, 0.388f);
				fireshadowscale = new Vector3(1.4f, 1.4f, 1f);
				fireshadowcolor.a = 0.6f;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.GetComponent<SpriteRenderer>().sprite = CircleShadow;
				playershadow.transform.localScale = shadowscale;
				break;
			case "xface":
				playerobject.GetComponent<SpriteRenderer>().sprite = xface;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = xfaceDeath;
				//shadow customization
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.6f, 0.6f, 1f);
				fireshadowcolor = new Color(0.9f, 0.9f, 0.9f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				fireshadowcolor.a = 0.6f;
				shadowTimeIntervalCreation = 0.03f;
				PlayerShadow.isTrail = true;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.GetComponent<SpriteRenderer>().sprite = xface;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Batman":
				PlayerShadow.isTrail = false;
				playerobject.GetComponent<SpriteRenderer>().sprite = BatMan;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = BatManDeath;
				//shadow customization
				shadowcolor = new Color(0f, 0f, 0f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(1.0f, 1.0f, 1f);
				fireshadowcolor = new Color(0f, 0.0f, 0f);
				fireshadowscale = new Vector3(1.8f, 1.8f, 1f);
				fireshadowcolor.a = 0.6f;
				shadowTimeIntervalCreation = 0.1f;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.GetComponent<SpriteRenderer>().sprite = BatShadow;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Ironman":
				playerobject.GetComponent<SpriteRenderer>().sprite = IronMan;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				PlayerShadow.isTrail = true;
				thisDeath = IronManDeath;
				//shadow customization
				shadowcolor = new Color(1f, 0.3137f, 0.3137f);
				shadowcolor.a = 0.9f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(0f, 1f, 1f);
				fireshadowscale = new Vector3(0.5f, 0.5f, 1f);
				shadowTimeIntervalCreation = 0.01f;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "CaptainA":
				playerobject.GetComponent<SpriteRenderer>().sprite = CaptainA;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = CaptainADeath;
				//shadow customization
				PlayerShadow.isTrail = false;
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0.2f;
				shadowscale = new Vector3(0.3f, 0.3f, 1f);
				fireshadowcolor = new Color(1f, 0.54f, 0.54f);
				fireshadowscale = new Vector3(1f, 1f, 1f);
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Spiderman":
				playerobject.GetComponent<SpriteRenderer>().sprite = SpiderMan;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = SpiderManDeath;
				PlayerShadow.isTrail = true;
				//shadow customization
				shadowcolor = new Color(1f,1f, 1f);
				shadowcolor.a = 0.5f;
				shadowscale = new Vector3(0.1f, 0.1f, 1f);
				fireshadowcolor = new Color(1f, 1f, 1f);
				fireshadowscale = new Vector3(0.3f, 0.3f, 1f);
				shadowTimeIntervalCreation = 0.001f;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.transform.localScale = shadowscale;
				break;
			case "Nyanco":
				playerobject.GetComponent<SpriteRenderer>().sprite = Nyanco;
				Destroy (GetComponent<Collider2D>());
				playerobject.AddComponent<BoxCollider2D>();
				playerscale.x = 0.6f;
				playerscale.y = 0.6f;
				playerobject.transform.localScale = playerscale;
				thisDeath = NyancoDeath;
				//shadow customization
				PlayerShadow.isTrail = false;
				shadowcolor = new Color(1f, 1f, 1f);
				shadowcolor.a = 0f;
				shadowscale = new Vector3(3f, 3f, 1f);
				fireshadowcolor = new Color(1f, 1f, 1f);
				fireshadowscale = new Vector3(3.6f, 3.6f, 1f);
				shadowTimeIntervalCreation = 0.15f;
				playershadow.GetComponent<SpriteRenderer>().color = shadowcolor;
				playershadow.GetComponent<SpriteRenderer>().sprite = kawaishadow;
				playershadow.transform.localScale = shadowscale;
				break;
		}
	}

	public void die() {
		Debug.Log ("timePassed " + PlayerPrefs.GetInt ("GameTime"));
		PlayerPrefs.SetInt ("GameTime", (int) timePassed + PlayerPrefs.GetInt ("GameTime"));
		playerDeather ();

		hasDied = true;
		if (!guardianAngel) {
			PlayerPrefs.SetInt ("LastScore", score);
			StoreValues (score, coins);
			gameObject.active = false;
		} else {
			revive ();
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
	public void revive() {
		GameObject closest = null;
		float closestvalue = Mathf.Infinity;
		foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform")) {
			if(platform.GetComponent<PlatformScript>().hasCollided == true) {
				float testValue = (platform.transform.position.y - playerobject.transform.position.y);
				if(testValue < closestvalue) {
					closest = platform;
					closestvalue = testValue;
				}
			}
		}
		Vector3 revivalposition = closest.GetComponent<PlatformScript>().contactpoint;
		revivalposition.y += 2.0f;
		playerobject.transform.position = revivalposition;
		playerobject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		playerobject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
		playerobject.GetComponent<Rigidbody2D> ().isKinematic = true;
		StartCoroutine (GuardianAngelDelay());
	}

	IEnumerator GuardianAngelDelay() {
		yield return new WaitForSeconds(3.0f);
		playerobject.GetComponent<Rigidbody2D> ().isKinematic = false;
		jumpsCount = 0;
		guardianAngel = false;
	}
	void playerDeather(){
		Instantiate (thisDeath, this.transform.position, this.transform.rotation);
	}
}

/*
 * 1-10 Easy 10-20 Medium 20-30 Hard 30-40 Very hard 40-50 Elite 50+ yolo mode
*/
