using UnityEngine;
using System.Collections;

// Unity Ads
using UnityEngine.Advertisements;

// Soomla store
#if UNITY_ANDROID || UNITY_IOS
using Soomla;
using Soomla.Store;
#endif

public class ShopManager : MonoBehaviour 
{
    public bool clicking;

    public static ShopManager instance = null;

    void Awake()
    {
        //print("Init ShopManger");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            //print("Object exist");

            //Destroy(gameObject);
        }
    }

    public void BuyButtonClick(int product)
    {
        // avoid multiple click
        if (clicking == true) return;

        clicking = true;

        StartCoroutine(ResetButtonClick());

#if UNITY_EDITOR

        int productCoin = 0;

        switch (product)
        {
            case 1:
                productCoin = Configure.instance.product1Coin;
                break;
            case 2:
                productCoin = Configure.instance.product2Coin;
                break;
            case 3:
                productCoin = Configure.instance.product3Coin;
                break;
            case 4:
                productCoin = Configure.instance.product4Coin;
                break;
            case 5:
                productCoin = Configure.instance.product5Coin;
                break;
        }

        // plus coin
        GameData.instance.SavePlayerCoin(GameData.instance.GetPlayerCoin() + productCoin);

        // play add coin sound
        AudioManager.instance.CoinAddAudio();

        // update text label
        UpdateCoinAmountLabel();

#elif UNITY_ANDROID || UNITY_IOS

        // charge money then add coin

        switch (product)
        {
            case 1:
                StoreInventory.BuyItem("product_1");
                break;
            case 2:
                StoreInventory.BuyItem("product_2");
                break;
            case 3:
                StoreInventory.BuyItem("product_3");
                break;
            case 4:
                StoreInventory.BuyItem("product_4");
                break;
            case 5:
                StoreInventory.BuyItem("product_5");
                break;
        }

#endif

    }

    public void UpdateCoinAmountLabel()
    {
        if (gameObject.GetComponent<UIShopPopupPlay>())
        {
            gameObject.GetComponent<UIShopPopupPlay>().UpdateCoinAmountLabel();

            // also update on lose popup
            var losePopup = GameObject.Find("LosePopup(Clone)");

            if (losePopup)
            {
                losePopup.GetComponent<UILosePopup>().coinText.text = GameData.instance.GetPlayerCoin().ToString();
            }
        }
        else if (GameObject.Find("MapScene"))
        {
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
        }
    }

    IEnumerator ResetButtonClick()
    {
        yield return new WaitForSeconds(1f);

        clicking = false;
    }

    public void WatchVideoForCoinButtonClick()
    {
        // avoid multiple click
        if (clicking == true) return;

        clicking = true;

        StartCoroutine(ResetButtonClick());

      
    }

   

    public void FbLoginPlusCoin()
    {
        //print("Facebook login plus coin");

        // plus coin
        GameData.instance.SavePlayerCoin(GameData.instance.GetPlayerCoin() + Configure.instance.FBLoginCoin);

        // play add coin sound
        AudioManager.instance.CoinAddAudio();

        // update text label
        UpdateCoinAmountLabel();
    }
}
