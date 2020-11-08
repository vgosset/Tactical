using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    public bool moveSelected = false;
    public bool fire = false;
    public bool c_actionOn = false;
    public bool c_MovementOn = true;

    public Character c_char;


    List<Character> c_lst = new List<Character>();

    Actions c_action;


    private void Awake()
    {
        Instance = this;
    }
    public void InitCharList(List<GameObject> lst)
    {
        for (int i = 0; i < lst.Count; i++)
            c_lst.Add(lst[i].transform.GetComponent<Character>());
        UpdateCharActions();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            CancelAllActions();
        }
    }
    public void NextTurn()
    {
        CancelAllActions();
        UpdateCharActions();
    }
    public void UpdateCharActions()
    {
        c_char = TurnManager.Instance.CurrentCharID();

        UiManager.Instance.UpdatePanelId(c_char);
        c_MovementOn = true;
    }
    public void CancelAllActions()
    {
        c_char.c_action = null;
        c_MovementOn = true;
        TileManager.Instance.UnSelectAllTiles();
    }
    public bool GetMovementState()
    {
        return c_MovementOn;
    }
    public void ActionActivated(Actions action)
    {
        c_MovementOn = false;

        TileManager.Instance.UnSelectAllTiles();

        c_action = action;
        
        if (c_char.n_pa >= c_action.pa_cost && c_char.n_pm >= c_action.pm_cost)
        {
            c_actionOn = true;
            SendAction();
            if (!c_char.m_characterMove.moving)
                c_char.ActionsHandeler();
        }
    }
    void SendAction()
    {
        c_char.c_action = c_action;
    }
    public Actions GetCurrentAction()
    {
        return c_action;
    }

    public bool GetSelectedAction()
    {
        return moveSelected;
    }
}
