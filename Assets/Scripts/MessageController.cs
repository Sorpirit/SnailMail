using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {
    

    [SerializeField] private TMP_Text quoteField;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer messageRenderer;
    [SerializeField] private ParticleSystem dustRffect;

    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip[] colliderClips;
    public MessageTamplate Parent{get => parent;set => parent = value;}
    public Image clock;

    [SerializeField] private MessageTamplate parent;
    private float messageTimerPireod;
    private float messageTimer;
    private bool isTimerOn = false;

    public void SetQuote(string quote){
        quoteField.text = quote;
    }
    public void StartTimer(float time){
        messageTimer = time;
        messageTimerPireod = time;
        isTimerOn = true;
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
        if(isTimerOn)
            TimerUpdate();
    }


    private void TimerUpdate(){
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

        if(other.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
            return;

        GameObject effect = Instantiate(dustRffect.gameObject,other.contacts[0].point,Quaternion.identity);
        
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        if(particle != null)
            particle.startColor = parent.MessageColor;

        if(!audio.isPlaying){
            int randIndex = Random.Range(0,colliderClips.Length);
            audio.clip = colliderClips[randIndex];
            audio.Play();
        }
    }

}