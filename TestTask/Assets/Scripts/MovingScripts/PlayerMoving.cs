using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MovingScripts
{
    public class PlayerMoving : ObjectMoving
    {
        float duration = 0;

        Vector3 nextPosition = new Vector3();
        Vector3 rotateVector = new Vector3();

        public void Start()
        {
            {
                Controller.DoDeserialize();
                System.Random random = new System.Random();
                int startX, startY;
                do
                {
                    startX = random.Next(1, Controller.height - 2);
                    startY = random.Next(1, Controller.width - 2);
                } while (Controller.mazeHandler.maze.map[startX, startY] != 1);


                transform.position = new Vector3((startX - Controller.height / 2) / 0.88f / 2f + 0.35f,
                    (startY - Controller.width / 2) / 0.88f / 2f + 0.3f, 0);
                Controller.duration = 0;
            }
        }
        public void Update()
        {
            speed = Controller.enemySpeed * 7;//скорость противников растет, они не должны бегать быстрее игрока
            duration += Time.deltaTime;
            animator.speed = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                MoveTop();
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                MoveBot();
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Controller.duration = duration;
                Application.LoadLevel(1);
                Controller.EndGame(true);
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Coin")
            {
                Destroy(collider.gameObject);
                Controller.InduceAddCoin();
                Controller.currentCoin--;
            }
            if (collider.tag == "Enemy")
            {
                 Controller.duration = duration;
                 Application.LoadLevel(1);
                 Controller.EndGame(false);
            }
        }

        public  void MoveLeft()
        {
            animator.speed = 1;
            nextPosition = thisGameObject.transform.position;
            nextPosition.x -= (float)speed / 200;
            if (thisGameObject.transform.localScale.x > 0)
            {
                rotateVector.x = -(thisGameObject.transform.localScale.x);
                rotateVector.y = thisGameObject.transform.localScale.y;
            }
            else
            {
                rotateVector.x = thisGameObject.transform.localScale.x;
                rotateVector.y = thisGameObject.transform.localScale.y;
            }
            thisGameObject.transform.localScale = rotateVector;
            thisGameObject.transform.position = nextPosition;
        }
        public void MoveTop()
        {
            animator.speed = 1;

            nextPosition = thisGameObject.transform.position;

            nextPosition.y += (float)speed / 200;
            thisGameObject.transform.position = nextPosition;

        }
        public void MoveBot()
        {
            animator.speed = 1;

            nextPosition = thisGameObject.transform.position;

            nextPosition.y -= (float)speed / 200;
            thisGameObject.transform.position = nextPosition;
        }
        public void MoveRight()
        {
            animator.speed = 1;
            nextPosition = thisGameObject.transform.position;

            nextPosition.x += (float)speed / 200;
            if (thisGameObject.transform.localScale.x < 0)
            {
                rotateVector.x = -(thisGameObject.transform.localScale.x);
                rotateVector.y = thisGameObject.transform.localScale.y;
            }
            else
            {
                rotateVector.x = thisGameObject.transform.localScale.x;
                rotateVector.y = thisGameObject.transform.localScale.y;
            }
            thisGameObject.transform.localScale = rotateVector;
            thisGameObject.transform.position = nextPosition;
        }
    }
}