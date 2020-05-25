using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesColor : MonoBehaviour
{
    public static TilesColor Instance;

    public List<Color> colorLst;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {

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
        return colorLst[4];
    }
}
