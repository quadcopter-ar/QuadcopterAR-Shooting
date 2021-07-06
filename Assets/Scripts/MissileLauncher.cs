using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject Missle;
    private Vector3 FireDirection;
    private AudioSource audioLocal;
    private PhotonView PV;

    void Awake()
    {
        this.FireDirection = new Vector3(0.0f,0.0f,0.0f);
        audioLocal = gameObject.AddComponent<AudioSource>();
        audioLocal.volume = 0.5f;
        this.PV = GetComponent<PhotonView>();  
    }

    void Start()
    {
        if (!this.PV.IsMine)
        {
          Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    public void SetFireDirection(Vector3 fireDirection)
    {
        this.FireDirection = fireDirection;
    }
     
    void Update()
    {
        if(!this.PV.IsMine)
        {
            return;
        }
        if(Input.GetButtonDown("Jump"))
        {
            GameObject player = GameObject.FindWithTag("PlayerMain");
            Transform cameraRotation = player.transform;


            AudioClip playedSound = Resources.Load("MissileSoundEffect", typeof(AudioClip)) as AudioClip;
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.volume = 0.5f;
            //source.PlayOneShot(playedSound);
            audio.clip = playedSound;
            audio.Play();
            audio.PlayOneShot(playedSound);

            audioLocal.clip = playedSound;
            audioLocal.PlayOneShot(playedSound);
            audioLocal.Play();



            GameObject missle = (GameObject)Instantiate(this.Missle, this.gameObject.transform.position, cameraRotation.rotation);
            missle.transform.Rotate(27.63f, 0, 0);
            // missle.tag = "Missle";
            //missle.GetComponent<NormalModeMissileMovement>().SetFireDirection(this.FireDirection);
            Transform rotation = this.gameObject.transform;

            missle.GetComponent<NormalModeMissileMovement>().Fire(rotation);
            Destroy(missle,15f);
        }
    }
}
