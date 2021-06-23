using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu[] menuLst;
    [SerializeField] private GameObject playerListItem;
    [SerializeField] private Transform playerList;
    [SerializeField] private Text errorMsg;
    [SerializeField] private Text playerName;
    [SerializeField] private Text roomName;
    [SerializeField] private InputField inputRoomName;
   
    void Start()
    {
        SetMenuState("Loading", true);
        inputRoomName.onValueChanged.AddListener( delegate { ToUpper(); } );
        inputRoomName.characterLimit = 6;    
    }
    void ToUpper ()
    {
        inputRoomName.text = inputRoomName.text.ToUpper();
    }
    public void SetMenuState(string name, bool state)
    {
        for (int i = 0; i < menuLst.Length; i++)
        {
            if (menuLst[i].id == name)
            {
                menuLst[i].SetState(state);
            }
        }
    }
    public void RoomJoined(string room)
    {
        SetMenuState("Loading", false);
        SetMenuState("Room", true);

        roomName.text = room;
    }
    public string GetJoinRoomName()
    {
        return inputRoomName.text;
    }
    public void BackToJoinAfterFail(string msg)
    {
        SetMenuState("Loading", false);
        SetMenuState("JoinRoom", true);

        SetErrorMsg(msg);
    }
    public void JoinRoom()
    {
        SetMenuState("Loading", true);
        SetMenuState("JoinRoom", false);
    }
    private void SetErrorMsg(string msg)
    {
        errorMsg.text = msg;
        SetMenuState("Error", true);
    }
    public string GetPlayerName()
    {
        return playerName.text;
    }
    public void SetMasterClientMenu(bool state)
    {
        SetMenuState("Start", state);
    }
    public void AddPlayerInLobby(Player player)
    {
        Instantiate(playerListItem, playerList).GetComponent<PlayerListItem>();
    }
}