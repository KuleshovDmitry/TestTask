using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MovingScripts
{
    public class ZombiMoving : EnemyMoving
    {
        void Update()
        {
            speed = Controller.enemySpeed;
            base.Update();
        }
    }
}