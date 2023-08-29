using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public AudioSource[] effectSources;
    public float[] effectDefaultVolumes;

    public AudioSource[] musicSources;

    public int slideIndex;
    public int windIndex;

    public GameObject player;
    Rigidbody2D rb;
    playerManager playerManagerScript;

    float volumeMultiplier = 1f;

    void Start() {
        rb = player.GetComponent<Rigidbody2D>();
        playerManagerScript = player.GetComponent<playerManager>();
    }

    void Update() {
    // update audio levels based on velocity
        if (playerManagerScript.grounded) {
            effectSources[slideIndex].volume = rb.velocity.magnitude/100f*volumeMultiplier;
        }
        effectSources[windIndex].volume = (rb.velocity.magnitude/50f+0.1f)*volumeMultiplier;
    }

    public void playSound(AudioSource sound) {
        sound.Play();
    }

    public void stopSound(AudioSource sound) {
        sound.Stop();
    }

    // any float, defaults to 1
    public void changeMasterVolume(float value) {
        AudioListener.volume = value;
    }

    // float 0 to 1
    public void changeEffectVolume(float value) {
        for (int i = 0; i < effectSources.Length; i++) {
            if (i != windIndex && i != slideIndex) {
                effectSources[i].volume = effectDefaultVolumes[i]*value;
            }
        }

        volumeMultiplier = value;
    }

    // float 0 to 1
    public void changeMusicVolume(float value) {
        for (int i = 0; i < musicSources.Length; i++) {
            musicSources[i].volume = value;
        }
    }
}
