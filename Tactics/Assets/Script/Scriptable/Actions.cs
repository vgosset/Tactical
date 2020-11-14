using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(menuName = "Actions/Action")]

public class Actions : ScriptableObject 
{
    public ActionType t_action;
    public ImpactMovement t_impact;
    [Space(10)]
    public TargetSettings t_target;
    public ZoneEffect a_zone;
    [Space(10)]
    public GameObject projectile;
    public int movementAmount;
    public int rallPm;
    [Space(10)]
    public int damage;
    public int heal;
    [Space(10)]
    public int line_diag_number;
    [Space(10)]
    public int pm_cost;
    public int pa_cost;
    [Space(10)]
    public string name;
    public Sprite icon;
    [Space(10)]
    public int turnLimit;
    public string animTrigger;
}
public enum ImpactMovement
{
    NONE,
    PUSH,
    PULL,
    RUSH,
    JUMP,
}
public enum ActionType
{
    LINE,
    DIAG,
    CLASSIC,
}
