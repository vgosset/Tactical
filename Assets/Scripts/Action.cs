using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
    Actions c_action;

    public GameObject hide;

    public Image icon;

    public Text pa;
    public Text pm;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetAction(Actions action)
    {
        c_action = action;

        icon.sprite = action.icon;

        pm.text = action.pm_cost.ToString();
        pa.text = action.pa_cost.ToString();
    }

    public void SendAction()
    {
        ActionManager.Instance.ActionActivated(c_action);
    }

    public void Toggle()
    {
        hide.SetActive(true);
    }
}
