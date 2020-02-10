using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour {
    
    private EnemyFollow follow;
    private EnemyMovment movment;

    [SerializeField] private float AttackRange;
    [SerializeField] private Collider2D AttackZone;
    [SerializeField] private GameObject AttackZoneTip;
    [SerializeField] private GameObject AttackZoneSprite;
    [SerializeField] private ContactFilter2D targetsFilter;
    [SerializeField] private Animator animator;

    private bool isAttacking;

    public void Attack(){

        SoundManager.instance.Play(SoundEffect.GhostAttack);
        CastHurtBox();

        if(follow != null) follow.isRotating = true;
        if(follow != null) movment.IsMoving = true;
        isAttacking = false;
    }
    private void CastHurtBox(){
        List<Collider2D> collider2Ds = new List<Collider2D>();
        AttackZone.OverlapCollider(targetsFilter,collider2Ds);
        if(collider2Ds.Count>0){
            foreach (Collider2D col in collider2Ds)
            {
                IDieAble die = col.GetComponent<IDieAble>();
                if(die != null){
                    die.Die();
                }
            }
        }
    }

    private bool ChekIfInRange(){
        if(follow == null) return false;

        return Vector2.Distance(transform.position,follow.Target.position) <= AttackRange;
    }

    private void Awake() {
        follow = GetComponent<EnemyFollow>();
        movment = GetComponent<EnemyMovment>();
    }

    private void Update() {
        if(!isAttacking && follow.Target != null && ChekIfInRange()){
            animator.SetTrigger("Attack");
            isAttacking = true;
            if(follow != null) follow.isRotating = false;
            if(follow != null) movment.IsMoving = false;
            //StartCoroutine("Attack");
        }
    }

}