using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    static public class Controller
    {

        public static event EnemiesChanging AddCoin;
        public delegate void EnemiesChanging();


        static public MazeGenerator.MazeHandler mazeHandler;
        public static List<GameStat> stats = new List<GameStat>();

        static public int height { get; set; }
        static public int width { get; set; }
        static public int coin { get; private set; }
        static public int currentCoin { get; set; }
        static public string name { get; set; } = "";
        static public float enemySpeed { get; set; } 
        static public bool hunt { get; set; }
        static public float duration { get; set; }


        static public void InduceAddCoin()
        {
            coin++;
            AddCoin();
        }
        static public void EndGame(bool isEscape)
        {
            stats.Add(new GameStat(name, duration, isEscape, coin));
            foreach (GameStat stat in stats)
                Debug.Log(stat.date.ToString());
            DoSerialize();
            coin = 0;
            hunt = false;
            duration = 0;
        }
        static public void DoSerialize()
        {
            try
            {
                using (FileStream file = new FileStream("Savefile.xml", FileMode.Create))
                {
                    new XmlSerializer(typeof(List<GameStat>)).Serialize(file, stats);
                }
            }
            catch { throw new Exception("Отсутствует файл сохранений"); }
        }
        static public void DoDeserialize()
        {
            try
            {
                using (FileStream file = new FileStream("Savefile.xml", FileMode.Open))
                {
                    stats = new List<GameStat>();
                    stats = new XmlSerializer(typeof(List<GameStat>)).Deserialize(file) as List<GameStat>;
                    stats.Sort(Comparer<GameStat>.Create((x, y) =>
                           (x as GameStat).date.CompareTo(
                           (y as GameStat).date)));
                }
            }
            catch (FileNotFoundException) { }
        }
    }
}
