using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Image roundTimerUI;
    [SerializeField] private float timeForRound;
    [SerializeField] private GameObject finishScreen;
    
    [Space]
    [SerializeField] private float punishmentPointsToSpawnGhost;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float ghostSpawnRange;

    private Dictionary <MessageTamplate,int> deliveredMessages;
    private Dictionary <MessageTamplate,int> expierdMessages;

    private int totalScores;
    private int totalPanishmentPoints;
    private int prevSpawPanishmentPoints;

    private bool isPlaying = true;
    private float roundTimer;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
            return;
        }

        deliveredMessages = new Dictionary<MessageTamplate, int>();
        expierdMessages = new Dictionary<MessageTamplate, int>();
    }

    private void Start() {
        roundTimer = timeForRound;
    }

    public void AddScores(MessageTamplate tamplate){
        if(!deliveredMessages.ContainsKey(tamplate))
            deliveredMessages.Add(tamplate,0);

        deliveredMessages[tamplate] ++;
        totalScores += tamplate.ScoresCost;
    }
    public void MessageExpierd(MessageTamplate tamplate){
        if(!expierdMessages.ContainsKey(tamplate))
            expierdMessages.Add(tamplate,0);

        expierdMessages[tamplate] ++;
        totalPanishmentPoints += tamplate.PunishmentPointsCost;

        if(isAbleToSpawnGhost()){
            prevSpawPanishmentPoints = totalPanishmentPoints;
            SpawnGhost();
        }

    }
    public void EndGame(){
        finishScreen.SetActive(true);
    }

    private void SpawnGhost(){
        Vector2 pos = Random.insideUnitCircle;
        pos.Normalize();
        Debug.Log(pos);

        GameObject enemy = Instantiate(ghostPrefab,(Vector2) player.position + pos * ghostSpawnRange,Quaternion.identity);

        EnemyFollow follow = enemy.GetComponent<EnemyFollow>();
        if(follow != null){
            follow.Target = player;
        }
    }
    private bool isAbleToSpawnGhost(){
        return (totalPanishmentPoints - prevSpawPanishmentPoints) >= punishmentPointsToSpawnGhost;
    }

    private void Update() {
        roundTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        
        roundTimerUI.fillAmount = roundTimer / timeForRound;
        

        if(isPlaying && roundTimer <= 0){
            EndGame();
        }
    }


}
