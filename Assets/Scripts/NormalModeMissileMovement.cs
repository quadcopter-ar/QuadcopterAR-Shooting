using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MissleFactory;
using Photon.Pun;

public class NormalModeMissileMovement : AbstractMissileMovement
{
    // private PhotonView PV;
    private Rigidbody RB; // for rigibody component
    void Awake()
    {
        this.Speed = 100.0f;
        // this.PV = GetComponent<PhotonView>();
        this.RB = GetComponent<Rigidbody>(); // get rigibody component
    }

    void Start()
    {
        // if(!PV.IsMine)
        // {
        //     Destroy(this.RB);
        // }

    }
    public override void Fire(Transform rotation, PhotonView PV)
    {
        if(!PV.IsMine)
        {
            Destroy(this.RB);
            return;
        }
        //this.GetComponent<Rigidbody>().AddForce(this.FireDirection*this.Speed);

        // Debug.Log(this.FireDirection*this.Speed);
        // initializations
        var projectileRigidBody = this.RB; 
        Vector3 correctingVector = new Vector3(1.0f, 0.0f, 1.0f);
        Vector3 test = new Vector3();
        test = Vector3.Scale(correctingVector, this.transform.forward);
        projectileRigidBody.velocity = test * this.Speed;
    }

    public void SetFireDirection(Vector3 fireDirection) // set fire direction
    {
        this.FireDirection = fireDirection;
    }

    public Vector3 GetFireDirection() // return fire direction
    {
        return this.FireDirection;
    }

    public void SetSpeed(float speed) // set speed for missile flying
    {
        this.Speed = speed;
    }

    public float getSpeed() // return the speed pf missile flying for the spawned object
    {
        return this.Speed;
    }
}
