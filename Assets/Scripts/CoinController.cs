using UnityEngine;
using System.Collections;
using System;

public class CoinController : MonoBehaviour {
	public GameObject player;
	public Vector3 position;
	public Vector3 playerPosition;
	public float speed;
	public bool willFollow;
	// Use this for initialization
	void Start () {
		willFollow = false;
		speed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<Player> ().coinMagnet == true && this.gameObject.name == "Coin(Clone)") {
			position = this.gameObject.transform.position;
			playerPosition = player.transform.position;
			if(Math.Abs(playerPosition.x - position.x) < 2.0f && Math.Abs(playerPosition.y - position.y) < 2.0f || willFollow == true) {
				Debug.Log ("this is being called");
				willFollow = true;
				float step = speed * Time.deltaTime;
				step += 0.1f;
				this.gameObject.transform.position = Vector3.MoveTowards (position, playerPosition, step);
			}
		}
	}
}
