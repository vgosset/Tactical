using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform container;

    [Space(10)]
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    
    [Space(10)]
    
    [SerializeField] private int obstacle_a;
    private List<Vector3> obsPos = new List<Vector3>();
    private void Start()
    {
        CreateObstacles();
        GenerateMap();
        // StartCoroutine(GenerateMap()); 
    }
    private void CreateObstacles()
    {
        for (int i = 0; i < obstacle_a; i++)
        {
            Vector3 rdnPos = new Vector3(Random.Range(0, rows), 0, Random.Range(0, columns));
            obsPos.Add(rdnPos);

            GameObject tmp = Instantiate(obstacle, rdnPos, Quaternion.identity);
            tmp.transform.parent = container;
        }
    }
    private void GenerateMap()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 point = new Vector3(i, 0, j);
                if (!obsPos.Contains(point))
                {
                    GameObject tmp = Instantiate(tile, point, Quaternion.identity);
                    tmp.transform.parent = container;
                }
                // yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
