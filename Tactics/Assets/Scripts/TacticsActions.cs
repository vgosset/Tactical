﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsActions : MonoBehaviour
{
    public int n_pa;
    public int n_pm;

    Tile tmp_tileTarget = new Tile();

    public List<Tile> selectableTiles = new List<Tile>();

    public CharacterData c_datas;
    public Character character;
    public Actions c_action;

    public GameObject[] tiles;

    Tile currentTile;
    
    void Start()
    {
    }

    void Update()
    {

    }

    public bool ActionIsPossbile()
    {
        if (c_action)
        {
            if (n_pa >= c_action.pa_cost && n_pm >= c_action.pm_cost)
            return true;
        }
        return false;
    }

    public void CheckMouseFire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Tile")
            {
                Tile t = hit.collider.GetComponent<Tile>();
                if (t.selectableAction)
                {
                    if (Input.GetMouseButtonUp(0) && ActionIsPossbile())
                        Fire(t, 1);
                    if (!t.target)
                    {
                        t.target = true;

                        if (c_action.t_impact == ImpactMovement.PUSH)
                            PredictionPush(t);
                        tmp_tileTarget.target = false;
                        tmp_tileTarget = t;
                    }
                }
                else
                {
                    tmp_tileTarget.target = false;
                    tmp_tileTarget = new Tile();
                }
            }
            if (hit.collider.tag == "Character")
            {
                Tile t = hit.transform.GetComponent<Character>().c_tile.transform.GetComponent<Tile>();
                if (t.selectableAction)
                {
                    if (Input.GetMouseButtonUp(0) && ActionIsPossbile())
                        Fire(t, 1);
                }
            }
        }
    }

    public void GetTileInLine()
    {
        selectableTiles.Clear();

        FindLine(Vector3.forward);
        FindLine(-Vector3.forward);
        FindLine(Vector3.right);
        FindLine(-Vector3.right);

        if (c_action.t_target.self)
            GetCurrentTile().selectableAction = true;
    }

    public void FindLine(Vector3 direction)
    {
        if (c_action.t_target.ldv && CheckObstacleBeforeMinRange(direction) || !c_action.t_target.ldv)
        {
            RaycastHit hit;
            Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
            List<Tile> tmpLine = new List<Tile>();

            int distance =  c_action.t_target.minRange;

            while (distance <  c_action.t_target.maxRange)
            {
                if (Physics.Raycast(pos + direction * distance, direction, out hit, 1))
                {
                    Tile t = hit.transform.GetComponent<Tile>();

                    if (c_action.t_target.ldv && t.ldvBlock)
                    {
                        Debug.Log("break At : " + distance + " Of " + direction);
                        break;
                    }
                    else if (!t.ldvBlock)
                    {
                        t.selectableAction = true;
                        tmpLine.Add(t);
                    }
                }
                distance++;
            }
        }
    }

    bool CheckObstacleBeforeMinRange(Vector3 direction)
    {
        RaycastHit hit;

        Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);

        int distance = 0;

        while (distance < c_action.t_target.minRange)
        {
            if (Physics.Raycast(pos + direction * distance, direction, out hit, 1))
            {
                Tile t = hit.transform.GetComponent<Tile>();
                if (t.ldvBlock)
                {
                    Debug.Log("break At : " + distance + " Of " + direction);
                    return false;
                }
            }
            distance++;
        }
        return true;
    }

    private void Fire(Tile t, int damage)
    {
        n_pa -= c_action.pa_cost;
        n_pm -= c_action.pm_cost;

        UiManager.Instance.UpdateStatsAmount(1, n_pa, c_action.pa_cost, "-");
        UiManager.Instance.UpdateStatsAmount(2, n_pm, c_action.pm_cost, "-");

        character.m_characterMove.move -= c_action.pm_cost;

        if (t.c_inTile != null)
        {
            Lifes c_life = t.c_inTile.GetComponent<Lifes>();

            if (c_action.damage > 0)
                c_life.GetHit(-c_action.damage, true);
            if (c_action.heal > 0)
                c_life.GetHit(c_action.heal, false);

            GameObject tmp = Instantiate(c_action.projectile, t.c_inTile.transform.position, c_action.projectile.transform.rotation);

            MainManager.Instance.DestroyItem(tmp, 3);
        }
        if (!ActionIsPossbile())
            ActionManager.Instance.CancelAllActions();
    }

    private void PredictionPush(Tile t)
    {
        Tile tile;

        float c_z = transform.position.z;
        float c_x = transform.position.x;
        float t_x = t.transform.position.x;
        float t_z = t.transform.position.x;

        Vector3 off = transform.position - t.transform.position;
        Vector3 dir = Vector3.zero;

        if (off.x >= off.z)
        {
            if (c_x > t_x)
                dir = Vector3.forward;
            else if  (c_x < t_x)
                dir = -Vector3.forward;  
        }
        
        else if (off.x <= off.z)
        {
            if (c_z > t_z)
                dir = Vector3.right; 
            else if  (c_z < t_z)          
                dir = -Vector3.right; 
        }
        GetNextTile(dir, t);
    }

    private void GetNextTile(Vector3 direction, Tile t)
    {
      Vector3 halfExtants = new Vector3(0.25f, (1 + 2) / 2.0f, 0.25f);
      Collider[] colliders = Physics.OverlapBox(t.transform.position + direction, halfExtants);

      foreach (Collider item in colliders)
      {
        Tile tile = item.GetComponent<Tile>();

        if (tile)
        {
            tile.movementDetection = true;
            Debug.Log(direction);
        }
      }
      if (colliders.Length == 0)
            ActionManager.Instance.UnSelectAllTiles();
    }

    public Tile GetCurrentTile()
    {
        return GetTargetTile(gameObject);
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
            tile = hit.collider.GetComponent<Tile>();
        return tile;
    }
}
