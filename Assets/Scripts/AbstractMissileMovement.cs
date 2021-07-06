using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MissleFactory
{

    // [RequireComponent(typeof(Rigidbody))]
    public abstract class AbstractMissileMovement : MonoBehaviour
    {
        
        protected float Speed;
        protected Vector3 FireDirection;
        public abstract void Fire(Transform rotation, PhotonView PV);

    }

}

