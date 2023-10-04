using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
public class ADController : MonoBehaviour
{
    private RewardAD reward;
    private RewardAD_Respawn rewardRespawn;
    private RewardAD_RandomColor rewardRandomColor;

    public static ADController Instance;

    public delegate void OnReward();
    public delegate void OnRewardRespawn();
    public delegate void OnRewardRandomColor();
    public static event OnReward OnGaveReward;
    public static event OnRewardRespawn OnGaveRewardRespawn;
    public static event OnRewardRandomColor OnGaveRewardRandomColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        reward = GetComponent<RewardAD>();
        rewardRespawn = GetComponent<RewardAD_Respawn>();
        rewardRandomColor = GetComponent<RewardAD_RandomColor>();
    }
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            rewardRespawn.LoadRewardedAd();
        });

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            rewardRandomColor.LoadRewardedAd();
        });

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            reward.LoadRewardedAd();
        });
    }
    public void ShowRewardAd() // 200 Coin
    {
        reward.ShowRewardedAd();
    }
    public void ShowRewardAdRespawn() // Respawn
    {
        rewardRespawn.ShowRewardedAd();
    }
    public void ShowRewardAdRandomColor() // RandomColor
    {
        rewardRandomColor.ShowRewardedAd();
    }
    public void GiveReward() // 200 Coin
    {
        OnGaveReward?.Invoke();
    }
    public void GiveRespawnReward() // Respawn
    {
        OnGaveRewardRespawn?.Invoke();
    }
    public void GiveRandomColorReward() // RandomColor
    {
        OnGaveRewardRandomColor?.Invoke();
    }
}
