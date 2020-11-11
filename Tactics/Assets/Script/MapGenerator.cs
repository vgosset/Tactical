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
    private List<Vector3> obsChar = new List<Vector3>();

    private List<Character> l_char = new List<Character>();
    private void Start()
    {
        SpawnChar();
        CreateObstacles();
        GenerateMap();

        TurnManager.Instance.InitCharList(l_char);
        // StartCoroutine(GenerateMap());
    }
    private void CreateObstacles()
    {
        for (int i = 0; i < obs_a; i++)
        {
            Vector3 rdnPos = new Vector3(Random.Range(obsOff, rows - obsOff), 0f, Random.Range(obsOff, col - obsOff));
            if (!obsChar.Contains(rdnPos) && !obsPos.Contains(rdnPos))
            {
                obsPos.Add(rdnPos);

                GameObject tmp = Instantiate(obstacle, rdnPos, Quaternion.identity);
                tmp.transform.parent = container;
            }
            else
            {
                Debug.Log("dsds");
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
    private void SpawnChar()
    {
        Vector3 char1Pos  = new Vector3((int) Random.Range(charRows.x, charRows.y), 1.5f, (int) Random.Range(charCol.x, charCol.y));
        Vector3 char2Pos  = new Vector3((int) Random.Range(rows - charRows.y, rows - charRows.x - 1), 1.5f, (int) Random.Range(charCol.x, charCol.y));
        
        obsChar.Add(new Vector3(char1Pos.x, 0, char1Pos.z));
        obsChar.Add(new Vector3(char2Pos.x, 0, char2Pos.z));

        l_char.Add(Instantiate(l_char_obj[0], char1Pos, Quaternion.identity).transform.GetComponent<Character>());
        l_char.Add(Instantiate(l_char_obj[1], char2Pos, Quaternion.identity).transform.GetComponent<Character>());
    }
    public void ResetScene()
    {
        SceneManager.LoadScene("Gameplay1");
    }
}
