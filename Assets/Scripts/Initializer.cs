using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;

public class Initializer : MonoBehaviour {
	void Start () {
		if (this.gameObject.name == "Initializer") {
			SoomlaStore.Initialize (new AppAssets ());
			StoreEvents.OnItemPurchased += ShopScript.onItemPurchased;
		} else if (this.gameObject.name != "Initializer") {
			DontDestroyOnLoad (this.gameObject);
		}
	}
	void Update() {
		StartCoroutine(loadScene1());
	}
	IEnumerator loadScene1() {
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("Scene1");
	}
}
