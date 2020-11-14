using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public GameObject uiChar;
    
    [SerializeField] private List<CharInfoUI> charInfoLst;
    [SerializeField] private Animator actionUpdate_a;
    [SerializeField] private Animator moveUpdate_a;
    
    [SerializeField] private Animator Update_a;

    public Text n_pm;
    public Text n_pa;

    public Transform p_actionLst;

    private Text lifeUiChar;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        lifeUiChar = uiChar.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }
    public CharInfoUI GetCharInfo(int id)
    {
        return charInfoLst[id];
    }
    public void UpdateStatsAmount(int id, int value, int amount, string ope)
    {
        switch(id)
        {
            case 1:
                actionUpdate_a.SetTrigger("amountUpdate");
                actionUpdate_a.transform.GetChild(0).GetComponent<Text>().text = ope + " " + amount.ToString();
                
                n_pa.text = value.ToString();
            break;

            default:
                moveUpdate_a.SetTrigger("amountUpdate");
                moveUpdate_a.transform.GetChild(0).GetComponent<Text>().text = ope + " " + amount.ToString();
                
                n_pm.text = value.ToString();
            break;
        }
    }
    public void UpdateLifeOnPos(int value, string ope, Vector3 pos)
    {
        Update_a.transform.position = Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y, pos.z));
        Update_a.transform.GetChild(0).GetComponent<Text>().text = ope + " " + value.ToString();
        Update_a.SetTrigger("amountUpdate");

        int life = System.Convert.ToInt32(lifeUiChar.text);
        
        lifeUiChar.text = (life + value).ToString();
    }
    public void UpdatePanelId(Character current)
    {
        n_pa.text = current.c_datas.n_pa.ToString();
        n_pm.text = current.c_datas.n_pm.ToString();

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
