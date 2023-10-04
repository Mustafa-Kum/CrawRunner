using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    private bool gamePaused;
    private bool gameMuted;
    private float muteButtonAlpha;

    [Header("Components")]
    [Space]
    public GameObject mainMenu;
    public GameObject endGame;
    public GameObject inGameUI;

    [Header("Text Info")]
    [Space]
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinsText;

    [Header("Volume Info")]
    [Space]
    [SerializeField] private UI_VolumeSlider[] slider;
    [SerializeField] private Image muteIcon;
    [SerializeField] private Image inGameMuteIcon;

    private void Start()
    {
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i].SetupSlider();
        }

        SwitchMenuTo(mainMenu);

        lastScoreText.text = ": " + PlayerPrefs.GetFloat("LastScore").ToString("#,#");
        highScoreText.text = ": " + PlayerPrefs.GetFloat("HighScore").ToString("#,#");

        float savedVolume = PlayerPrefs.GetFloat("AudioVolume", 1.0f);
        AudioListener.volume = savedVolume;
        gameMuted = PlayerPrefs.GetInt("GameMuted", 0) == 1;
        muteButtonAlpha = PlayerPrefs.GetFloat("MuteButtonAlpha", 1.0f);
        UpdateMuteButtonAlpha();
    }

    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

        if (GameManager.instance.player.isDead == false)
            AudioManager.instance.PlaySFX(8);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
    }

    public void SwitchMenuToRespawn(GameObject uiMenu)
    {
        if (!GameManager.instance.player.respawnUsed)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            uiMenu.SetActive(true);

            AudioManager.instance.PlaySFX(8);

            coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");

        }
    }

    public void SwitchSkyBox(int index)
    {
        AudioManager.instance.PlaySFX(8);
    }

    public void MuteButton()
    {
        gameMuted = !gameMuted;

        if (gameMuted)
        {
            muteButtonAlpha = 0.5f;
            AudioListener.volume = 0;
        }
        else
        {
            muteButtonAlpha = 1.0f;
            AudioListener.volume = 1;
        }

        PlayerPrefs.SetFloat("AudioVolume", AudioListener.volume);
        PlayerPrefs.SetInt("GameMuted", gameMuted ? 1 : 0);
        PlayerPrefs.SetFloat("MuteButtonAlpha", muteButtonAlpha);
        PlayerPrefs.Save();

        UpdateMuteButtonAlpha();
    }

    private void UpdateMuteButtonAlpha()
    {
        Color muteIconColor = muteIcon.color;
        muteIconColor.a = muteButtonAlpha;
        muteIcon.color = muteIconColor;

        inGameMuteIcon.color = muteIconColor;
    }

    public void StartGameButton()
    {
        GameManager.instance.UnlockPlayer();
    }

    public void PauseGameButton()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
    }

    public void RestartGameButton() => GameManager.instance.RestartLevel();

    public void OpenEndGameUI()
    {
        SwitchMenuTo(endGame);
    }
}
