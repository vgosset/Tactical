using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Character c_inTile;
    public bool walkable = true;
    public bool current = false;
    public bool onCurrentPath = false;
    public bool mouseOnIt = false;
    public bool ldvBlock = false;
    public bool target = false;
    public bool selectableMove = false;
    public bool selectableAction = false;
    public bool movementDetection = false;


    public List<Tile> adjTileLst = new List<Tile>();

    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;
    bool defaultColorOnly = false;

    void Start()
    {

    }

    void Update()
    {
        if (!this.name.Contains("Obstacle"))
        {
          TileColorHandeler();
        }
       
    }
    private void TileColorHandeler()
    {
 // if (current)
          // GetComponent<Renderer>().material.color = TilesColor.Instance.GetTileColor("current");
        if (movementDetection)
            GetComponent<Renderer>().material.color = TilesColor.Instance.GetTileColor("MovmentDetection");
        else if (target || onCurrentPath)
            GetComponent<Renderer>().material.color = TilesColor.Instance.GetTileColor("Path");
        else if (selectableMove)
          GetComponent<Renderer>().material.color = TilesColor.Instance.GetTileColor("PathOptions");
        else if (selectableAction)
            GetComponent<Renderer>().material.color = TilesColor.Instance.GetTileColor("ActionsOptions");
        else
            GetComponent<Renderer>().material.color = TilesColor.Instance.GetTileColor("none");
    }

    public void Reset()
    {
      adjTileLst.Clear();

      target = false;
      selectableMove = false;
      selectableAction = false;

      RemoveMovementPrediction(); //  TO APPLY FOR ALL STATS

      visited = false;
      parent = null;
      distance = 0;
    }
    public void RemoveMovementPrediction()
    {
        movementDetection = false;
    }
    public void DefaultColorOnly(bool state)
    {
        defaultColorOnly = false; //TMP default color after move begin
    }
    public void FindNeighbors(float jumpHeight, bool includePlayer, bool walkableTile)
    {
      Reset();

      CheckTile(Vector3.forward, jumpHeight, includePlayer, walkableTile);
      CheckTile(-Vector3.forward, jumpHeight, includePlayer, walkableTile);
      CheckTile(Vector3.right, jumpHeight, includePlayer, walkableTile);
      CheckTile(-Vector3.right, jumpHeight, includePlayer, walkableTile);
    }
    public void CheckTile(Vector3 direction, float jumpHeight, bool includePlayer, bool walkableTile)
    {
      Vector3 halfExtants = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
      Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtants);

      foreach (Collider item in colliders)
      {
        Tile tile = item.GetComponent<Tile>();
        
        if (walkableTile && tile != null && tile.walkable || !walkableTile && tile != null)
        {
          if (!includePlayer && !tile.isBusy() || includePlayer)
          {
            adjTileLst.Add(tile);
          }
          // if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
          // {
          //
          //   adjTileLst.Add(tile);
          // }
        }
      }
    }
    public bool isBusy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1))
        {
            if (hit.transform.tag == "Character")
                return true;
        }
        return false;
    }
}
