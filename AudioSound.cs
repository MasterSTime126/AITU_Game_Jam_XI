using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
        DontDestroyOnLoad(gameObject);
    }
}
