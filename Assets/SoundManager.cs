using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	public AudioSource fxSource;
	public AudioSource musicSource;
	public static SoundManager instance = null;

	public float lowPitch = .95f;
	public float highPitch = 1.05f;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	public void PlaySing(AudioClip clip){
		fxSource.clip = clip ;
		fxSource.Play ();
	}
	public void RandomPitch(AudioClip clip){
		float randomPitch = Random.Range (lowPitch, highPitch);
		fxSource.pitch = randomPitch;
		fxSource.clip = clip;
		fxSource.Play ();
	}

}
