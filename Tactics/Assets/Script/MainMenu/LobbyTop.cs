﻿using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.Asteroids
{
    public class LobbyTop : MonoBehaviour
    {
        private readonly string connectionStatusMessage = "    Connection Status: ";

        [Header("UI References")]
        public Text ConnectionStatusText;

        #region UNITY

        public void Update()
        {
            ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        }

        #endregion
    }
}