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
		coinsvalue = PlayerPrefs.GetInt ("coins");
		coins = coins.GetComponent<Text> ();
		coins.text = "Total Coins: " + coinsvalue;
		currentskins = PlayerPrefsX.GetStringArray ("purchased");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void back() {
		Application.LoadLevel ("Menu");
	}
}
