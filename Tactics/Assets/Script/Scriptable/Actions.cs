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
    public TargetSettings t_target;
    public ZoneEffect a_zone;
    public GameObject projectile;
    public int movementAmount;
    public int rallPm;
    public int damage;
    public int heal;
    public int line_diag_number;
    public int pm_cost;
    public int pa_cost;
    public string name;
    public Sprite icon;
    public int turnLimit;
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
