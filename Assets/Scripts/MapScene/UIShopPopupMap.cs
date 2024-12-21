using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShopPopupMap : MonoBehaviour 
{
    public Image topBar;

    public Text coin1;
    public Text cost1;

    public Text coin2;
    public Text cost2;

    public Text coin3;
    public Text cost3;

    public Text coin4;
    public Text cost4;

    public Text coin5;
    public Text cost5;

	void Start () 
    {
        // shop top bar
        float height = ((float)Screen.height / (float)Screen.width) * 720f;

        topBar.GetComponent<RectTransform>().localPosition = new Vector3(0, height / 2f, 0);

        coin1.text = Configure.instance.product1Coin.ToString();
        coin2.text = Configure.instance.product2Coin.ToString();
        coin3.text = Configure.instance.product3Coin.ToString();
        coin4.text = Configure.instance.product4Coin.ToString();
        coin5.text = Configure.instance.product5Coin.ToString();

        cost1.text = "$" + Configure.instance.product1Price.ToString();
        cost2.text = "$" + Configure.instance.product2Price.ToString();
        cost3.text = "$" + Configure.instance.product3Price.ToString();
        cost4.text = "$" + Configure.instance.product4Price.ToString();
        cost5.text = "$" + Configure.instance.product5Price.ToString();
	}

    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
}
