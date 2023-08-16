using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    public UI_Main ui;

    [Header("Skybox Materials")]
    [SerializeField] private Material[] skyBoxMat;
    
    [Header("Purchased Color Info")]
    public Color platformColor;

    [Header("Score Info")]
    public float distance;
    public float score;
    public int coins;

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1;

        SetupSkyBox(PlayerPrefs.GetInt("SkyBoxSettings"));

        //LoadColor();
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

    public void SaveColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("ColorR", r);
        PlayerPrefs.SetFloat("ColorG", g);
        PlayerPrefs.SetFloat("ColorB", b);
    }

    private void LoadColor()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        Color newColor = new Color(PlayerPrefs.GetFloat("ColorR"),
                                   PlayerPrefs.GetFloat("ColorG"),
                                   PlayerPrefs.GetFloat("ColorB"),
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

        score = distance * coins;

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
