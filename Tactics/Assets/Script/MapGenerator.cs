using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> l_char_obj;
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform container;

    [Space(10)]
    [SerializeField] private int rows;
    [SerializeField] private int col;
    
    [Space(10)]

    [SerializeField] private Vector2 charRows;
    [SerializeField] private Vector2 charCol;
    
    [Space(10)]
    
    [SerializeField] private int obs_a;
    [SerializeField] private int obsOff;
    private List<Vector3> obsPos = new List<Vector3>();
    private List<Vector3> cPos = new List<Vector3>();

    private List<Character> l_char = new List<Character>();
    private void Start()
    {
        // SpawnChar();
        CreateObstacles();
        GenerateMap();

        // TurnManager.Instance.InitCharList(l_char);
        // StartCoroutine(GenerateMap());
    }
    private void CreateObstacles()
    {
        for (int i = 0; i < obs_a; i++)
        {
            Vector3 rdnPos = new Vector3(Random.Range(obsOff, rows - obsOff), 0f, Random.Range(obsOff, col - obsOff));
            if (!cPos.Contains(rdnPos) && !obsPos.Contains(rdnPos))
            {
                obsPos.Add(rdnPos);

                GameObject tmp = Instantiate(obstacle, rdnPos, Quaternion.identity);
                tmp.transform.parent = container;
            }
            else
            {
                i--;
            }
        }
    }
    private void GenerateMap()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < col; j++)
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
    private void SpawnChar() // TMP
    {
        int rnd = Random.Range(0, 2);

        Vector3 c1Pos  = new Vector3((int) Random.Range(charRows.x, charRows.y), 1.5f, (int) Random.Range(charCol.x, charCol.y));
        Vector3 c2Pos  = new Vector3((int) Random.Range(rows - charRows.y, rows - charRows.x - 1), 1.5f, (int) Random.Range(charCol.x, charCol.y));
        
        cPos.Add(new Vector3(c1Pos.x, 0, c1Pos.z));
        cPos.Add(new Vector3(c2Pos.x, 0, c2Pos.z));

        GameObject c1Obj = Instantiate(l_char_obj[rnd], c1Pos, Quaternion.identity);
        c1Obj.transform.rotation = Quaternion.Euler(0, 90, 0);

        Character c1 = c1Obj.GetComponent<Character>();
        c1.Setup(0);
        l_char.Add(c1);

        l_char_obj.RemoveAt(rnd);

        GameObject c2Obj = Instantiate(l_char_obj[0], c2Pos, Quaternion.identity);
        c2Obj.transform.rotation = Quaternion.Euler(0, -90, 0);
        
        Character c2 = c2Obj.GetComponent<Character>();
        c2.Setup(1);
        l_char.Add(c2);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene("Gameplay1");
    }
}
