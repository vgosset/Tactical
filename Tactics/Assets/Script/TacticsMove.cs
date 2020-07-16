using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public Character character;
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    public Tile currentTile;

    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 3;
    public bool moving = false;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    protected void Init()
    {
      tiles = GameObject.FindGameObjectsWithTag("Tile");

      halfHeight = GetComponent<Collider>().bounds.extents.y;
    }
    void Update()
    {
    }
    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
        currentTile.c_inTile = character;
        character.c_tile = currentTile;
    }
    public Tile GetTargetTile(GameObject target)
    {
      RaycastHit hit;
      Tile tile = null;
      if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
          tile = hit.collider.GetComponent<Tile>();
      return tile;
    }
    public void ComputeAdjLst()
    {
      foreach (GameObject tile in tiles)
      {
        Tile t = tile.GetComponent<Tile>();
        t.FindNeighbors(jumpHeight, false, true);
      }
    }
    public void RemoveLastPath()
    {
      foreach (GameObject tile in tiles)
      {
        tile.GetComponent<Tile>().mouseOnIt = false;
        tile.GetComponent<Tile>().onCurrentPath = false;
      }
    }
    public void FindSelectableTiles()
    {
      ComputeAdjLst();
      GetCurrentTile();

      Queue<Tile> process = new Queue<Tile>();

      process.Enqueue(currentTile);
      currentTile.visited = true;
      while (process.Count > 0)
      {
          Tile t = process.Dequeue();

          selectableTiles.Add(t);
          t.selectableMove = true;

          if (t.distance < move)
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
    public void MoveToTile(Tile tile)
    {
        // foreach (GameObject go in tiles)   //TMP default color after move begin
        //     go.transform.GetComponent<Tile>().DefaultColorOnly(true);
        GeneratePath(tile);
        move -= path.Count - 1;
        character.n_pm = move;

        UiManager.Instance.UpdateStatsAmount(2, move, path.Count - 1, "-");
    }
    public void GeneratePath(Tile tile)
    {
      path.Clear();
      tile.target = true;

      moving = true;

      Tile next = tile;

      while(next != null)
      {
        path.Push(next);
        next = next.parent;
      }
    }
    public void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalcHeading(target);
                SetHorizotalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectedTiles();
            moving = false;
            character.ActionsHandeler();
        }
    }
    protected void RemoveSelectedTiles()
    {
      if (currentTile != null)
      {
        currentTile.current = false;
        currentTile.c_inTile = null;
        currentTile = null;
      }
      foreach (Tile tile in selectableTiles)
      {
        tile.Reset();
        // tile.DefaultColorOnly(false);  //TMP default color after move begin
      }
      selectableTiles.Clear();
    }
    void CalcHeading(Vector3 target)
    {
      heading = target - transform.position;
      heading.Normalize();
    }
    void SetHorizotalVelocity()
    {
      velocity = heading * moveSpeed;
    }
    public void ShowPath(Tile tile)
    {
        GeneratePath(tile);

        while (path.Count > 0)
        {
            Tile t = path.Peek();
            t.onCurrentPath = true;
            path.Pop();
        }
    }
}
