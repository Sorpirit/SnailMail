using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IDieAble
{

    [SerializeField]private float deathDelay = .05f;
    private bool isDead = false;
    public void Die()
    {
        Debug.Log("Boom!");
        if(!isDead) StartCoroutine("Death");
        isDead = true;
        
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(deathDelay);
        SoundManager.instance.Play(SoundEffect.PlayerDie);
        yield return new WaitForSeconds(deathDelay);
        GameManager.instance.EndGame(); 
        gameObject.SetActive(false);
    }
}