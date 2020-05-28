using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public List<GameObject> l_char_init;
    public List<Vector3> l_spawn_pos;
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
        for (int i = 0; i < l_char_init.Count; i++)
        {
            int id = Random.Range(0, l_spawn_pos.Count);

            Vector3 rnd = l_spawn_pos[id];

            l_char_init[i] = Instantiate(l_char_init[i], rnd, Quaternion.identity);

            l_spawn_pos.RemoveAt(id);
        }
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
