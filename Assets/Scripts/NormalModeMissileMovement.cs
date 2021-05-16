using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MissleFactory;

public class NormalModeMissileMovement : AbstractMissileMovement
{
    void Awake()
    {
        this.Speed = 1000.0f;
    }
    public override void Fire()
    {
        this.GetComponent<Rigidbody>().AddForce(this.FireDirection*this.Speed);
        // Debug.Log(this.FireDirection*this.Speed);
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
