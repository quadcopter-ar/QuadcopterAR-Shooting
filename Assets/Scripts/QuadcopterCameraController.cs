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
        this.AttachCamera();
        this.CameraLogicCenter = new Vector3(0.0f,0.0f,this.Target.transform.position.z - this.transform.position.z);
        this.MarkerCenter = new Vector3(0.0f,0.0f,0.0f);
        this.MarkerLength = 5.0f;
        this.MarkerWidth = 5.0f;
        this.Target.transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
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
    public override void AttachCamera()
    {
        var target = this.Target.transform.position;
        this.ManagedCamera.transform.position = new Vector3(target.x, target.y + 12.0f, target.z - 39.0f);
        Debug.Log(this.Target.transform.position);
    }

    public override void DrawCameraLogic()
    {
        var playerZCameraCoordinate = this.Target.transform.position.z - this.transform.position.z - 39.0f;
        this.MarkerCenter.z = playerZCameraCoordinate;
        this.MarkerCenter.y += 12;

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
