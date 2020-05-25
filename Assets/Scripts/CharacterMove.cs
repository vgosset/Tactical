﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : TacticsMove
{
    void Start()
    {
      Init();
    }

    void Update()
    {
      if (moving)
      {
        Move();
      }
    }
    void LateUpdate()
    {
      if (!moving && character.t_current && ActionManager.Instance.GetMovementState())
      {
          FindSelectableTiles();
          CheckMouseTarget();
      }
    }

    void CheckMouseTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
          if (hit.collider.tag == "Tile")
          {
            Tile t = hit.collider.GetComponent<Tile>();

            if (t.selectableMove)
            {
              if (Input.GetMouseButtonUp(0))
              {
                MoveToTile(t);
              }
              if (!t.onCurrentPath || !t.mouseOnIt)
              {
                  RemoveLastPath();
                  t.mouseOnIt = true;
                  ShowPath(t);
              }
            }
            else
                RemoveLastPath();
          }
          else
              RemoveLastPath();
        }
        else
              RemoveLastPath();
    }
}
