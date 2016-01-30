using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
//#if UNITY_EDITOR
//using UnityEditor
//#endif


public class AudioManager : MonoBehaviour {

	// Use this for initialization
	public AudioSource efxSource;
	public AudioSource musicSource;

	// for paused/unpaused transitions
	public AudioMixerSnapshot paused; 
	public AudioMixerSnapshot unpaused; 


	public static AudioManager instance = null;

	// pitch variations for pitch of sound effects
	public float lowPitchRange = .95f; 
	public float highPitchRange = 1.05f;


    public AudioClip sheep1;
    public AudioClip sheep2;
    public AudioClip sheep3;

    public AudioClip bounce1;
    public AudioClip bounce2;

    public AudioClip burnt;


	void Awake () {
		if (instance == null) {
			instance = this;
		}
//		} else if (instance != this) {
//			Destroy (gameObject);
//		}
		
//		DontDestroyOnLoad (gameObject); 
//		musicSource.Play (); 
	}


	public void PlaySingleEffect (AudioClip clip) {
		efxSource.clip = clip;
		efxSource.Play();
	}
		
	// can input objects as a comma separated list
	// and will be inserted into the array because of params keyword
	public void randomizeSheepVocals(params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length); 
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			
		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play (); 
	}

	public void PlayRandomSounds(params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length); 
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play (); 
	}

	// call this when pause has been done
	public void playPauseSound() {
		paused.TransitionTo (.01f);
//		if (Time.timeScale == 0) {
//			// Lowpass ();
//			paused.TransitionTo(.01f); 
//		}
//		else {
//			unpaused.TransitionTo(.01f); 
//		}
	}

	public void unPausedMusic() {
		unpaused.TransitionTo (.01f);
	}



    public void playBounce(){
        PlayRandomSounds(bounce1, bounce2);
    }


    public void playSheep(){
        PlayRandomSounds(sheep1, sheep2, sheep3);
    }

    public void playBurnt(){
        PlaySingleEffect(burnt);
    }



		
}
