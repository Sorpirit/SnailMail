using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSpot : MonoBehaviour
{
    [SerializeField] private MessageTamplate message;
    [SerializeField] private ParticleSystem deliveredEffect;
    [SerializeField] private Color spotColor;

    private void Start() {
        deliveredEffect.startColor = spotColor;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        MessageController controller = other.GetComponent<MessageController>();

        if(controller != null && message.Equals(controller.Parent)){
            GameManager.instance.AddScores(controller.Parent);
            controller.OnSpot();
            deliveredEffect.transform.position = other.transform.position;
            deliveredEffect.Play();
        }
    }
}
