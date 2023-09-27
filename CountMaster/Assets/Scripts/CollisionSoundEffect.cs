using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundEffect : MonoBehaviour
{

    public AudioClip collisionEffect;
    AudioSource soundDed;
    void Start()
    {
        soundDed = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            soundDed.PlayOneShot(collisionEffect, 0.8f);
        }
    }
}

