using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    private const string glyphs= "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static PhotonLauncher Instance;
    [SerializeField] private MenuManager menu_m;
    [SerializeField] private List<Player> playerLst = new List<Player>();
    private PhotonView PV;
    private string roomName;
    private string joinRoomName;

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
    private IEnumerator Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.LogError("Start Connecting To Master");
        }

        yield return new WaitForSeconds(0.1f);

        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.CurrentRoom.IsOpen)
            {
                Debug.LogError("Room Open --> Join");
                menu_m.JoinRoom();
                SetUpRoom();
            }
            else
            {
                Debug.LogError("Room Close --> Waiting for host");
            }
        }
    }
    public void CreateRoom()
    {
        SetUpPlayerName();

        roomName = GenerateRoomName();
        // CHECK IF NAME ALREADY EXIST
        Debug.LogError("Room Created : Name -->  " + roomName);
        PhotonNetwork.CreateRoom(roomName);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void JoinRoom()
    {
        joinRoomName = menu_m.GetJoinRoomName();
        SetUpPlayerName();

        if (joinRoomName.Length != 6)
        {
            return;
        }
        menu_m.JoinRoom();
        PhotonNetwork.JoinRoom(joinRoomName);
    }
    
    private string GenerateRoomName()
    {
        string name = "";

        for(int i = 0; i < 6; i++)
        {
            name += glyphs[Random.Range(0, glyphs.Length)];
        }
        return name;
    }
    
    private void RemoveFromPlayerList(Player player)
    {
        for (int i = 0; i < playerLst.Count; i++)
        {
            if (player.UserId == playerLst[i].UserId)
            {
                playerLst.RemoveAt(i);
            }
        }
    }
    private void JoinLobby()
    {
        Debug.LogError("Join Lobby");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void AddInRoomPlayer()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            menu_m.AddPlayerInLobby(players[i]);
            playerLst.Add(players[i]);
        }
    }
    private void SetUpPlayerName()
    {
        string name = menu_m.GetPlayerName();
     
        if (name == "")
        {
            name = "Player";
        }
        PhotonNetwork.LocalPlayer.NickName = name;
    }
    public void StartGame()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.IsOpen)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.LogError("Close Room");
            PV.RPC("LoadingScreen", RpcTarget.All);
            LoadLevel(1);
        }
    }
    [PunRPC]
    private void LoadingScreen()
    {
        menu_m.SetMenuState("Loading", true);
        menu_m.SetMenuState("Room", false);
    }
    [PunRPC]
    private void LoadLevel(int id)
    {
        PhotonNetwork.LoadLevel(id);
    }
    private void SetUpRoom()
    {
        menu_m.RoomJoined(PhotonNetwork.CurrentRoom.Name);
        AddInRoomPlayer();
        menu_m.SetMasterClientMenu(PhotonNetwork.IsMasterClient);
    }
    
    ////////////  PHOTON CALLBACKS ////////////

    public override void OnLeftRoom()
    {
        Debug.LogError("Room Left");
        menu_m.SetMenuState("Room", false);
    }
    public override void OnJoinedRoom()
    {
        SetUpRoom();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        menu_m.SetMasterClientMenu(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room Creation Failed : " + message);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room " + joinRoomName + " Join Failed : " + message);
        
        menu_m.BackToJoinAfterFail(message);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        menu_m.AddPlayerInLobby(newPlayer);
        playerLst.Add(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        RemoveFromPlayerList(newPlayer);
    }
    public override void OnConnectedToMaster()
    {
        Debug.LogError("Connected to Master");
        JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        menu_m.SetMenuState("Panel", true);
        menu_m.SetMenuState("Loading", false);
    }
    public virtual void OnRoomPropertiesUpdate(Room room)
    {
        Debug.LogError("Room Update To : " + room.IsOpen);
        if (room.IsOpen)
        {
            menu_m.JoinRoom();
            SetUpRoom();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
