using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageGrab : MonoBehaviour
{
    [SerializeField] private float holdRadious;
    [SerializeField] private SpringJoint2D leftHand;
    [SerializeField] private DistanceJoint2D leftHandDist;
    [SerializeField] private Collider2D leftGrabArea;
    [SerializeField] private ContactFilter2D messageMask;
    [SerializeField] private PlayTestMode testMode;

    //Delete in Final version
    enum PlayTestMode{

        HoldMouse,
        PressMouse

    }

    private bool isLeftHandFree = true;
    private bool isRightHandFree = true;

    private void Grab(Rigidbody2D other,SpringJoint2D hand, DistanceJoint2D handDist)
    {

        hand.connectedBody = other;

    }

    private void Throw(SpringJoint2D hand,DistanceJoint2D handDist)
    {

        hand.connectedBody = hand.attachedRigidbody;
        handDist.connectedBody = handDist.attachedRigidbody; 

    }

    private GameObject TryToGrab(Collider2D grabArea){
        List<Collider2D> results = new List<Collider2D>();
        grabArea.OverlapCollider(messageMask,results);
        if(results.Count == 0)
            return null;

        GameObject closest = null;
        float distance = Vector2.Distance(transform.position,grabArea.bounds.min + grabArea.transform.position);
        foreach (Collider2D coll in results)
        {
            float tempDistance = Vector2.Distance(coll.transform.position,transform.position);
            if(tempDistance < distance){
                if( coll.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb) && (leftHand.connectedBody != otherRb))
                    closest = coll.gameObject;
            }
        }
        
        return closest;
    }

    //Refactor In Final version!
    private void UpdateLeftHandPress(){
        if(Input.GetButtonDown("Fire1")){
            if(isLeftHandFree){
            
            GameObject grabObject = TryToGrab(leftGrabArea);
            if(grabObject != null && grabObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb)){
                Grab(otherRb,leftHand,leftHandDist);
                isLeftHandFree = false;
            }
            
            }else{
                Throw(leftHand,leftHandDist);
                isLeftHandFree = true;
            }
        }
    }
    /*
    private void UpdateRightHandPress(){
        if(Input.GetButtonDown("Fire2")){

            if(isRightHandFree){
            
            GameObject grabObject = TryToGrab(rightGrabArea);
            if(grabObject != null && grabObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb)){
                Grab(otherRb,rightHand,rightHandDist);
                isRightHandFree = false;
            }
            
            }else{
                Throw(rightHand,rightHandDist);
                isRightHandFree = true;
            }


        }
    }
    */
    //Refactor In Final version!
    private void UpdateLeftHandHold(){
        if(Input.GetButtonDown("Fire1") && isLeftHandFree){
            GameObject grabObject = TryToGrab(leftGrabArea);
            if(grabObject != null && grabObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb)){
                Grab(otherRb,leftHand,leftHandDist);
                isLeftHandFree = false;
            }
        }

        if(Input.GetButtonUp("Fire1") && !isLeftHandFree){
            Throw(leftHand,leftHandDist);
            isLeftHandFree = true;
        }
    }
    /*
    private void UpdateRightHandHold(){
        if(Input.GetButtonDown("Fire2") && isRightHandFree){
            GameObject grabObject = TryToGrab(rightGrabArea);
            if(grabObject != null && grabObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRb)){
                Grab(otherRb,rightHand,rightHandDist);
                isRightHandFree = false;
            }
        }

        if(Input.GetButtonUp("Fire2") && !isRightHandFree){
            Throw(rightHand,rightHandDist);
            isRightHandFree = true;
        }
    }
    */
    //Refactor In Final version!
    // private void CombinedModeUpdate(){
    //     if(Input.GetButtonDown("Fire1")){

    //     }

    //     if(Input.GetButtonDown("Fire1")){

    //     }

    // }
    private void AplyHoldRadious(){

        if(!isLeftHandFree && Vector2.Distance(leftHand.transform.position,leftHand.connectedBody.position) > holdRadious){
            Throw(leftHand,leftHandDist);
            isLeftHandFree = true;
        }
        // if(!isRightHandFree && Vector2.Distance(rightHand.transform.position,rightHand.connectedBody.position) > holdRadious){
        //     Throw(leftHand,rightHandDist);
        //     isLeftHandFree = true;
        // }
    }
    private void NullChek(){
        if(!isLeftHandFree && leftHand.connectedBody == null){
            Throw(leftHand,leftHandDist);
            isLeftHandFree = true;
        }

        // if(!isRightHandFree && rightHand.connectedBody == null){
        //     Throw(rightHand,rightHandDist);
        //     isRightHandFree = true;
        // }
    }


    private void Update() {
    
        //Refactor In Final
        switch(testMode){
            case PlayTestMode.HoldMouse:
                UpdateLeftHandHold();
                //UpdateRightHandHold();
            break;
            case PlayTestMode.PressMouse:
                UpdateLeftHandPress();
                //UpdateRightHandPress();
            break;
        }

        NullChek();
        AplyHoldRadious();

        if(Input.GetKeyDown(KeyCode.T))
            ChnegePlayTestMode();

    }
        

    private void ChnegePlayTestMode(){
        testMode = testMode == PlayTestMode.HoldMouse ? PlayTestMode.PressMouse : PlayTestMode.HoldMouse;
    }

        
}
