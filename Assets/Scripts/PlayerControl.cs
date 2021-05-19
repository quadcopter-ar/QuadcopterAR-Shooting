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
        this.MovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        this.gameObject.GetComponent<MissileLauncher>().SetFireDirection(this.MovementDirection);
        // var targetX = this.gameObject.transform.rotation.x + (this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed)).x;
        // var targetY = this.gameObject.transform.rotation.y + (this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed)).y;
        // var targetZ = this.gameObject.transform.rotation.z + (this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed)).z;
        // this.gameObject.transform.rotation = Quaternion.Euler(targetX, targetY, targetZ);
        // this.gameObject.transform.rotation.x += 10.0f;
        // this.MainCamera.transform.rotation.x += 10.0f;
        this.gameObject.transform.RotateAround(this.MainCamera.transform.position, this.MovementDirection, (this.ModifiedSpeed + this.Speed) * Time.deltaTime);
        // this.MainCamera.transform.RotateAround(this.MainCamera.transform.position, this.MovementDirection, (this.ModifiedSpeed + this.Speed) * Time.deltaTime);
        // var cameraX = this.MainCamera.transform.rotation.x + (this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed)).x;
        // var cameraY = this.MainCamera.transform.rotation.y + (this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed)).y;
        // var cameraZ = this.MainCamera.transform.rotation.z + (this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed)).z;
        // this.MainCamera.transform.rotation = Quaternion.Euler(cameraX, cameraY, cameraZ);
        // this.MainCamera.transform.localRotation = Quaternion.Euler(this.MovementDirection * Time.deltaTime * (this.ModifiedSpeed+this.Speed));
    }
}
