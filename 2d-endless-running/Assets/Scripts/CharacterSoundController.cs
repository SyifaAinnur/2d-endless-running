﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public AudioClip jump;
    private AudioSource audioPlayer;
    public AudioClip scoreHighlight;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //jump music play
    public void PlayJump(){
        audioPlayer.PlayOneShot(jump);
    }
    public void PlayScoreHighlight(){
        audioPlayer.PlayOneShot(scoreHighlight);
    }
}
