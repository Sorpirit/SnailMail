using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource source;

    public static SoundManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
            return;
        }
    }

    

    public void Play(SoundEffect effect){
        Play((int) effect);
    }

    public void Play(int index){
        source.clip = clips[index];
        source.Play();
    }
}
