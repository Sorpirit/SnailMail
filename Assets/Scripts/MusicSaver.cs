using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSaver : MonoBehaviour
{
    private static MusicSaver saver;
   
    private void Awake() {
        if(saver == null){
            saver = this;
        }else if(saver != this){
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        AudioSource source = GetComponent<AudioSource>();
        if(source != null && !source.isPlaying){
            source.Play();
        }
    }

}
