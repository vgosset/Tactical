using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSetup : MonoBehaviour
{
    [SerializeField] private GameObject slot; 
    [SerializeField] private Material mat; 
    [SerializeField] private int[] range_a; 
    [SerializeField] private float height; 
    private float heightSpawn = 0f;
    private void Start()
    {
        heightSpawn = 0.9f; 
        SpawnSlot();
    }
    void Update()
    {
        
    }
    private void SpawnSlot()
    {
        int rndSlot = Random.Range(range_a[0], range_a[1]);

        for (int i = 0; i < rndSlot; i++)
        {
            GameObject tmp = Instantiate(slot, new Vector3(transform.position.x, heightSpawn, transform.position.z), slot.transform.rotation);
            tmp.transform.parent = this.transform;

            heightSpawn += height;
        }
    }
    void OnMouseEnter()
    {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.3f);
    }
    void OnMouseExit()
    {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);
    }
}
