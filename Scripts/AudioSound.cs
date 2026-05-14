using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] circles;
    [SerializeField] private AudioClip winningSound;

    [SerializeField] private AudioClip waveMusic;
    [SerializeField] private AudioClip ordinaryMusic;
    [SerializeField] private AudioClip intenseMusic;
    
    private AudioSource waveSource;
    private AudioSource ordinarySource;
    private AudioSource intenseSource;
    private AudioSource winningSource;

    public static AudioSound instance;

    void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        waveSource = sources[0];
        ordinarySource = sources[1];
        intenseSource = sources[2];
        winningSource = sources[3];

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        waveSource.clip = waveMusic;
        ordinarySource.clip = ordinaryMusic;
        intenseSource.clip = intenseMusic;
        winningSource.clip = winningSound;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveSource.loop = true;
        waveSource.volume = 0.5f;
        waveSource.Play();

        ordinarySource.loop = true;
        ordinarySource.volume = 0f;
        ordinarySource.Play();

        intenseSource.loop = true;
        intenseSource.volume = 0f;
        intenseSource.Play();

    }


    public void LevelCompleted()
    {
        winningSource.PlayOneShot(winningSound);
    }

    public void PlayOncePitch(int index)
    {
        if (index >= 0 && index < circles.Length)
        {
            AudioSource.PlayClipAtPoint(circles[index], Camera.main.transform.position);
        }
    }

    public void StartMusicTransition()
    {
        StartCoroutine(StartGameMusic());
    }

    public void TransitionToIntense()
    {
        StartCoroutine(TransitionToIntenseMusic());
    }

    public void PlayMenuMusic()
    {
        StartCoroutine(StartMenuMusic());
    }

    IEnumerator StartMenuMusic()
    {
        float transitionDuration = 2f;
        float elapsedTime = 0f;

        float initialWaveVolume = waveSource.volume;
        float initialOrdinaryVolume = ordinarySource.volume;
        float initialIntenseVolume = intenseSource.volume; 

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            waveSource.volume = Mathf.Lerp(initialWaveVolume, 0.5f, t);
            ordinarySource.volume = Mathf.Lerp(initialOrdinaryVolume, 0f, t);
            intenseSource.volume = Mathf.Lerp(initialIntenseVolume, 0f, t);

            yield return null;
        }

        waveSource.volume = 0.5f;
        ordinarySource.volume = 0f;
        intenseSource.volume = 0f;
    }

    IEnumerator StartGameMusic()
    {
        float transitionDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            waveSource.volume = Mathf.Lerp(0.5f, 0f, t);
            ordinarySource.volume = Mathf.Lerp(0f, 0.5f, t);
            intenseSource.volume = Mathf.Lerp(0f, 0f, t);

            yield return null;
        }

        waveSource.volume = 0f;
        ordinarySource.volume = 0.5f;
        intenseSource.volume = 0f;
    }

    IEnumerator TransitionToIntenseMusic()
    {
        float transitionDuration = 2f;
        float elapsedTime = 0f;

        float initialOrdinaryVolume = ordinarySource.volume;
        float goalOrdinaryVolume = Mathf.Max(0, ordinarySource.volume - 0.0385f);

        float initialIntenseVolume = intenseSource.volume;
        float goalIntenseVolume = intenseSource.volume + 0.0385f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            ordinarySource.volume = Mathf.Lerp(initialOrdinaryVolume, goalOrdinaryVolume, t);
            intenseSource.volume = Mathf.Lerp(initialIntenseVolume, goalIntenseVolume, t);

            yield return null;
        }

        ordinarySource.volume = goalOrdinaryVolume;
        intenseSource.volume = goalIntenseVolume;
    }
}
