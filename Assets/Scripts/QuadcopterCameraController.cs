using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuadcopterVR;

public class QuadcopterCameraController
{
    private Camera ManagedCamera;
    private LineRenderer CameraLineRenderer;
    private GameObject Target;
    private Vector3 CameraLogicCenter;
    public void Awake()
    {
        this.ManagedCamera = this.gameObject.GetComponent<Camera>();
        this.CameraLineRenderer = this.gameObject.GetComponent<LineRenderer>();
        this.AttachCamera();
        this.CameraLogicCenter = Vector3(0.0f,0.0f,this.Target.transform.position.z - this.transform.position.z);
    }

    public void LateUpdate()
    {
        if (this.DrawLogic)
        {
            this.CameraLineRenderer.enabled = true;
            this.DrawCameraLogic();
        }
        else
        {
            this.CameraLineRenderer.enabled = false;
        }
        this.AttachCamera();
        this.DrawCameraLogic();
    }
    public void AttachCamera()
    {
        this.Target.transform.position = this.ManagedCamera.main.ScreenToWorldPoint(Vector3(Screen.width/2, Screen.height/2, this.ManagedCamera.main.nearClipPlane));
    }

    public void DrawCameraLogic()
    {
        var playerZCameraCoordinate = this.Target.transform.position.z - this.transform.position.z;
        this.MarkerCenter.z = playerZCameraCoordinate;

        this.CameraLineRenderer.positionCount = 5;
        this.CameraLineRenderer.useWorldSpace = false;
        this.CameraLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        this.CameraLineRenderer.material.color = Color.white;
        this.CameraLineRenderer.SetPosition(0, new Vector3(this.MarkerCenter.x - this.MarkerWidth/2, this.MarkerCenter.y, this.MarkerCenter.z));
        this.CameraLineRenderer.SetPosition(1, new Vector3(this.MarkerCenter.x + this.MarkerWidth/2, this.MarkerCenter.y, this.MarkerCenter.z));
        this.CameraLineRenderer.SetPosition(2, this.MarkerCenter);
        this.CameraLineRenderer.SetPosition(3, new Vector3(this.MarkerCenter.x, this.MarkerCenter.y - this.MarkerLength/2, this.MarkerCenter.z));
        this.CameraLineRenderer.SetPosition(4, new Vector3(this.MarkerCenter.x, this.MarkerCenter.y + this.MarkerLength/2, this.MarkerCenter.z));     
    }
}
