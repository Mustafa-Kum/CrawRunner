using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private int bgmIndex;

    [Header("Components")]
    [Space]
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!bgm[bgmIndex].isPlaying)
            PlayRandomBGM();
    }

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlaySFX(int index)
    {
        if (index < sfx.Length)
        {
            float pitch = Random.Range(0.85f, 1.15f);
            sfx[index].pitch = pitch;
            sfx[index].Play();
        }
    }

    public void PlaySFXSlide(int index)
    {
        if (index < sfx.Length)
        {
            float pitch = Random.Range(0.55f, 0.65f);
            sfx[index].pitch = pitch;
            sfx[index].Play();
        }
    }

    public void StopSFX(int index)
    {
        sfx[index].Stop();
    }

    public void PlayBGM(int i)
    {
        StopBGM();
        bgm[i].Play();
    }

    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
