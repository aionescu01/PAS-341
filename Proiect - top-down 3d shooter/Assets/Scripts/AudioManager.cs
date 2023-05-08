using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;
    private Sound[] copysounds;

    AudioSource source;

    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup masterMixerGroup;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        copysounds = sounds;
    }

    void Update()
    {
        if (copysounds.Length == 0)
        {
            copysounds = sounds;
        }

        if (!source.isPlaying)
        {
            PlayRandom();
        }
    }

    public void PlayRandom()
    {
        source.outputAudioMixerGroup = musicMixerGroup;
        
        Sound s = copysounds[Random.Range(0, copysounds.Length)];
        source.clip = s.clip;
        
        source.Play();

        copysounds = copysounds.Where(snd => snd != s).ToArray();
    }
}