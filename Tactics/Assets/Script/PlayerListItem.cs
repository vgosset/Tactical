using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text nameTxt;
    Player player;
    private void Start()
    {
    }
    public void SetUp(Player _player)
    {
        player = _player;
        nameTxt.text = _player.NickName;
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log(other.NickName + " Left Room");
        if (player.NickName == other.NickName)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(this.gameObject);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }
}