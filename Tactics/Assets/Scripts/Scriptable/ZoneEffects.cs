using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Actions/ZoneEffect")]

public class ZoneEffect : ScriptableObject 
{
    public ZoneType type;
    public int z_line_diag_number;
    public int rangeMin;
    public int rangeMax;
}

public enum ZoneType
{
    LINE,
    DIAG,
    CLASSIC,
}
