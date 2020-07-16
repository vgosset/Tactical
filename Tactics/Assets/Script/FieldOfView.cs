using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    public GameObject[] tiles;
    Tile currentTile;

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public LayerMask targetMask;
    public LayerMask ObstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }
    public void FindClassicSelectableTile(Actions c_action)
    {
        SetTileDistance(c_action.t_target.maxRange);
        visibleTargets.Clear();

        viewRadius = c_action.t_target.maxRange;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (!c_action.t_target.ldv)
                visibleTargets.Add(target);
            else if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast (transform.position, dirToTarget, dstTarget, ObstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
        foreach (Transform visibleTarget in visibleTargets)
        {
            Tile t = visibleTarget.GetComponent<Tile>();
            if (t.distance <= c_action.t_target.maxRange && t.distance > c_action.t_target.minRange && t.walkable)
                t.selectableAction = true;
            if (c_action.t_target.self)
                currentTile.selectableAction = true;
        }
    }
    
    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
            tile = hit.collider.GetComponent<Tile>();
        return tile;
    }
    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
    }
    
    public void SetTileDistance(int maxRange)
    {
      ComputeAdjLst();
      GetCurrentTile();

      Queue<Tile> process = new Queue<Tile>();

      process.Enqueue(currentTile);
      currentTile.visited = true;
      
      while (process.Count > 0)
      {
          Tile t = process.Dequeue();

          if (t.distance < maxRange)
          {
              foreach(Tile tile in t.adjTileLst)
              {
                  if (!tile.visited)
                  {
                    tile.parent = t;
                    tile.visited = true;
                    tile.distance = 1 + t.distance;
                    process.Enqueue(tile);
                  }
              }
          }
      }
    }

    public void ComputeAdjLst()
    {
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(2, true, false);
        }
    }
}
