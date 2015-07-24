using UnityEngine;
using System.Collections;

public class FeedBackManager : MonoBehaviour {

	public GameObject hitCoinFeedback;
	public GameObject hitPlatformFeedback;
	public static FeedBackManager instance;
	// Use this for initialization
	void Start () {
		if (instance = null) {
			instance = this;
		} else if(instance != this){
			Destroy(gameObject);
		}

	}
	public void hitPlatform(Player player){
		Instantiate (hitPlatformFeedback, player.transform.position, player.transform.rotation);
	
	}
	public void hitCoin(Player player, Collider2D thiscoin){
		for (int i = 0; i < Random.Range(3,7); i++) {
			Vector3 newspawn;
			newspawn = new Vector3 (thiscoin.transform.position.x, thiscoin.transform.position.y, 0);

			Instantiate (hitCoinFeedback, newspawn, Quaternion.identity);

		}

	}
}
