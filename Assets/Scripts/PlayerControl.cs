using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float Speed;
    private float ModifiedSpeed;
    private Vector3 MovementDirection;
    [SerializeField]
    private Camera MainCamera;

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
        this.gameObject.GetComponent<MissileLauncher>().SetFireDirection(this.MovementDirection);
        // this.gameObject.transform.localRotation = Quaternion.Euler(this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed));
        // this.MainCamera.transform.localRotation = Quaternion.Euler(this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed));
    }
}
