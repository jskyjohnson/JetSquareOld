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
	// Use this for initialization
	void Start () {
		ShopMenu = ShopMenu.GetComponent<Canvas> ();
		currentskins = PlayerPrefsX.GetStringArray ("purchased");
		coins = ShopMenu.GetComponentInChildren<Text> ();
		loadcoinvalue (PlayerPrefs.GetInt ("coins"));
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
}
