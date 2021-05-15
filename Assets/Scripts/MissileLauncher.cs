using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject Missle;
    private Vector3 FireDirection;

    void Awake()
    {
        this.FireDirection = new Vector3(0.0f,0.0f,0.0f);
    }

    public void SetFireDirection(Vector3 fireDirection)
    {
        this.FireDirection = fireDirection;
    }
     
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GameObject missle = (GameObject)Instantiate(this.Missle, this.gameObject.transform.position, Quaternion.identity);
            // missle.tag = "Missle";
            missle.GetComponent<NormalModeMissileMovement>().SetFireDirection(this.FireDirection);
            missle.GetComponent<NormalModeMissileMovement>().Fire();
            Destroy(missle,15f);
        }
    }
}
