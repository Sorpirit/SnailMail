using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZeroBechaviour : MonoBehaviour
{
    
    [SerializeField] private float winDelay = 1f;
    private bool isWon = false;

    void Update()
    {
        if(!isWon && GameManager.instance.Total > 0){
            isWon =true;
            StartCoroutine(Win());
        }
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(winDelay);
        
            GameManager.instance.EndGame();
    }
}
