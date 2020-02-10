using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCondition : MonoBehaviour
{
    public float ReqScore{get => reqTotalScore;}
    [SerializeField] private float reqTotalScore;
    [SerializeField] private int nextSceneNum;

    public void TryLoadNext(){
        if(GameManager.instance.Total >= reqTotalScore){
            PlayerPrefs.SetInt("Level",nextSceneNum);
            SceneManager.LoadScene(nextSceneNum);
        }
    }
}
