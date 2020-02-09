using UnityEngine;

[CreateAssetMenu(fileName = "NewMessageType", menuName = "SnailMail/Create New Message Type", order = 0)]
public class MessageTamplate : ScriptableObject {
    
    [SerializeField] private string typeName;
    [SerializeField] private MessageController message;
    [SerializeField] private string[] quotes;
    [SerializeField] private float TimePeriod;
    [SerializeField] private float spawnChance;
    [SerializeField] private int scoresCost;
    [SerializeField] private int punishmentPointsCost;
    [SerializeField] private Sprite messageSprite;
    [SerializeField] private Color messageColor;

    public int ScoresCost{get => scoresCost;}
    public int PunishmentPointsCost{get => punishmentPointsCost;}
    public string TypeName{get => typeName;}
    public Color MessageColor{get => messageColor;}

    public GameObject CreateMessage(Transform point,bool makeParent = false){
        MessageController controller = Instantiate(message,point.position,point.rotation) as MessageController;
        controller.SetQuote(quotes[Random.Range(0,quotes.Length)]);
        controller.SetSprite(messageSprite);
        controller.StartTimer(TimePeriod);
        controller.Parent = this;
        
        return controller.gameObject;
    }


}