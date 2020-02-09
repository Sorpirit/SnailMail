using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {
    

    [SerializeField] private TMP_Text quoteField;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer messageRenderer;
    [SerializeField] private ParticleSystem dustRffect;
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
    public void SetSprite(Sprite spr){
        messageRenderer.sprite = spr;
        
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
        animator.SetFloat("TimeToExpierd",clock.fillAmount);

        if(messageTimer <= 0){
            MessageExpierd();
        }
    }

    public void MessageGrabbed(){
        animator.SetTrigger("Grabbed");
    } 

    private void OnCollisionEnter2D(Collision2D other) {

        GameObject effect = Instantiate(dustRffect.gameObject,other.contacts[0].point,Quaternion.identity);
        
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        if(particle != null)
            particle.startColor = parent.MessageColor;
    }

}