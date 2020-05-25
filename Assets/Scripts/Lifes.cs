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
    public void GetHit(int value)
    {
        n_life -= value;

        if (n_life <= 0)
            Death();

        UiManager.Instance.UpdateLifeOnPos(value, "-", transform.position);
        // UiManager.Instance.UpdateStatsAmount(0, n_life, value, "-");
    }
    public void SetLife(int n)
    {
        n_life = n;
        ActionManager.Instance.UpdateCharActions();
    }
    void Death()
    {
        // isDead = true;
        // Destroy(this.gameObject);
        MainManager.Instance.EndGame();
    }
}
