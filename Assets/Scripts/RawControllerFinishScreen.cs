using UnityEngine;
using TMPro;

public class RawControllerFinishScreen : MonoBehaviour {
    
    [SerializeField] private TMP_Text key;
    [SerializeField] private TMP_Text value;

    public void SetKey(string key){
        this.key.text = key;
    }
    public void SetValue(string value){
        this.value.text = value;
    }

}