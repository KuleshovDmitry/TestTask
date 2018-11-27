using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

namespace Assets.Scripts.MovingScripts
{
    public class EnemyMoving : ObjectMoving
    {
        class Position
        {
            public int x { get; set; }
            public int y {get;set;}
            public int step { get; set; }
            public Position (int x, int y, int step)
            {
                this.x = x;//ex(?)
                this.y = y;
                this.step = step;
            }
        }
        List<Position> passList = new List<Position>();
        System.Random random = new System.Random();
        int counter;
        public GameObject thisGameObject;
        public Animator animator;
        private Transform target;
        private float prevX=0, prevY=0;

        public void UpdatePosition()
        {
            x = (int)(thisGameObject.transform.position.x * 2 * 0.88 + Controller.height / 2);
            y = (int)(thisGameObject.transform.position.y * 2 * 0.88 + Controller.width / 2);
        }
        protected int getX(Transform gameObject)
        {
            return (int)(gameObject.transform.position.x * 2 * 0.88 + Controller.height / 2);
        }
        protected int getY(Transform gameObject)
        {
            return (int)(gameObject.transform.position.y * 2 * 0.88 + Controller.width / 2);
        }


        public void Start()
        {
            System.Random random = new System.Random();
            int startX, startY;
            do
            {
                startX = random.Next(1, Controller.height /2);
                startY = random.Next(1, Controller.width /2);
            } while (Controller.mazeHandler.maze.map[startX, startY] != 1);

            transform.position = new Vector3((startX - Controller.height / 2) / 0.88f / 2f + 0.35f,
                (startY - Controller.width / 2) / 0.88f / 2f + 0.3f, 0);
            RandomMove();
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        public void Update()
        {
            if (Controller.hunt) MoveToPlayer();//переключение на преследование игрока
            animator.speed = 0;
            if (passList.Count != 0)//Если в списке клеток до цели есть клетки
            {

                animator.speed = 1;
                float targX = (passList[0].x - Controller.height / 2) / 0.88f / 2f + 0.35f;//переводим координаты и смещаем оторажение
                float targY = (passList[0].y - Controller.width / 2) / 0.88f / 2f + 0.3f;//т.к. иначе противник будет отображаться на углу клетки
                
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targX, targY,1), speed * Time.deltaTime);//движемся к этой клетке


                if (transform.position.x<targX// поворот по движению
                    &&thisGameObject.transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3 ( 
                        -thisGameObject.transform.localScale.x, 
                        thisGameObject.transform.localScale.y, 
                        thisGameObject.transform.localScale.z);
                }
                else if (transform.position.x > targX// поворот по движению
                    &&  thisGameObject.transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(
                        -thisGameObject.transform.localScale.x,
                        thisGameObject.transform.localScale.y,
                        thisGameObject.transform.localScale.z);
                }
                if (Math.Round((float)prevX, 2) == Math.Round((float)transform.position.x, 2)
                        && Math.Round((float)prevY, 2) == Math.Round((float)transform.position.y, 2))
                { RandomMove(); }// если за такт противник не переместился- начинаем движение в другом направлнии

                else
                {
                    prevX = (float)Math.Round((float)transform.position.x, 2);
                    prevY = (float)Math.Round((float)transform.position.y, 2);
                }// иначе сохраняем текущие координаты. Это добавлено, т.к. из-за дробных частей противнк иногда не мог дойти до цели

                if (Math.Round((double)transform.position.x, 1) == Math.Round((double)targX, 1)
                    && Math.Round((double)transform.position.y, 1) == Math.Round((double)targY, 1))
                    passList.Remove(passList[0]);//если дошли до текущей целевой клетки-удаляем ее и начинаем работать с следующей
            }
            else RandomMove();// если дошли до цели-начать завново
        }


        public void FindPass(int startX, int startY, int targetX, int targetY)//работает по "волновому алгоритму"
        {
            passList = new List<Position>();//создаем новвый лист пути
            bool add = true;
            int[,] buffMap = new int[Controller.height, Controller.width];
            int i, j, step = 0;
            for (i = 0; i < Controller.height; i++)//инициализируем буферную карту
            {
                for (j = 0; j < Controller.width; j++)
                   {
                    if (Controller.mazeHandler.maze.map[i, j] == 0)
                        buffMap[i, j] = -2;
                    else buffMap[i, j] = -1;
                }
            }
            buffMap[targetX, targetY] = 0;
            while (add)
                {
                    add = false;
                    for (j = 0; j < Controller.width; j++)
                        for (i = 0; i < Controller.height; i++)
                        {
                            if (buffMap[i, j] == step)
                            {
                                if (i - 1 >= 0 && buffMap[i - 1, j] != -2 && buffMap[i - 1, j] == -1)
                                    buffMap[i - 1, j] = step + 1;
                                if (j - 1 >= 0 && buffMap[i, j - 1] != -2 && buffMap[i, j - 1] == -1)
                                    buffMap[i, j - 1] = step + 1;
                                if (i + 1 < Controller.height && buffMap[i + 1, j] != -2 && buffMap[i + 1, j] == -1)
                                    buffMap[i + 1, j] = step + 1;
                                if (j + 1 < Controller.width && buffMap[i, j + 1] != -2 && buffMap[i, j + 1] == -1)
                                    buffMap[i, j + 1] = step + 1;
                            }
                        }
                    step++;
                    add = true;
                    if (buffMap[startX, startY] != -1)
                        add = false;
                }//пока не дойдем до цели- соседним клеткам назначаем значение ткущего шага+1
            UpdatePass(buffMap, startX, startY, step);

        }
        public void UpdatePass(int[,] buffMap, int i, int j, int step)//поиск пути: проходим по карте, инкрементируя переменную шага и добавляя клетку в карту пути
        {
            if (step == 0) return;
            step--;
            if (i - 1 >= 0 && buffMap[i - 1, j] == step)
            {
                passList.Add(new Position(i - 1, j, step));
                UpdatePass(buffMap, i - 1, j, step);
            }
            else if (j - 1 >= 0 && buffMap[i, j - 1] == step)
            {
                passList.Add(new Position(i, j - 1, step));
                UpdatePass(buffMap, i, j - 1, step);
            }
            else if (i + 1 < Controller.height && buffMap[i + 1, j] == step)
            {
                passList.Add(new Position(i + 1, j, step));
                UpdatePass(buffMap, i + 1, j, step);
            }
            else if (j + 1 < Controller.width && buffMap[i, j + 1] == step)
            {
                passList.Add(new Position(i, j + 1, step));
                UpdatePass(buffMap, i, j + 1, step);
            }
        }


        public void MoveToPlayer()
        {
            passList = new List<Position>();
            UpdatePosition();
            FindPass(x, y, getX(target), getY(target));
        }
        public void RandomMove()
        {
            passList = new List<Position>();
            System.Random random = new System.Random();
            int targX, targY;
            do
            {
                targX = random.Next(1, Controller.height - 2);
                targY = random.Next(1, Controller.width - 2);
            } while (Controller.mazeHandler.maze.map[targX, targY] != 1);
            UpdatePosition();

            FindPass(x, y, targX, targY);
        }
    }
}