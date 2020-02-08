using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoresTableUI : MonoBehaviour
{
    [SerializeField] private GameObject raw;
    [SerializeField] private Vector2 halfSize;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 rawOfset;

    private RectTransform myRect;
    private Vector2 virtualCursor;

    private void Start() {
        virtualCursor = startPos;
        myRect = GetComponent<RectTransform>();
    }
    public void AddRaw(string key,string val){
        GameObject newRaw = Instantiate(raw);
        newRaw.transform.SetParent(transform);
        Debug.Log("Add raw - " + newRaw.name );

        RectTransform rawRect = newRaw.GetComponent<RectTransform>();
        rawRect.anchorMax = new Vector2(.5f,1);
        rawRect.anchorMin = new Vector2(.5f,1);
        rawRect.pivot = new Vector2(.5f,1);
        rawRect.anchoredPosition = virtualCursor;
        //rawRect.anchoredPosition = virtualCursor;
        //rawRect.anchoredPosition
        virtualCursor -= (halfSize + rawOfset);
        
        RawControllerFinishScreen rawController = rawRect.GetComponent<RawControllerFinishScreen>();

        if(rawController != null){

            rawController.SetKey(key);
            rawController.SetValue(val);

        }
    }

    

  
}
