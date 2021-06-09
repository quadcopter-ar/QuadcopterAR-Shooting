using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MissleFactory;

public class NormalModeMissileMovement : AbstractMissileMovement
{
    void Awake()
    {
        this.Speed = 100.0f;
    }
    public override void Fire(Transform rotation)
    {
        //this.GetComponent<Rigidbody>().AddForce(this.FireDirection*this.Speed);

        // Debug.Log(this.FireDirection*this.Speed);
        var projectileRigidBody = this.GetComponent<Rigidbody>();
        Vector3 correctingVector = new Vector3(1.0f, 0.0f, 1.0f);
        Vector3 test = new Vector3();
        test = Vector3.Scale(correctingVector, this.transform.forward);
        projectileRigidBody.velocity = test * this.Speed;
    }

    public void SetFireDirection(Vector3 fireDirection)
    {
        this.FireDirection = fireDirection;
    }

    public Vector3 GetFireDirection()
    {
        return this.FireDirection;
    }

    public void SetSpeed(float speed)
    {
        this.Speed = speed;
    }

    public float getSpeed()
    {
        return this.Speed;
    }
}
