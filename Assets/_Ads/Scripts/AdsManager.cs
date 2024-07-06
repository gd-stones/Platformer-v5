using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : Singleton<AdsManager>
{
    [SerializeField] AdsDataSO _data;
    [SerializeField] Transform _adsTransform;

    Ads _ads;

    bool _isInterstitialShouldShow = false;

    bool _isInterstitialTimerPassed = false;
    float _interstitialTimer;

    static int _gameplayCount;

    public static event Action OnRewardedAdWatchedComplete;
    public static void HandleRewardedAdWatchedComplete() => OnRewardedAdWatchedComplete?.Invoke();

    public static event Action OnClosedInterstitial;
    public static void HandleClosedInterstitial() => OnClosedInterstitial?.Invoke();

    private void OnEnable() => SceneManager.sceneLoaded += HandleSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= HandleSceneLoaded;

    private void HandleSceneLoaded(Scene s, LoadSceneMode sm)
    {
        ShoudShow();

        ShowBanner();
    }

    void ShoudShow()
    {
        _gameplayCount++;

        int interval = Mathf.Clamp(_data.InterstitialAdInterval, 1, 100);

        if (_gameplayCount % interval == 0)
        {
            _isInterstitialShouldShow = true;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if(_ads == null)
        {
            _ads = GetAdsChoice();
        }
    }

    private void Start()
    {
        _interstitialTimer = _data.MinDelayBetweenInterstitial;
    }

    Ads GetAdsChoice()
    {
        _adsTransform = transform.GetChild(0);

        if (_adsTransform != null)
        {
            _adsTransform.gameObject.SetActive(true);
            _ads = _adsTransform.gameObject.GetComponent<Ads>();
        }
        return _ads;
    }

    private void Update()
    {
        if (!_isInterstitialTimerPassed && Time.time > _interstitialTimer)
        {
            _isInterstitialTimerPassed = true;
        }
    }

    public void ShowBanner()
    {
        _ads.ShowBanner();
    }

    public void ShowInterstitial(Action close)
    {
        try
        {
            ShoudShow();

            if (_isInterstitialTimerPassed && _isInterstitialShouldShow)
            {
                _isInterstitialShouldShow = false;

                _isInterstitialTimerPassed = false;
                _interstitialTimer = Time.time + _data.MinDelayBetweenInterstitial;

                _ads.ShowInterstitial();

                OnClosedInterstitial = close;
            }
            else
            {
                close();
            }
        }
        catch
        {
            close.Invoke();
        }
    }

    public void ShowRewarded(Action complete)
    {
        try
        {
            _ads.ShowRewarded();

            OnRewardedAdWatchedComplete = complete;
        }
        catch
        {
            complete.Invoke();
        }
    }
}