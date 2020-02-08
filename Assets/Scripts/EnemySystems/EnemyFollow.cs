using UnityEngine;

public class EnemyFollow : MonoBehaviour {
    
    [SerializeField] private Transform follow;

    public bool isRotating{get => rotating; set => rotating = value;}
    public Transform Target{get => follow;set => follow = value;}
    private bool rotating = true;
    private Vector2 direction;

    private void RotateTowardsFollow(){
        transform.right = direction.normalized;
    }
    private void CalculateDir(){
        direction = follow.position - transform.position;
        direction.Normalize();
    }

    private void Update() {
        if(follow != null) CalculateDir();
        if(rotating) RotateTowardsFollow();
    }


}