using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManagerScript : MonoBehaviour, IUnityAdsListener
{
    public GameObject gameManager;

#if UNITY_IOS
    private string gameId = "4293132";
    string mySurfacingId = "Rewarded_iOS";

#elif UNITY_ANDROID
    private string gameId = "4293133";
    string mySurfacingId = "Rewarded_Android";
#endif

    private static int addCounter = 0;
    private bool getReward = false;
    bool testMode = false;

    public bool inGame;
    public bool mainMenu;

    private void Start()
    {
        Initialise();

        if (inGame)
            addCounter++;

        if (mainMenu)
        {
            if (addCounter >= 4)
            {
                ShowInterstitialAd();
                addCounter = 0;
                //Debug.Log("Show ad");
            }
        }
    }
    void Initialise()
    {
        Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        getReward = false;
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
        Initialise();
    }


    public void ShowRewardedVideo()
    {
        getReward = true;
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            //Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
        Initialise();
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (getReward)
            {
                //give reward
                gameManager.GetComponent<SinglePlayerStructure>().RewardedRedoShape();
            }

        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            //Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == mySurfacingId)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }



    public void AddCountdown()
    {
        addCounter++;
        Debug.Log(addCounter);
        if (addCounter >= 4)
        {
            ShowInterstitialAd();
            addCounter = 0;
            //Debug.Log("Show ad");
        }
    }
}
