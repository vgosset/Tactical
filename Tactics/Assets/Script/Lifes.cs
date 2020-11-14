using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    public int n_life;

    public bool isDead = false;

    private CharInfoUI charInfo;

    public void GetHit(int value, bool damage)
    {
        string ope;

        if (damage)
            ope = "-";
        else
            ope = "+";

        n_life += value;

        if (value != 0)
        {
            UiManager.Instance.UpdateLifeOnPos(value, ope, transform.position);
        }
        if (n_life <= 0)
            Death();

        charInfo.UpdateLife(n_life);
    }
    public void SetLifeAndIcon(int n, int id, Texture texture)
    {
        n_life = n;

        charInfo = UiManager.Instance.GetCharInfo(id);
        charInfo.Setup(n_life, texture);
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
