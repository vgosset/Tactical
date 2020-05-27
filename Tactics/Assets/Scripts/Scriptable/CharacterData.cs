using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(menuName = "CharacterData")]

public class CharacterData : ScriptableObject {
    public int id;
    public List<Actions> l_action;
    public int n_pm;
    public int n_pa;
    public int n_lifes;
    public Texture uiTexture;
}
