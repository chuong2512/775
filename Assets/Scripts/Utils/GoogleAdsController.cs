using System;
using UnityEngine;
using System.Collections;

// Google ads

public class GoogleAdsController : MonoBehaviour 
{
    public static GoogleAdsController instance = null;


	// Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "INSERT_ANDROID_BANNER_AD_UNIT_ID_HERE";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Create a banner.
       
    }

    public void ShowBanner()
    {
        
    }

    public void HideBanner()
    {
     
    }

    public void RequestInterstitial()
    {
        // http://stackoverflow.com/questions/12553929/is-there-any-admob-dummy-id
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712"; // Test Interstitial ID
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
    }

    // Returns an ad request with custom ad targeting.
    /*private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
            //.AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("358C650724C178EEF6A9F24E3CCB5002")
            //.AddKeyword("game")
            //.SetGender(Gender.Male)
            //.SetBirthday(new DateTime(1985, 1, 1))
            //.TagForChildDirectedTreatment(false)
            //.AddExtra("color_bg", "9B30FF")
            .Build();
    }*/

    public void ShowInterstitial()
    {
      

        RequestInterstitial();
    }

  
}
