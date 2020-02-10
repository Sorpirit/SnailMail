using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{


    [SerializeField] private MessageTamplate[] messages;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float throwForce;
    [SerializeField] private float spawnRandomRange;
    
    [SerializeField] private float SpawnRate;
    [SerializeField] private bool isSpawning = true;
    [SerializeField] private ParticleSystem spawnEffect;
    [SerializeField] private AudioSource messageSpawned;
 
    private float spawnTimer;

    private void Spawn(){
        int randIndex = Random.Range(0,messages.Length);
        GameObject mes = messages[randIndex].CreateMessage(spawnPoint);
        Rigidbody2D mesRb = mes.GetComponent<Rigidbody2D>();
        if(mesRb != null){
            mesRb.AddForce(((Vector2) spawnPoint.right + new Vector2(0,Random.Range(-spawnRandomRange,spawnRandomRange))).normalized * throwForce);
        }
        spawnEffect.startColor = messages[randIndex].MessageColor;
        messageSpawned.Play();
        spawnEffect.Play();
    }

    private void Update() {
        spawnTimer -= Time.deltaTime;

        if(isSpawning && spawnTimer <= 0){
            Spawn();
            spawnTimer = SpawnRate;
        }
    }
}
