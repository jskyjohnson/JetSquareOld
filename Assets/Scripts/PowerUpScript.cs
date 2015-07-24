using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PowerUpScript : MonoBehaviour {
	public GameObject player;
	public Button button;
	public Text text;
	public Text quantity;
	public float CoinMagnetDuration;
	public float GuardianDuration;
	// Use this for initialization
	void Start () {
		CoinMagnetDuration = 15.0f;
		GuardianDuration = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		quantity.GetComponent<Text> ().text = PlayerPrefs.GetInt (quantity.name).ToString ();
	}
	
	public void StartCoinMagnet() {
		if (PlayerPrefs.GetInt ("CoinMagnetQuantity") > 0) {
			StartCoroutine (CoinMagnet ());
			StartCoroutine (countDown (CoinMagnetDuration));
		}
	}
	public void StartGuardianAngel() {
		if (PlayerPrefs.GetInt ("GuardianQuantity") > 0) {
			StartCoroutine (GuardianAngel ());
			StartCoroutine (countDown (GuardianDuration));
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

	IEnumerator CoinMagnet() {
		player.GetComponent<Player>().coinMagnet = true;
		PlayerPrefs.SetInt ("CoinMagnetQuantity", PlayerPrefs.GetInt ("CoinMagnetQuantity") - 1);
		yield return new WaitForSeconds(CoinMagnetDuration);
		player.GetComponent<Player>().coinMagnet = false;
	}

	IEnumerator GuardianAngel() {
		Player.guardianAngel = true;
		PlayerPrefs.SetInt ("GuardianQuantity", PlayerPrefs.GetInt ("GuardianQuantity") - 1);
		yield return new WaitForSeconds(GuardianDuration);
	}
}
