using UnityEngine;
using System.Collections;
using System;

public class CoinController : MonoBehaviour {
	public GameObject player;
	public Vector3 position;
	public Vector3 playerPosition;
	public float speed;
	public bool willFollow;
	public GameObject feedback;
	public int coinValue;
	// Use this for initialization
	void Start () {
		willFollow = false;
		speed = 2.0f;
		if (coinValue == 0)
			coinValue = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (player.GetComponent<Player> ().coinMagnet);
		if(player.GetComponent<Player> ().coinMagnet == true && this.gameObject.name == "Coin(Clone)") {
			position = this.gameObject.transform.position;
			playerPosition = player.transform.position;
			if(Math.Abs(playerPosition.x - position.x) < 5.0f && Math.Abs(playerPosition.y - position.y) < 5.0f || willFollow == true) {
				Debug.Log ("this is being called");
				willFollow = true;
				float step = speed * Time.deltaTime;
				step += 0.1f;
				this.gameObject.transform.position = Vector3.MoveTowards (position, playerPosition, step);
			}
		}
	}
}
