using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : TacticsActions
{
    public Tile c_tile;
    public CharacterMove m_characterMove;

    public bool t_passed = false;
    public bool t_current = false;

    public int t_currentId;

    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        ResetSkills();
        GetComponent<Lifes>().SetLife(c_datas.n_lifes);
    }

    void Update()
    {
        ActionsHandeler();
    }
    void ActionsHandeler()
    {
        if (t_current && ActionManager.Instance.c_actionOn )
        {
            if  (c_action && c_action.t_action == ActionType.LINE)
            {
                GetTileInLine(c_action.t_target.minRange, c_action.t_target.maxRange, c_action.t_target.ldv);
                CheckMouseFire();
            }
            if (c_action && c_action.t_action == ActionType.CLASSIC)
            {
                FindTileAll(c_action.t_target.minRange, c_action.t_target.maxRange, c_action.t_target.ldv);
                CheckMouseFire();
            }
        }
    }

    public void ResetSkills()
    {
        n_pa = c_datas.n_pa;
        n_pm = c_datas.n_pm;

        m_characterMove.move = n_pm;
    }
    public bool GetTurnStatue()
    {
        return t_passed;
    }

    public void SetTurnStatue(bool state)
    {
        t_passed = state;
    }

    public void ShowUiStats()
    {
        UiManager.Instance.ShowUiChar(GetComponent<Lifes>().n_life, transform);
    }
}
