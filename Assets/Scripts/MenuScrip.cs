using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScrip : MonoBehaviour
{
    public void Play(){
        if(PlayerPrefs.HasKey("Level")){
            
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));

        }else{

            SceneManager.LoadScene(1);

        }
    }

    public void Exit(){
        Application.Quit();
    }
}
