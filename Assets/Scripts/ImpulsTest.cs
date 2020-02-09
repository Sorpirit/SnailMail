using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ImpulsTest : MonoBehaviour
{
    
    [SerializeField] CinemachineImpulseSource impulseSource;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)){
            impulseSource.GenerateImpulse();
        }
    }
}
