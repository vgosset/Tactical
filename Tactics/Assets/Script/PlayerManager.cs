using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun
{
    public PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
}