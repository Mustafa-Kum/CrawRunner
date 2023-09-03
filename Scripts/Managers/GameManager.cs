using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Components")]
    [Space]
    public Player player;
    public UI_Main ui;

    [Header("Skybox Materials")]
    [Space]
    [SerializeField] private Material[] skyBoxMat;

    [Header("Purchased Color Info")]
    [Space]
    public Color platformColor;

    [Header("Score Info")]
    [Space]
    public float distance;
    public float score;
    public int coins;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        SetupSkyBox(PlayerPrefs.GetInt("SkyBoxSettings"));
        LoadColor();
        LoadPlatformColor();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    public void SetupSkyBox(int i)
    {
        if (i <= 1)
            RenderSettings.skybox = skyBoxMat[i];
        else
            RenderSettings.skybox = skyBoxMat[Random.Range(0, skyBoxMat.Length)];

        PlayerPrefs.SetInt("SkyBoxSettings", i);
    }

    public void SavePlatformColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("PlatformColorR", r);
        PlayerPrefs.SetFloat("PlatformColorG", g);
        PlayerPrefs.SetFloat("PlatformColorB", b);
    }

    private void LoadPlatformColor()
    {
        Color platformColor = new Color(PlayerPrefs.GetFloat("PlatformColorR", 255),
                                        PlayerPrefs.GetFloat("PlatformColorG", 255),
                                        PlayerPrefs.GetFloat("PlatformColorB", 255),
                                        PlayerPrefs.GetFloat("PlatformColorA", 1));

        this.platformColor = platformColor;
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
        SceneManager.LoadScene(0);
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
        SaveInfo();
        ui.OpenEndGameUI();
    }
}
