using UnityEngine;

[CreateAssetMenu(fileName = "MessageTamplate", menuName = "SnailMail/Create MessageTamplate", order = 0)]
public class MessageTamplate : ScriptableObject {
    
    [SerializeField] private MessageController message;
    [SerializeField] private string[] quotes;
    [SerializeField] private float TimePeriod;
    [SerializeField] private float spawnChance;
    [SerializeField] private int scoresCost;

    public int ScoresCost{get => scoresCost;}

    public GameObject CreateMessage(Transform point,bool makeParent = false){
        MessageController controller = Instantiate(message,point.position,point.rotation) as MessageController;
        controller.SetQuote(quotes[Random.Range(0,quotes.Length)]);
        controller.StartTimer(TimePeriod);
        controller.Parent = this;
        return controller.gameObject;
    }


}