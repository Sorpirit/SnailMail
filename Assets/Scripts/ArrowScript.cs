using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem lightEffect;
    [SerializeField] private MessageTamplate targetTemplate;
    [SerializeField] private float balackEffectDutration;

    private float blackEffectTime;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.TryGetComponent<MessageController>(out MessageController controller))
        {

            if(controller.Parent == targetTemplate){
                PlayRewordingEffect();
            }else{
                PlayPunishMentEffect();
            }

        }

    }

    private void PlayRewordingEffect(){
        if(lightEffect.isPlaying) 
            return;

        animator.SetTrigger("Reword");
        lightEffect.Play();
    }

    private void PlayPunishMentEffect(){
        if(blackEffectTime + balackEffectDutration > Time.time)
            return;

        blackEffectTime = Time.time;
        animator.SetTrigger("Punish");
    }
}
