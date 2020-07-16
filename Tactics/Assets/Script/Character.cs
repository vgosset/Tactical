using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : TacticsActions
{
    public Tile c_tile;
    public CharacterMove m_characterMove;
    public FieldOfView fovHandeler;

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
        if (t_current && c_action)
            CheckMouseFire();
    }
    public void ActionsHandeler()
    {
        if (t_current)
        {
            if  (c_action && c_action.t_action == ActionType.LINE)
                GetTileInLine();
            if (c_action && c_action.t_action == ActionType.CLASSIC)
                fovHandeler.FindClassicSelectableTile(c_action);
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
