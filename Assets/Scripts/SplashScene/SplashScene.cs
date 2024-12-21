using UnityEngine;
using System.Collections;

// Soomla store
#if UNITY_ANDROID || UNITY_IOS
using Soomla;
using Soomla.Store;
using Soomla.Profile;
#endif

public class SplashScene : MonoBehaviour 
{
    void Start()
    {
        // Request Interstitial Google Ads
        GoogleAdsController.instance.RequestInterstitial();

        // Soomla Store
        StoreEventHandler.instance.InitializeEventHandler();
     
    }

    public void TouchStartClick()
    {
        GetComponent<SceneTransition>().PerformTransition();
    }
}
