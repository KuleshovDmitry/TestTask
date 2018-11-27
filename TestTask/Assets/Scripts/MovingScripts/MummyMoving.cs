using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MovingScripts
{
    public class MummyMoving : EnemyMoving
    {
        void Update()
        {
            speed = Controller.enemySpeed*2;
            base.Update();

        }
    }
}
