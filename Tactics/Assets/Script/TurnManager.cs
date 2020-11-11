using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum TurnState
    {
        IN_TURN,
        IN_TRANSITION
    }

    TurnState t_state;

    public static TurnManager Instance;

    public TurnPanel t_panel;

    public List<Character> c_lst;

    public float n_turn;
    public float t_current = 0;

    Stack<Character> c_stack = new Stack<Character>();

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        t_state = TurnState.IN_TURN;
    }
    public void InitCharList(List<Character> lst)
    {
        for (int i = 0; i < lst.Count; i++)
            c_lst.Add(lst[i]);

        InitTurn();
    }
    private void InitTurn()
    {
        c_stack.Clear();
        for (int i = 0; i < c_lst.Count; i++)
        {
            c_stack.Push(c_lst[i]);

            c_stack.Peek().t_passed = false;
            c_stack.Peek().t_currentId = i;
        }

        SetCurrentCharacter();

        ResetCharacterSkills();
    }
    public bool IsInTurn()
    {
        if (t_state == TurnState.IN_TURN)
            return true;
        return false;
    }

    public void ResetCharacterSkills()
    {
        for (int i = 0; i < c_lst.Count; i++)
            c_lst[i].ResetSkills();
    }
    public void NextTurn()
    {
        t_current ++;
        if (n_turn == t_current)
            Debug.Log("GameFinished");
        else
            InitTurn();
    }

    public Character CurrentCharID()
    {
        return c_stack.Peek();
    }

    public void NextCharTurn()
    {
        t_panel.NextTurn();

        c_stack.Peek().SetTurn(false);
        c_stack.Pop();

        if (c_stack.Count == 0)
            NextTurn();
        else
            SetCurrentCharacter();
        ActionManager.Instance.NextTurn();
    }
    
    public void SetCurrentCharacter()
    {
        c_stack.Peek().SetTurn(true);
    }
}
