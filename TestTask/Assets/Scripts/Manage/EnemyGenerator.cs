using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Assets.Scripts
{
    public class EnemyGenerator : MonoBehaviour
    {
        public GameObject ZombiPrefab;
        public GameObject MummyPrefab;
        void Start()
        {
            CreateZombi();
            Controller.enemySpeed = 1;
            Controller.AddCoin += CreateMummy;
            Controller.AddCoin += CreateZombi;
            Controller.AddCoin += StartHunting;
            Controller.AddCoin += AddEnemySpeed;
        }
        
        void Update()
        {
        }
        public void CreateZombi()
        {
            if (Controller.coin == 5||Controller.coin==0)
                Instantiate(ZombiPrefab);
        }
        public void CreateMummy()
        {
            if (Controller.coin == 10)
                Instantiate(MummyPrefab);
        }
        public void StartHunting()
        {
            if (Controller.coin ==20)
                Controller.hunt = true;
        }
        public void AddEnemySpeed()
        {
            if (Controller.coin>20)
            Controller.enemySpeed+=Controller.enemySpeed*0.05f;
        }
    }
}
