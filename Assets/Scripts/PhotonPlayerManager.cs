using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonPlayerManager : MonoBehaviour
{
    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>(); 
    }




    // Start is called before the first frame update
    void Start()
    {

        // if (PV.IsMine)
        // {
        //     CreateController();
        // }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Player"),Vector3.zero, Quaternion.identity); // initantiate player object in the game
    }

}
