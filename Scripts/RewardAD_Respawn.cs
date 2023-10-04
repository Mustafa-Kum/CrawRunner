using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class RewardAD_Respawn : MonoBehaviour
{
    [SerializeField] private string androidID;
    [SerializeField] private string iosID;

    private string _adUnitId;

    private void InitADUnit()
    {
#if UNITY_ANDROID
        this._adUnitId = androidID;
#elif UNITY_IPHONE
        this._adUnitId = iosID;
#else
        this._adUnitId = "unused";
#endif
    }

    private void Awake()
    {
        InitADUnit();
    }

    private RewardedAd rewardedAd;

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
        RegisterEventHandlers(rewardedAd);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                ADController.Instance.GiveRespawnReward();
                LoadRewardedAd();
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }
}
