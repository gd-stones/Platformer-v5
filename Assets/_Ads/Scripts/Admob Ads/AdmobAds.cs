using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine;

public class AdmobAds : Ads
{
    [SerializeField] AdsDataSO _data;

    [Space]
    [SerializeField] AdPosition _bannerPosition;
    [SerializeField]
    TagForChildDirectedTreatment _tagForChildDirectedTreatment;

    [HideInInspector] BannerView AdBanner;
    [HideInInspector] InterstitialAd AdInterstitial;
    [HideInInspector] RewardedAd AdReward;

    bool _isInitComplete = false;

    private void Start()
    {
        
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(_tagForChildDirectedTreatment)
                .build();

        MobileAds.Initialize(initstatus =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                _isInitComplete = true;

                RequestAndShowBanner();
                RequestRewardAd();
                RequestInterstitialAd();
            });
        });
    }

    private void OnDestroy()
    {
        DestroyBannerAd();
        DestroyInterstitialAd();
    }

    public override bool IsRewardedAdLoaded()
    {
        if (AdReward != null && AdReward.IsLoaded())
            return true;
        else
            return false;
    }

    AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
           //.TagForChildDirectedTreatment(false)
           .AddExtra("npa", PlayerPrefs.GetInt("npa", 1).ToString())
           .Build();
    }

    #region Banner Ad -----------------------------------------------------------------------------
    public override void ShowBanner()
    {
        if (!_isInitComplete) return;

        if(AdBanner != null)
        {
            AdBanner.Show();
        }
    }

    public void RequestAndShowBanner()
    {
        if (!_data.BannerEnabled) return;

        AdSize adaptiveSize = AdSize.Banner;
        AdBanner = new BannerView(_data.BannerID, adaptiveSize, _bannerPosition);

        AdBanner.LoadAd(CreateAdRequest());
    }

    public void DestroyBannerAd()
    {
        if (AdBanner != null)
        {
            AdBanner.Destroy();
        }
    }
    #endregion

    #region Interstitial Ad ------------------------------------------------------------------------
    public void RequestInterstitialAd()
    {
        if (!_data.InterstitialEnabled) return;

        AdInterstitial = new InterstitialAd(_data.InterstitialID);

        AdInterstitial.OnAdClosed += HandleInterstitialAdClosed;

        AdInterstitial.LoadAd(CreateAdRequest());
    }

    public override void ShowInterstitial()
    {
        if (AdInterstitial != null && AdInterstitial.IsLoaded())
            AdInterstitial.Show();
    }

    public void DestroyInterstitialAd()
    {
        if (AdInterstitial != null)
        {
            AdInterstitial.Destroy();
        }
    }
    #endregion

    #region Rewarded Ad ----------------------------------------------------------------------------
    public void RequestRewardAd()
    {
        if (!_data.RewardedEnabled) return;

        AdReward = new RewardedAd(_data.RewardedID);

        AdReward.OnAdClosed += HandleOnRewardedAdClosed;
        AdReward.OnUserEarnedReward += HandleOnRewardedAdWatched;

        AdReward.LoadAd(CreateAdRequest());
    }


    public override void ShowRewarded()
    {
        if (AdReward != null && AdReward.IsLoaded())
            AdReward.Show();
    }
    #endregion

    #region Event Handler
    private void HandleInterstitialAdClosed(object sender, EventArgs e)
    {
        DestroyInterstitialAd();
        RequestInterstitialAd();
        AdsManager.HandleClosedInterstitial();
    }

    private void HandleOnRewardedAdClosed(object sender, EventArgs e)
    {
        RequestRewardAd();
    }

    private void HandleOnRewardedAdWatched(object sender, Reward e)
    {
        AdsManager.HandleRewardedAdWatchedComplete();
    }
    #endregion
}
