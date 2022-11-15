using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsListener
{
#if UNITY_ANDROID
    string gameId = "5008963";
    string Interstitial = "Interstitial_Android";
    string Rewarded = "Rewarded_Android";
    string Banner = "Banner_Anroid";

#elif UNITY_IOS
    string gameId = "5008962";
    string Interstitial = "Interstitial_iOS";
    string Rewarded = "Rewarded_iOS";
    string Banner = "Banner_iOS";
#endif

    BannerOptions bannerOptions = new BannerOptions();
    ShowOptions showOptions = new ShowOptions();
    // initialize the Ads listener and service:
    private void Start()
    {
        Advertisement.Initialize(gameId, testMode: true, enablePerPlacementLoad: true, initializationListener: this);
        Advertisement.AddListener(listener: this);
        bannerOptions.showCallback += OnShowBanner;
        bannerOptions.hideCallback += OnHideBanner;
        bannerOptions.clickCallback += OnClickBanner;
    }

    private void OnDestroy()
    {
        bannerOptions.showCallback -= OnShowBanner;
        bannerOptions.hideCallback -= OnHideBanner;
        bannerOptions.clickCallback -= OnClickBanner;
    }

    public void ShowInterstitial()
    {
        Advertisement.Load(Interstitial, this);
        Advertisement.Show(placementId: Interstitial, showOptions: showOptions, showListener: this);
    }
    public void ShowRewarded()
    {
        Advertisement.Load(Rewarded, this);
        Advertisement.Show(placementId: Rewarded, showOptions: showOptions, showListener: this);
    }

    public void ShowBanner()
    {
        Advertisement.Banner.Load(Banner);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId: Banner, options: bannerOptions);
    }
    public void HideBanner()
    {
        Advertisement.Banner.Hide(false);
    }


    //banner callback
    private void OnShowBanner()
    {
        Debug.Log("Banner is shown");
    }
    private void OnHideBanner()
    {
        Debug.Log("Banner is hidden");
    }
    private void OnClickBanner()
    {
        Debug.Log("Banner is clicked");
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(placementId: Banner, loadListener: this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Ads initialization failed: " + error + ", " + message);
    }


    // load callback
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Ad failed to load: " + placementId + ", " + error + ", " + message);
    }



    // show callback

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure: [" + placementId + "] [" + error + "] [" + message + "]");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart: [" + placementId + "]");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick: [" + placementId + "]");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete: [" + placementId + "] [" + showCompletionState + "]");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("OnUnityAdsReady: [" + placementId + "]");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("OnUnityAdsDidError: [" + message + "]");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("OnUnityAdsDidStart: [" + placementId + "]");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("OnUnityAdsDidFinish: [" + placementId + "] [" + showResult + "]");
    }
}
