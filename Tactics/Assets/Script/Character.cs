using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : TacticsActions
{
    [SerializeField] Texture iconTexture;
    public Tile c_tile;
    public CharacterMove m_characterMove;

    public Animator anim;
    public bool t_passed = false;

    public int t_currentId;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        t_current = false;
    }
    private void Start()
    {
        ResetSkills();
    }
    private void Update()
    {
        if (t_current && c_action && !actionPlaying)
        {
            CheckMouseFire();
        }
    }
    public void Setup(int id)
    {
        GetComponent<Lifes>().SetLifeAndIcon(c_datas.n_lifes, id, iconTexture);
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
    public void SetTurn(bool state)
    {
        t_current = state;
        // if (state)
        // {
        //     torusMesh.material = turnOn;
        // }
        // else 
        // {
        //     torusMesh.material = turnOff;
        //     c_tile.current = false;
        // }
    }
    public void TriggerAnim(string s)
    {
        anim.SetTrigger(s);
    }
    public void FireInTargetDir(string s, Tile t)
    {
        Vector3 target = t.transform.position;
        target.y += m_characterMove.halfHeight + t.GetComponent<Collider>().bounds.extents.y;
        
        RotateTo(target);
        TriggerAnim(s);
    }
    private void RotateTo(Vector3 pos)
    {
        m_characterMove.CalcHeading(pos);
        transform.forward = m_characterMove.heading;
    }
}
