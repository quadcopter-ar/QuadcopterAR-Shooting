using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // connect
    }

    public override void OnConnectedToMaster() // when on connected to master, output the debug msg
    {
        Debug.Log("game is connected to the "+PhotonNetwork.CloudRegion+" server!");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
