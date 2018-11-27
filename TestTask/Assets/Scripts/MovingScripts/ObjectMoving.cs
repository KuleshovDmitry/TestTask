using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MovingScripts
{
    public abstract class ObjectMoving : MonoBehaviour
    {
        protected float speed;
        public GameObject thisGameObject;
        public Animator animator;
        protected int x, y;
        protected Vector3 nextVector;

        public void Start()
        {
        }
        public void Update() { }
    }
}
