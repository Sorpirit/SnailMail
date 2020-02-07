using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Dictionary <MessageTamplate,int> deliveredMessages;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
            return;
        }

        deliveredMessages = new Dictionary<MessageTamplate, int>();
    }

    public void AddScores(MessageTamplate tamplate){
        if(!deliveredMessages.ContainsKey(tamplate))
            deliveredMessages.Add(tamplate,0);

        deliveredMessages[tamplate] += tamplate.ScoresCost;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
