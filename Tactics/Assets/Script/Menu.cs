using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string id;

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
