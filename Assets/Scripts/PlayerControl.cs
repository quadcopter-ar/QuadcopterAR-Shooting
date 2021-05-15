using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float Speed;
    private float ModifiedSpeed;
    private Vector3 MovementDirection;

    void Awake()
    {
        this.Speed = 200.0f;
        this.ModifiedSpeed = 0.0f;
    }

    public float getModifiedSpeed()
    {
        return this.ModifiedSpeed;
    }


    public void setModifiedSpeed(float modifiedSpeed)
    {
        this.ModifiedSpeed = modifiedSpeed;
    }

    void Update()
    {
        this.MovementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed));
    }
}
