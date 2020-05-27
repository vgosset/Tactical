using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public GameObject uiChar;
    
    public Animator lifeAmountUpdate;
    public Animator actionAmountUpdate;
    public Animator moveAmountUpdate;
    
    public Animator amountUpdate;

    public Text n_pm;
    public Text n_pa;
    public Text n_lifes;

    public Transform p_actionLst;

    Text lifeUiChar;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        lifeUiChar = uiChar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {

    }
    
    public void UpdateStatsAmount(int id, int value, int amount, string ope)
    {
        switch(id)
        {
            case 0:
                lifeAmountUpdate.SetTrigger("amountUpdate");
                lifeAmountUpdate.transform.GetChild(0).GetComponent<Text>().text = ope + " " + amount.ToString();
            break;

            case 1:
                actionAmountUpdate.SetTrigger("amountUpdate");
                actionAmountUpdate.transform.GetChild(0).GetComponent<Text>().text = ope + " " + amount.ToString();
               
                n_pa.text = value.ToString();
            break;

            default:
                moveAmountUpdate.SetTrigger("amountUpdate");
                moveAmountUpdate.transform.GetChild(0).GetComponent<Text>().text = ope + " " + amount.ToString();
                
                n_pm.text = value.ToString();
            break;
        }
    }
    public void UpdateLifeOnPos(int value, string ope, Vector3 pos)
    {
        amountUpdate.transform.position = Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y, pos.z));
        amountUpdate.transform.GetChild(0).GetComponent<Text>().text = ope + " " + value.ToString();
        amountUpdate.SetTrigger("amountUpdate");

        int life = System.Convert.ToInt32(lifeUiChar.text);
        if (ope == "+")
            lifeUiChar.text = (life + value).ToString();
        else
            lifeUiChar.text = (life - value).ToString();
    }
    public void UpdatePanelId(Character current)
    {
        n_pa.text = current.c_datas.n_pa.ToString();
        n_pm.text = current.c_datas.n_pm.ToString();
        n_lifes.text = current.GetComponent<Lifes>().n_life.ToString();

        for (int i = 0; i < current.c_datas.l_action.Count; i++)
        {
            Actions tmp = current.c_datas.l_action[i];

            p_actionLst.transform.GetChild(i).transform.GetComponent<Action>().SetAction(tmp);
        }
    }
    public void ShowUiChar(int n_life, Transform pos)
    {
        if (!uiChar.activeSelf)
        {
            uiChar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(pos.position.x, pos.position.y + 1.5f, pos.position.z));
            uiChar.SetActive(true);
            lifeUiChar.text = n_life.ToString();
        }
    }
    public void HideUiChar()
    {
        uiChar.SetActive(false);
    }
}
