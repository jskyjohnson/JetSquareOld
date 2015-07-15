using UnityEngine;
using System.Collections;

public class FeedBackManager : MonoBehaviour {

	public GameObject hitCoinFeedback;
	public GameObject hitPlatformFeedback;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void hitPlatform(Player player){
		Instantiate (hitPlatformFeedback, player.transform.position, player.transform.rotation);
	}
	public void hitCoin(Player player){
		Instantiate (hitCoinFeedback, player.transform.position, player.transform.rotation);
	}
}
