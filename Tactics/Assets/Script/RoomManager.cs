using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    private PlayerManager[] playerManagerLst;
    private PhotonView PV;
    private Transform[] spawnPos;
    private int playerSpawnCount = 0;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }    
        DontDestroyOnLoad(gameObject);
        Instance = this;

        PV = GetComponent<PhotonView>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex >= 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PlayerManager"), Vector3.zero, Quaternion.identity);
            // SetUpRoles();
        }
    }
    public void Init()
    {
        playerSpawnCount = 0;
    }
    private List<Transform> Shuffle(List<Transform> target)
    {
        for (int i = 0; i < target.Count; i++)
        {
            Transform tmp = target[i];
            int rndId = Random.Range(i, target.Count);

            target[i] = target[rndId];
            target[rndId] = tmp;
        }
        return target;
    }
    private void GiveRoles()
    {
        int amount = PhotonNetwork.PlayerList.Length;

        string characterId = "";
        
        spawnPos = GameObject.Find("CharacterSpawns").transform.Cast<Transform>().ToArray();
        List<Transform> posLst = new List<Transform>(spawnPos);
        posLst = Shuffle(posLst);

        for (int i = 0; i < amount; i++)
        {
            string role = PhotonNetwork.PlayerList[i].CustomProperties["Role"].ToString();
            if (role == "S")
            {
                characterId = "Sherif";
            }
            else if (role == "A")
            {
                characterId = "Assassin";
            }
            Vector3 pos = posLst[0].position;

            PV.RPC("SpawnCharacter", PhotonNetwork.PlayerList[i], characterId, pos);
            posLst.RemoveAt(0);
        }
    }
    private void SetUpRoles()
    {
        int amount = PhotonNetwork.PlayerList.Length;
        int sherifId = Random.Range(0, amount);

        string characterId;
        
        spawnPos = GameObject.Find("CharacterSpawns").transform.Cast<Transform>().ToArray();
        List<Transform> posLst = new List<Transform>(spawnPos);
        posLst = Shuffle(posLst);

        for (int i = 0; i < amount; i++)
        {
            // Vector3 pos = posLst[0].position;

            // PV.RPC("SpawnCharacter", PhotonNetwork.PlayerList[i], characterId, pos);
            // posLst.RemoveAt(0);
        }
    }
    [PunRPC]
    private void SpawnCharacter(string id, Vector3 pos)
    {
        // playerManagerLst = GameObject.FindObjectsOfType<PlayerManager>();
        
        // for (int i = 0; i < playerManagerLst.Length; i++)
        // {
        //     if (playerManagerLst[i].PV.IsMine)
        //     {
        //         playerManagerLst[i].CreateController(id, pos);
        //         GameManager.Instance.SetPlayerManager(playerManagerLst[i]);
        //     }
        // }
    }
}