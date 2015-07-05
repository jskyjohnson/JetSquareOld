using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
public class PurchaseScript : MonoBehaviour {
	// Use this for initialization
	private string[] currentskins;

	public void purchase() {
		//get the object's name
		currentskins = ShopScript.currentskins;
		bool contains = false;
		//checks if already purchased
		for (int i = 0; i < currentskins.Length; i++) {
			if(currentskins[i] == this.gameObject.name) {
				contains = true;
			}
		}
		if (contains == false) {
			//if does not exist, then add to existing.
			List<string> currentskinsList = new List<string> ();
			currentskinsList.AddRange (currentskins);
			currentskinsList.Add (this.gameObject.name);
			currentskins = currentskinsList.ToArray ();
			ShopScript.currentskins = currentskinsList.ToArray ();
			PlayerPrefsX.SetStringArray("purchased", ShopScript.currentskins);
			PlayerPrefs.Save ();
		} else if (contains == true) {
			//if already contains, select skin and use it.
			PlayerPrefs.SetString ("currentskin", this.gameObject.name);
			Debug.Log ("setting currentskin to: " + this.gameObject.name);
			PlayerPrefs.Save();
		}
	}
}
