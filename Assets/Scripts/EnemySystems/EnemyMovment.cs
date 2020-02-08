using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] private float acc;

    public bool IsMoving{get => isMoving; set => isMoving = value; }
    private bool isMoving = true;
    private float currentSpeed = 0;

    private void MoveEnemy(){
        transform.position += transform.right * currentSpeed * Time.deltaTime;
        currentSpeed += acc * Time.deltaTime;
    }

    private void FixedUpdate() {
        if(isMoving) MoveEnemy();
    }
}
