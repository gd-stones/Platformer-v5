using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Ads Control", fileName = "Ads Control")]
public class AdsControlSO : ScriptableObject
{
    [SerializeField] AdsDataSO _adsData;

    [Space]
    [SerializeField] bool _bannerEnabled;
    [SerializeField] bool _interstitialEnabled;
    [SerializeField] bool _rewardedEnabled;

    private void OnValidate()
    {
        if (_adsData == null)
        {
            Debug.LogWarning("Ads Data References not set in the Inspector!");
        }
        else
        {
            _adsData.controlBanner = _bannerEnabled;
            _adsData.controlInterstitial = _interstitialEnabled;
            _adsData.controlRewarded = _rewardedEnabled;
        }

    }
}
