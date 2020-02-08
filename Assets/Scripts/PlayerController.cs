using UnityEngine;

public class PlayerController : MonoBehaviour, IDieAble
{
    public void Die()
    {
        Debug.Log("Boom!");
        gameObject.SetActive(false);
        GameManager.instance.EndGame();
    }
}