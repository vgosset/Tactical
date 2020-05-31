using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    public int n_life;

    public bool isDead = false;

    void Start()
    {

    }

    void Update()
    {

    }
    public void GetHit(int value, bool damage)
    {
        string ope;

        if (damage)
            ope = "-";
        else
            ope = "+";

        n_life += value;

        if (value != 0)
            UiManager.Instance.UpdateLifeOnPos(value, ope, transform.position);
        
        if (n_life <= 0)
            Death();
    }
    public void SetLife(int n)
    {
        n_life = n;
        ActionManager.Instance.UpdateCharActions();
    }
    public void PushTo(Vector3 dest)
    {
        transform.position = new Vector3(dest.x, 1.5f, dest.z);

        GetComponent<CharacterMove>().GetCurrentTile();
    }
    void Death()
    {
        // isDead = true;
        // Destroy(this.gameObject);
        MainManager.Instance.EndGame();
    }
}
