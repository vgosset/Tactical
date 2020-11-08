using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    public List<Color> colorLst;
    private GameObject[] tiles;

    void Awake()
    {
        Instance = this;
    }
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }
    public Color GetTileColor(string id)
    {
        if (id == "current")
          return colorLst[0];
        if (id == "PathOptions")
            return colorLst[1];
        if (id == "Path")
            return colorLst[2];
        if (id == "ActionsOptions")
            return colorLst[3];
        if (id == "MovmentDetection")
            return colorLst[4];
        return colorLst[5];
    }
    public void RemovePrediction()
    {
        if (tiles != null)
        {
            foreach (GameObject tile in tiles)
            {
                tile.GetComponent<Tile>().RemoveMovementPrediction();
            }
        }
    }
    public void UnSelectAllTiles()
    {
        if (tiles != null)
        {
            foreach (GameObject tile in tiles)
            {
                tile.GetComponent<Tile>().Reset();
            }
        }
    }
    public void ComputeAdjLst(bool player, bool walkable)
    {
        if (tiles != null)
        {
            foreach (GameObject tile in tiles)
            {
                Tile t = tile.GetComponent<Tile>();
                t.FindNeighbors(2, player, walkable);
            }
        }
    }
    public void RemoveLastPath()
    {
        if (tiles != null)
        {
            foreach (GameObject tile in tiles)
            {
                Tile t = tile.GetComponent<Tile>();

                t.mouseOnIt = false;
                t.onCurrentPath = false;
            }
        }
    }
}