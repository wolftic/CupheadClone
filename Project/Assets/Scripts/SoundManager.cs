using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class SoundManager : MonoBehaviour {

    public static SoundManager current;
    [SerializeField]
    private AudioClip _backgroundMusic;
    private AudioSource _audioSource;
    [SerializeField][Range(0, 1)]
    private float _volume = 1f;

	void Start () {
        current = this;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _backgroundMusic;
        _audioSource.Play();
	}

    public void PlaySound(AudioClip audio)
    {
        _audioSource.PlayOneShot(audio, _volume);
    }
}
