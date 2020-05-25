using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public List<GameObject> l_char_init;
    public Vector2 mapSize;

    void Awake()
    {
        Instance = this;
        Init();
    }

    void Start()
    {
      StartGame();
    }
    void Update()
    {
        CheckMouseOverChar();
    }
    void Init()
    {
        Shuffle();
        Spawn();
    }
    void StartGame()
    {
        TurnManager.Instance.InitCharList(l_char_init);
        TurnManager.Instance.InitTurn();
        ActionManager.Instance.InitCharList(l_char_init);
    }
    void Shuffle()
    {
      for (int i = 0; i < l_char_init.Count; i++)
      {
          GameObject tmp = l_char_init[i];
          int randomIndex = Random.Range(i, l_char_init.Count);

          l_char_init[i] = l_char_init[randomIndex];
          l_char_init[randomIndex] = tmp;
      }
    }
    void Spawn()
    {
      l_char_init[0] = Instantiate(l_char_init[0], new Vector3(9,1.5f,0), Quaternion.identity);
    //   l_char_init[0] = Instantiate(l_char_init[0], new Vector3(0,1.5f,0), Quaternion.identity);
      l_char_init[1] = Instantiate(l_char_init[1], new Vector3(0,1.5f,12), Quaternion.identity);
    }


    public void DestroyItem(GameObject target, float delay)
    {
        StartCoroutine(Cor_DestroyTimed(target, delay));
    }
    private IEnumerator Cor_DestroyTimed(GameObject target, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(target);
    }

    public void CheckMouseOverChar()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

          if (hit.collider.tag == "Character")
                hit.transform.GetComponent<Character>().ShowUiStats();
          else
              UiManager.Instance.HideUiChar();
        }
    }
    public void EndGame()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
