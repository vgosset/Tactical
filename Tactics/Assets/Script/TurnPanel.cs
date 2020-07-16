using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    int id = 1;
    void Start()
    {
        Init();
    }

    void Update()
    {

    }
    public void NextTurn()
    {
        Transform main = transform.GetChild(transform.childCount - 1);

        main.SetSiblingIndex(0);
        main.localScale = new Vector3(1, 1, 1);

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).transform.localScale = new Vector3(0.8f, 0.8f,0.8f);
        }
        main = transform.GetChild(transform.childCount - 1);
        main.localScale = new Vector3(1, 1, 1);
    }
    public void Init()
    {
        List<GameObject> l_char = MainManager.Instance.l_char_init;

        for (int i = 0; i < l_char.Count; i++)
        {
            // Debug.Log(l_char[i].GetComponent<Character>().c_datas.uiTexture);
            // transform.GetChild(i).GetComponent<RawImage>().texture = l_char[i].GetComponent<Character>().c_datas.uiTexture;
        }
    }
}
