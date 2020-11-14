using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Actions/TargetSettings")]

public class TargetSettings : ScriptableObject 
{
    public TargetType type;
    public bool self;
    public bool ldv;
    public int minRange;
    public int maxRange;
}

public enum TargetType
{
    ALIES,
    EMPTY,
    ENEMIES,
    ALL,
}
