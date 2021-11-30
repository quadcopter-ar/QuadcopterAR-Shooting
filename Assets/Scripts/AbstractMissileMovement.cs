using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MissleFactory
{

    // [RequireComponent(typeof(Rigidbody))]
    public abstract class AbstractMissileMovement : MonoBehaviour
    {
        
        protected float Speed; // abstract member of missile flying speed
        protected Vector3 FireDirection; // abstract member of missile for flying direction
        public abstract void Fire(Transform rotation, PhotonView PV); // abstract member of missile for fire triggering

    }

}

