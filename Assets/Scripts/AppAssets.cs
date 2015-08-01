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
			return 2;
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
			return new VirtualGood[] {COINS_1200_ITEM, COINS_3000_ITEM, COINS_10000_ITEM, NO_ADS_LTVG};
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
		
		public const string COINS_1200_ID = "coins1200";

		public const string COINS_3000_ID = "coins3000";

		public const string COINS_10000_ID = "coins10000";

		public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";//"no_ads";
		
		public static SingleUseVG COINS_1200_ITEM = new SingleUseVG(
			"1200 Coins",                                       		// name
			"Get 1200 Coins", // description
			"coins1200",                                       		// item id
			new PurchaseWithMarket(new MarketItem(COINS_1200_ID, 0.99))); // the way this virtual good is purchased

		public static SingleUseVG COINS_3000_ITEM = new SingleUseVG(
			"3000 Coins",                                       		// name
			"Get 3000 Coins", // description
			"coins3000",                                       		// item id
			new PurchaseWithMarket(new MarketItem(COINS_3000_ID, 1.99))); // the way this virtual good is purchased

		public static SingleUseVG COINS_10000_ITEM = new SingleUseVG(
			"10000 Coins",                                       		// name
			"Get 10000 Coins", // description
			"coins10000",                                       		// item id
			new PurchaseWithMarket(new MarketItem(COINS_10000_ID, 2.99))); // the way this virtual good is purchased

	/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		public static LifetimeVG NO_ADS_LTVG = new LifetimeVG(
			"No Ads", 														// name
			"No More Ads!",				 									// description
			"no_ads",														// item id
			new PurchaseWithMarket(new MarketItem(NO_ADS_LIFETIME_PRODUCT_ID, 0.99)));	// the way this virtual good is purchased
	}
	