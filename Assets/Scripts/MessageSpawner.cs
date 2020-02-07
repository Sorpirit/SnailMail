using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{


    [SerializeField] private MessageTamplate[] messages;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float throwForce;
    [SerializeField] private float spawnRandomRange;
    
    

    private void Spawn(){
        GameObject mes = messages[Random.Range(0,messages.Length)].CreateMessage(spawnPoint);
        Rigidbody2D mesRb = mes.GetComponent<Rigidbody2D>();
        if(mesRb != null){
            mesRb.AddForce(((Vector2) spawnPoint.right + new Vector2(0,Random.Range(-spawnRandomRange,spawnRandomRange))).normalized * throwForce);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) Spawn();
    }
}
