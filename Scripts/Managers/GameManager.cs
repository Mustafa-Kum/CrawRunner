using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Components")]
    [Space]
    public Player player;
    public UI_Main ui;
    public UI_Shop uiShop;
    public LevelGenerator levelGenerator;

    [Header("Purchased Color Info")]
    [Space]
    public Color platformColor;

    [Header("Score Info")]
    [Space]
    public float distance;
    public float score;
    public int coins;

    private Vector3 respawnPoint;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        LoadColor();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
    }

    public void SaveColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("ColorR", r);
        PlayerPrefs.SetFloat("ColorG", g);
        PlayerPrefs.SetFloat("ColorB", b);
    }

    private void LoadColor()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        Color newColor = new Color(PlayerPrefs.GetFloat("ColorR", 255),
                                   PlayerPrefs.GetFloat("ColorG", 255),
                                   PlayerPrefs.GetFloat("ColorB", 255),
                                   PlayerPrefs.GetFloat("ColorA", 1));

        sr.color = newColor;
    }

    private void Update()
    {
        if (player.transform.position.x > distance)
            distance = player.transform.position.x;
    }

    public void UnlockPlayer() => player.playerUnlocked = true;

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SaveInfo()
    {
        int savedCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", savedCoins + coins);
        score = distance;
        PlayerPrefs.SetFloat("LastScore", score);

        if (PlayerPrefs.GetFloat("HighScore") < score)
            PlayerPrefs.SetFloat("HighScore", score);
    }

    public void GameEnded()
    {
        ui.OpenEndGameUI();
    }

    public void SaveInfoButton()
    {
        SaveInfo();
    }

    private void OnEnable()
    {
        ADController.OnGaveReward += RewardCoin; // 200 Coin
        ADController.OnGaveRewardRespawn += player.Respawn; // Respawn
        ADController.OnGaveRewardRandomColor += RewardRandomColor; // RandomColor
    }

    private void OnDisable()
    {
        ADController.OnGaveReward -= RewardCoin; // 200 Coin
        ADController.OnGaveRewardRespawn -= player.Respawn; // Respawn
        ADController.OnGaveRewardRandomColor -= RewardRandomColor; // RandomColor
    }

    private void RewardCoin()
    {
        int rewardCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", rewardCoins + 200);
    }

    public void SetRespawnPoint(Vector3 point)
    {
        respawnPoint = point;
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = respawnPoint;
    }

    public void RewardRandomColor()
    {
        int randomIndex = Random.Range(0, uiShop.playerColors.Length);
        Color selectedColor = uiShop.playerColors[randomIndex].color;
        uiShop.RewardRandomColor(selectedColor, 0, ColorType.playerColor);
    }
}
