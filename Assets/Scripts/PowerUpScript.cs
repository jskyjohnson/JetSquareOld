using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PowerUpScript : MonoBehaviour {
	public GameObject player;
	public Button button;
	public Text text;
	public Text quantity;
	public float JumpPowerupDuration;
	public float CoinMagnetDuration;
	public float PassObstaclesDuration;
	// Use this for initialization
	void Start () {
		JumpPowerupDuration = 10.0f;
		CoinMagnetDuration = 15.0f;
		PassObstaclesDuration = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		quantity.GetComponent<Text> ().text = PlayerPrefs.GetInt (quantity.name).ToString ();
	}

	public void StartInfiniteJump() {
		if (PlayerPrefs.GetInt ("InfiniteJumpQuantity") > 0) {
			StartCoroutine (InfiniteJumpPowerup ());
			StartCoroutine (countDown (JumpPowerupDuration));
		}
	}
	public void StartCoinMagnet() {
		if (PlayerPrefs.GetInt ("CoinMagnetQuantity") > 0) {
			StartCoroutine (CoinMagnet ());
			StartCoroutine (countDown (CoinMagnetDuration));
		}
	}
	public void StartPassObstacles() {
		if (PlayerPrefs.GetInt ("PassObstaclesQuantity") > 0) {
			StartCoroutine (PassObstacles ());
			StartCoroutine (countDown (PassObstaclesDuration));
		}
	}

	IEnumerator countDown(float init) {
		button.interactable = false;
		while (init > 0.0f) {
			text.GetComponent<Text>().text = init.ToString ();
			yield return new WaitForSeconds(1.0f);
			init -= 1.0f;
		}
		text.GetComponent<Text>().text = "";
		button.interactable = true;
	}

	IEnumerator InfiniteJumpPowerup() {
		player.GetComponent<Player>().jumpLoaded = true;
		player.GetComponent<Player>().infiniteJumpAllowed = true;
		PlayerPrefs.SetInt ("InfiniteJumpQuantity", PlayerPrefs.GetInt ("InfiniteJumpQuantity") - 1);
		yield return new WaitForSeconds(JumpPowerupDuration);
		player.GetComponent<Player>().infiniteJumpAllowed = false;
	}
	
	IEnumerator CoinMagnet() {
		player.GetComponent<Player>().coinMagnet = true;
		PlayerPrefs.SetInt ("CoinMagnetQuantity", PlayerPrefs.GetInt ("CoinMagnetQuantity") - 1);
		yield return new WaitForSeconds(CoinMagnetDuration);
		player.GetComponent<Player>().coinMagnet = false;
	}

	IEnumerator PassObstacles() {
		player.GetComponent<Player> ().passObstacles = true;
		PlayerPrefs.SetInt ("PassObstaclesQuantity", PlayerPrefs.GetInt ("PassObstaclesQuantity") - 1);
		yield return new WaitForSeconds(PassObstaclesDuration);
		player.GetComponent<Player> ().passObstacles = false;
		foreach (GameObject deathblock in GameObject.FindGameObjectsWithTag("DeathBlock")) {
			deathblock.GetComponent<Collider2D> ().isTrigger = false;
			Color deathblockcolor = deathblock.GetComponent<SpriteRenderer>().color;
			deathblockcolor.a = 1f;
			deathblock.GetComponent<SpriteRenderer>().color = deathblockcolor;
		}
	}
}
