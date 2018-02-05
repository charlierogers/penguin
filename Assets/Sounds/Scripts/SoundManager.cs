using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource soundEffects;
    public AudioSource backgroundMusic;
    public static SoundManager instance = null;

    public AudioClip jumpSound;
    public AudioClip ballBounceSound;
    public AudioClip ballDieSound;

	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayJumpSound() {
        soundEffects.clip = jumpSound;
        soundEffects.Play();
    }

    public void PlayBallBounceSound() {
        soundEffects.clip = ballBounceSound;
        soundEffects.Play();
    }

    public void PlayBallDieSound() {
        soundEffects.clip = ballDieSound;
        soundEffects.Play();
    }
}
