using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuadcopterVR;

public class QuadcopterCameraController: AbstractCameraController
{
    private Camera ManagedCamera;
    private LineRenderer CameraLineRenderer;
    private Vector3 CameraLogicCenter;
    private float MarkerLength;
    private float MarkerWidth;
    private Vector3 MarkerCenter;
    public void Awake()
    {
        this.ManagedCamera = this.gameObject.GetComponent<Camera>();
        this.CameraLineRenderer = this.gameObject.GetComponent<LineRenderer>();
        var target = this.Target.transform.position;
        // this.AttachCamera();
        this.CameraLogicCenter = new Vector3(0.0f,0.0f,this.Target.transform.position.z - this.transform.position.z);
        this.MarkerCenter = new Vector3(0.0f, 0.0f, 0.0f);
        this.MarkerLength = 2.0f;
        this.MarkerWidth = 2.0f;
        var light = GameObject.Find("Light");
        if(light)
        {
            Destroy(light);
        }
        
        this.ManagedCamera.transform.position = new Vector3(target.x, target.y + 10.0f, target.z - 30.0f);
        this.Target.transform.Rotate(new Vector3(0.0f,270.0f,0.0f));
        // this.ManagedCamera.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        // this.Target.transform.rotation = Quaternion.Euler(0.0f,270.0f,0.0f);

    }

    public void LateUpdate()
    {
        // if (this.DrawLogic)
        // {
        //     this.CameraLineRenderer.enabled = true;
        //     this.DrawCameraLogic();
        // }
        // else
        // {
        //     this.CameraLineRenderer.enabled = false;
        // }
        // this.AttachCamera();
        // this.DrawCameraLogic();
    }
    // public override void AttachCamera()
    // {

    // }

    public override void DrawCameraLogic()
    {

        // this.MarkerCenter = Quaternion.AngleAxis(this.ManagedCamera.transform.rotation) * this.MarkerCenter;
        this.CameraLineRenderer.positionCount = 7;
        this.CameraLineRenderer.useWorldSpace = false;
        this.CameraLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        this.CameraLineRenderer.material.color = Color.green;
        this.CameraLineRenderer.SetWidth(0.3f, 0.3f);
        this.CameraLineRenderer.SetPosition(0, new Vector3(this.MarkerCenter.x - this.MarkerWidth/2, this.MarkerCenter.y, this.MarkerCenter.z));
        this.CameraLineRenderer.SetPosition(1, this.MarkerCenter);
        this.CameraLineRenderer.SetPosition(2, new Vector3(this.MarkerCenter.x + this.MarkerWidth/2, this.MarkerCenter.y, this.MarkerCenter.z));
        this.CameraLineRenderer.SetPosition(3, this.MarkerCenter);
        this.CameraLineRenderer.SetPosition(4, new Vector3(this.MarkerCenter.x, this.MarkerCenter.y - this.MarkerLength/2, this.MarkerCenter.z));
        this.CameraLineRenderer.SetPosition(5, this.MarkerCenter);
        this.CameraLineRenderer.SetPosition(6, new Vector3(this.MarkerCenter.x, this.MarkerCenter.y + this.MarkerLength/2, this.MarkerCenter.z));     

    }
}
