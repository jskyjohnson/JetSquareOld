/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;
using Soomla.Store;
	
	/// <summary>
	/// This class defines our game's economy, which includes virtual goods, virtual currencies
	/// and currency packs, virtual categories
	/// </summary>
	public class AppAssets : IStoreAssets{
		
		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion() {
			return 0;
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies() {
			return new VirtualCurrency[]{};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods() {
			return new VirtualGood[] {COINS_200_ITEM, NO_ADS_LTVG};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return new VirtualCurrencyPack[] {};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{};
		}
		
		/** Static Final Members **/
		
		public const string COINS_200_ID = "coins200";

		public const string NO_ADS_LIFETIME_PRODUCT_ID = "android.test.purchased";//"no_ads";
		
		public static VirtualGood COINS_200_ITEM = new SingleUseVG(
			"200 Coins",                                       		// name
			"Get 200 Coins", // description
			"coins200",                                       		// item id
			new PurchaseWithMarket(new MarketItem(COINS_200_ID, 0.99))); // the way this virtual good is purchased

		/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		public static LifetimeVG NO_ADS_LTVG = new LifetimeVG(
			"No Ads", 														// name
			"No More Ads!",				 									// description
			"no_ads",														// item id
			new PurchaseWithMarket(new MarketItem(NO_ADS_LIFETIME_PRODUCT_ID, 0.99)));	// the way this virtual good is purchased
	}
	