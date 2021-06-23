using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Asteroids;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MainManager : MonoBehaviourPunCallbacks
{
    public static MainManager Instance;

    public Text InfoText;
    public List<GameObject> l_char_init;
    public List<Vector3> l_spawn_pos;
    public Vector2 mapSize;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Hashtable props = new Hashtable{{TacticGame.PLAYER_LOADED_LEVEL, true}};
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }
    void Update()
    {
        CheckMouseOverChar();
    }
    void StartGame()
    {
        // Shuffle();
        // Spawn();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        
        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }
    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }
    private void OnCountdownTimerIsExpired()
    {
        Debug.Log("Start Game !");
        StartGame();
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
            // int id = Random.Range(0, l_spawn_pos.Count);

            // Vector3 rnd = l_spawn_pos[id];

            l_char_init[i] = Instantiate(l_char_init[i], l_spawn_pos[i], Quaternion.identity);

            // l_spawn_pos.RemoveAt(id);
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
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        // if there was no countdown yet, the master client (this one) waits until everyone loaded the level and sets a timer start
        int startTimestamp;
        bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);

        if (changedProps.ContainsKey(TacticGame.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                if (!startTimeIsSet)
                {
                    CountdownTimer.SetStartTime();
                }
            }
            else
            {
                // not all players loaded yet. wait:
                Debug.Log("setting text waiting for players! ", this.InfoText);
                InfoText.text = "Waiting for other players...";
            }
        }
    }
    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(TacticGame.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool) playerLoadedLevel)
                {
                    continue;
                }
            }
            return false;
        }

        return true;
    }
}
