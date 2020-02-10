using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Image roundTimerUI;
    [SerializeField] private float timeForRound;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private ScoresTableUI scoresUI;
    
    [Space]
    [SerializeField] private float punishmentPointsToSpawnGhost;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float ghostSpawnRange;

    [Space]
    [SerializeField] private NextLevelCondition levelCondition;
    [SerializeField] private TMP_Text reqScoresText;

    private Dictionary <MessageTamplate,int> deliveredMessages;
    private Dictionary <MessageTamplate,int> expierdMessages;

    public int Total{get => totalScores;}

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
        finishScreen.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.Play(SoundEffect.RoundStarts);
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
        if(!isPlaying)
           return; 

        SoundManager.instance.Play(SoundEffect.RoundEnds);
        finishScreen.SetActive(true);
        PrepareFinishScreen();
        Time.timeScale = 0f;
        isPlaying = false;
    }
    public void PrepareFinishScreen(){
        foreach (MessageTamplate mess in deliveredMessages.Keys)
        {
            scoresUI.AddRaw(mess.TypeName,"You have delivered " + deliveredMessages[mess] + "(sp for one " +mess.ScoresCost+"sp)");
        }
        scoresUI.AddRaw("Your Total score :", totalScores + "sp");
        
        if(reqScoresText != null && levelCondition != null){
            reqScoresText.text = "Next Level " +  totalScores + " / " + levelCondition.ReqScore;
            if(totalScores >= levelCondition.ReqScore){
                reqScoresText.color = Color.green;
            }else{
                reqScoresText.color = Color.red;
            }
        }

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
        SoundManager.instance.Play(SoundEffect.ChostSpawn);
    }
    private bool isAbleToSpawnGhost(){
        return (totalPanishmentPoints - prevSpawPanishmentPoints) >= punishmentPointsToSpawnGhost;
    }

    private void Update() {
        roundTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(0);
        }
        
        roundTimerUI.fillAmount = roundTimer / timeForRound;
        

        if(isPlaying && roundTimer <= 0){
            EndGame();
        }
    }

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }



}
