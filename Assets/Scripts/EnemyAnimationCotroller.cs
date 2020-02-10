using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCotroller : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemy;

    public void Attack(){
        enemy.Attack();
    }
}
