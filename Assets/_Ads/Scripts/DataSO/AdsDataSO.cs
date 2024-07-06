using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Ads Data", fileName = "Ads Data")]
public class AdsDataSO : ScriptableObject
{
    [SerializeField] int _interstitialAdInterval;
    [SerializeField] float _minDelayBetweenInterstitial = 20f;

    [SerializeField] [Range(0f, 1f)] float _rewardedAdFrequency = .5f;
    [SerializeField] float _minDelayBetweenRewarded = 20f;

    [SerializeField] bool _GDPR = true;
    public bool HasGDPR => _GDPR;
    public bool EnableGDPR
    {
        set
        {
            _GDPR = value;
        }
    }

    [SerializeField] AdsType _adsType;
    public AdsType GetAdsType => _adsType;

    [SerializeField] bool _enableBanner = true;
    [SerializeField] bool _enableInterstitial = true;
    [SerializeField] bool _enableRewarded = true;

    public bool controlBanner = true;
    public bool controlInterstitial = true;
    public bool controlRewarded = true;

    // ADMOB
    // Test App Id = "ca-app-pub-3940256099942544~3347511713";
    [SerializeField] [TextArea(1, 2)] string idBanner = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] [TextArea(1, 2)] string idInterstitial = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] [TextArea(1, 2)] string idReward = "ca-app-pub-3940256099942544/5224354917";

    public int InterstitialAdInterval => _interstitialAdInterval;
    public float RewardedAdFrequency => _rewardedAdFrequency;

    public float MinDelayBetweenInterstitial => _minDelayBetweenInterstitial;
    public float MinDelayBetweenRewarded => _minDelayBetweenRewarded;

    public bool BannerEnabled => _enableBanner;
    public bool InterstitialEnabled => _enableInterstitial;
    public bool RewardedEnabled => _enableRewarded;


    public string BannerID
    {
        get
        {
            return idBanner;
        }
    }

    public string InterstitialID
    {
        get
        {
            return idInterstitial;
        }
    }

    public string RewardedID
    {
        get
        {
            return idReward;
        }
    }
}

public enum AdsType { Admob }
//public enum BannerPos { Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight, Center }
