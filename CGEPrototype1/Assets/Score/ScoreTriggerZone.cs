using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    
    bool active = true;

    public AudioClip zoneSound;
    private AudioSource playerAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAudio = GetComponent<AudioSource>();

        if (active && collision.gameObject.tag == "Player") { 
        active = false;
        ScoreManager.score++;
            playerAudio.PlayOneShot(zoneSound, 1.0f);

        }
    }
}
