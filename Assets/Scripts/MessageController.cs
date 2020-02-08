using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {
    

    [SerializeField] private TMP_Text quoteField;
    public MessageTamplate Parent{get => parent;set => parent = value;}
    public Image clock;

    private MessageTamplate parent;
    private float messageTimerPireod;
    private float messageTimer;

    public void SetQuote(string quote){
        quoteField.text = quote;
    }
    public void StartTimer(float time){
        messageTimer = time;
        messageTimerPireod = time;
    }
    public void OnSpot(){
        Destroy(gameObject);
    }
    
    private void MessageExpierd(){
        GameManager.instance.MessageExpierd(parent);
        Destroy(gameObject);
    }

    private void Update() {
        messageTimer -= Time.deltaTime;
        clock.fillAmount = messageTimer / messageTimerPireod;

        if(messageTimer <= 0){
            MessageExpierd();
        }
    }
    
}