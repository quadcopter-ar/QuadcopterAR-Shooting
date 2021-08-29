using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    private Vector3 JoystickEulerAngles;
    void Awake()
    {
        this.JoystickEulerAngles = this.transform.rotation.eulerAngles;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(this.xAngle,this.yAngle,this.zAngle));
    }
}
