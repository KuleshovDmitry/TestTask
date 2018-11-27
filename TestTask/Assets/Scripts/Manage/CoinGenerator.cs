using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Assets.Scripts
{
    public class CoinGenerator : MonoBehaviour
    {
        static float delay = 6.0f;
        public GameObject CoinPrefab;
        void Start()
        {

        }
        void Update()
        {
            delay -= Time.deltaTime;
            if (delay < 1.0f)
            {
                if (Controller.currentCoin < 10)
                {
                    Instantiate(CoinPrefab, GetRandomPosition(), Quaternion.identity); delay = 6.0f;
                    Controller.currentCoin++;
                }
            }
        }
        public Vector3 GetRandomPosition()
        {
            System.Random random = new System.Random();
            int startX, startY;
            do
            {
                startX = random.Next(1, Controller.height - 2);
                startY = random.Next(1, Controller.width - 2);
            } while (Controller.mazeHandler.maze.map[startX, startY] != 1);

            return new Vector3((startX - Controller.height / 2) / 0.88f / 2 + 0.35f,
                (startY - Controller.width / 2) / 0.88f / 2 + 0.3f, 1);
        }
    }
}
