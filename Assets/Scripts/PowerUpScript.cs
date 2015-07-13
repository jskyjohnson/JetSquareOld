using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PowerUpScript : MonoBehaviour {
	public GameObject player;
	public Button button;
	public Text text;
	public Text quantity;
	public float JumpPowerupDuration;
	// Use this for initialization
	void Start () {
		JumpPowerupDuration = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		quantity.GetComponent<Text> ().text = PlayerPrefs.GetInt ("InfiniteJumpQuantity").ToString();
	}

	public void StartInfiniteJump() {
		if (PlayerPrefs.GetInt ("InfiniteJumpQuantity") > 0) {
			StartCoroutine (InfiniteJumpPowerup ());
			StartCoroutine (countDown (JumpPowerupDuration));
		}
	}
	public void StartCoinMagnet() {
		StartCoroutine (CoinMagnet());
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
		Image buttonImage = button.image;
		player.GetComponent<Player>().jumpLoaded = true;
		player.GetComponent<Player>().infiniteJumpAllowed = true;
		yield return new WaitForSeconds(JumpPowerupDuration);
		player.GetComponent<Player>().infiniteJumpAllowed = false;
	}
	
	IEnumerator CoinMagnet() {
		player.GetComponent<Player>().coinMagnet = true;
		yield return new WaitForSeconds(10.0f);
		player.GetComponent<Player>().coinMagnet = false;
	}
}
