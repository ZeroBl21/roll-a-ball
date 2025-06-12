using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;           // Clip de música a reproducir
    public float fadeInDuration = 3f;     // Duración del fade-in en segundos

    private AudioSource audioSource;
    private float targetVolume;           // Volumen final al que haremos el fade-in

    private static MusicPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ¡No destruir este objeto al cambiar escena!
        }
        else
        {
            Destroy(gameObject); // Ya existe otro MusicPlayer, destruye este duplicado
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetVolume = audioSource.volume;  // Guardamos el volumen configurado en el Inspector
        audioSource.clip = musicClip;
        audioSource.volume = 0f;            // Comienza desde silencio
        audioSource.loop = true;
        audioSource.Play();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeInDuration);
            audioSource.volume = t * targetVolume;
            yield return null;
        }

        audioSource.volume = targetVolume;  // Asegura el volumen final exacto
    }
}
