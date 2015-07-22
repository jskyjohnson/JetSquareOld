using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Soomla;
using Soomla.Store;


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

			//StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
			SoomlaStore.Initialize (new AppAssets ());
			//StoreEvents.OnItemPurchaseStarted += (PurchasableVirtualItem pvi, string str, Dictionary<string, string> dic) => {};
			StoreEvents.OnItemPurchased += (PurchasableVirtualItem pvi, string payload) => { 
				switch(pvi.ID) {
				case AppAssets.COINS_200_ID:
					PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 200);
					loadcoinvalue (PlayerPrefs.GetInt ("coins"));
					break;
				case AppAssets.NO_ADS_LIFETIME_PRODUCT_ID:
				Debug.Log ("THIS HSOULD HSOW UP!!!!!");
					break;
				}
			};
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
		public void buy(String name) {
		Debug.Log ("called");
			foreach (VirtualGood vg in StoreInfo.Goods) {
				if(vg.Name == name) {
					try {
						StoreInventory.BuyItem (vg.ItemId);
					} catch (Exception e) {
						Debug.Log ("SOOMLA" + e.Message);
					}
				}
			}
		}
	}