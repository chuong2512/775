using UnityEngine;
using System.Collections;


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

        int productCoin = 0;

        switch (product)
        {
            case 1:
                productCoin = Configure.instance.product1Coin;
                IAPManager.OnPurchaseSuccess = () => AddCoin(productCoin);
                IAPManager.Instance.BuyProductID(IAPKey.PACK1_RE);
                break;
            case 2:
                //productCoin = Configure.instance.product2Coin;
                break;
            case 3:
                productCoin = Configure.instance.product3Coin;
                IAPManager.OnPurchaseSuccess = () => AddCoin(productCoin);
                IAPManager.Instance.BuyProductID(IAPKey.PACK2_RE);
                break;
            case 4:
                productCoin = Configure.instance.product4Coin;
                IAPManager.OnPurchaseSuccess = () => AddCoin(productCoin);
                IAPManager.Instance.BuyProductID(IAPKey.PACK3_RE);
                break;
            case 5:
                productCoin = Configure.instance.product5Coin;
                IAPManager.OnPurchaseSuccess = () => AddCoin(productCoin);
                IAPManager.Instance.BuyProductID(IAPKey.PACK4_RE);
                break;
        }
    }

    public void AddCoin(int productCoin)
    {
        // plus coin
        GameData.instance.SavePlayerCoin(GameData.instance.GetPlayerCoin() + productCoin);

        // play add coin sound
        AudioManager.instance.CoinAddAudio();

        // update text label
        UpdateCoinAmountLabel();
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