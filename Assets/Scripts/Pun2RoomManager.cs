using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;
public class Pun2RoomManager : MonoBehaviourPunCallbacks
{
    public static Pun2RoomManager Instance;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            if(PhotonNetwork.IsMasterClient){
                GameObject quadCopter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Player"), new Vector3(0f,1000f,0f),Quaternion.Euler(0f,180f,0f));
                Debug.Log(quadCopter.transform.position);
            }
            else
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Player"), Vector3.zero,Quaternion.identity);
                Debug.Log("isNotMasterClient");
            }
            Debug.Log("in test scene");

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
