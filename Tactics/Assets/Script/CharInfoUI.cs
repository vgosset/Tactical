using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoUI : MonoBehaviour
{
    [SerializeField] private Text life;
    [SerializeField] private RawImage icon;
    public void UpdateLife(int amount)
    {
        life.text = amount.ToString();
    }
    public void Setup(int life, Texture texture)
    {
        UpdateLife(life);
            icon.texture = texture;
    }
}