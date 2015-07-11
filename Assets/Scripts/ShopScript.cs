using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public class ShopScript : MonoBehaviour {
	public Canvas ShopMenu;
	public static Text coins;
	public Button backButton;
	int coinsvalue;
	public static string[] currentskins = {};
	public Button[] skinsButtons;
	public Sprite unknownImage;
	// Use this for initialization
	void Start () {
		ShopMenu = ShopMenu.GetComponent<Canvas> ();
		currentskins = PlayerPrefsX.GetStringArray ("purchased");
		coins = ShopMenu.GetComponentInChildren<Text> ();
		loadcoinvalue (PlayerPrefs.GetInt ("coins"));
		resetSkinsButtons ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void back() {
		Application.LoadLevel ("Menu");
	}

	public static void loadcoinvalue(int coinsvalue) {
		coins.text = "Total Coins: " + coinsvalue;
	}
	public void resetSkinsButtons() {
		foreach (Button button in skinsButtons) {
			currentskins = ShopScript.currentskins;
			bool contains = false;
			//checks if already purchased
			for (int i = 0; i < currentskins.Length; i++) {
				if(currentskins[i] == button.name) {
					contains = true;
				}
			}
			
			//handle Color when items are loaded.
			Image itemImage = button.image;
			if (PlayerPrefs.GetString ("currentskin") == button.name) {
				//if item is currently used
				itemImage.color = new Color (0.34509f, 0.94509f, 1f);
				Debug.Log (button.name);
			} else if (!(PlayerPrefs.GetString ("currentskin") == button.name) && contains) {
				//if item is bought but not currently used
				itemImage.color = new Color (1f, 1f, 1f);
				Debug.Log (button.name);
			} else {
				//set to unknown picture
				Debug.Log ("setting to unknown");
				button.image.overrideSprite = unknownImage;
				Debug.Log (button.name);
			}
			//unknown does not have a configuration because item will stay black despite color because black things tinted different colors are still black.
			button.image = itemImage;
		}
	}
}
