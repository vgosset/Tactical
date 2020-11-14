using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : TacticsMove
{
    [SerializeField] private LayerMask moveLayer;
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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveLayer))
        {
          if (hit.collider.tag == "Tile")
          {
            Tile t = hit.collider.GetComponent<Tile>();

            if (t.selectableMove && t != currentTile)
            {
              if (Input.GetMouseButtonUp(0))
              {
                moving = true;
                MoveToTile(t);
              }
              if (!t.onCurrentPath || !t.mouseOnIt)
              {
                  TileManager.Instance.RemoveLastPath();
                  t.mouseOnIt = true;
                  ShowPath(t);
              }
            }
            else
                TileManager.Instance.RemoveLastPath();
          }
          else
              TileManager.Instance.RemoveLastPath();
        }
        else
              TileManager.Instance.RemoveLastPath();
    }
}
