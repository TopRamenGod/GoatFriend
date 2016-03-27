using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using GoatFriend.Events;
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

	// pitch variations for pitch of sound effects
	public float lowPitchRange = .95f; 
	public float highPitchRange = 1.05f;


    public AudioClip sheep1;
    public AudioClip sheep2;
    public AudioClip sheep3;

    public AudioClip bounce1;
    public AudioClip bounce2;

    public AudioClip burnt;

    public AudioClip collect;

    public AudioClip portalOpen;



//		} else if (instance != this) {
//			Destroy (gameObject);
//		}
		
//		DontDestroyOnLoad (gameObject); 
//		musicSource.Play (); 
	
    void Start(){
        
        EventSystem.Instance.GoatDied.AddListener(new SimpleFunc(() => {
            PlaySingleEffect(burnt);
        }));

//        EventSystem.Instance.AddListener( new SimpleFunc( () => {
//            PlaySingleEffect(portalOpen);
//        }));

        EventSystem.Instance.GoatReleased.AddListener(new SimpleFunc(() => {
            PlayRandomSounds(sheep1, sheep2, sheep3);
        }));

        EventSystem.Instance.GoatHit.AddListener( new SimpleFunc(() => {
            PlayRandomSounds(sheep1, sheep2, sheep3);
        }));

        EventSystem.Instance.GoatBounced.AddListener( new SimpleFunc(() => {
            PlayRandomSounds(bounce1, bounce2);
        }));

        EventSystem.Instance.HeartCollected.AddListener( new DataFunc<int>((hearts) => {
            Debug.Log("Collected:" + hearts);
            PlaySingleEffect(collect);
        }));

//        EventSystem.Instance.GamePaused.AddListener( new SimpleEvent(() => {
//            paused.TransitionTo (.01f);
//        }));
//
//        EventSystem.Instance.GameResumed.AddListener( new SimpleEvent(() => {
//            unpaused.TransitionTo (.01f);
//        }));
    }


	private void PlaySingleEffect (AudioClip clip) {
		efxSource.clip = clip;
		efxSource.Play();
	}
		
	// can input objects as a comma separated list
	// and will be inserted into the array because of params keyword
	private void randomizeSheepVocals(params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length); 
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			
		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play (); 
	}

	private void PlayRandomSounds(params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length); 
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play (); 
	}

		
}
