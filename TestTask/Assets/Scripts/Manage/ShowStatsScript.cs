using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Assets.Scripts
{
    public class ShowStatsScript : MonoBehaviour
    {

        public GameObject statsPrefab;
        
        void Start()
        {
            Controller.DoDeserialize();
            foreach (GameStat stat in Controller.stats)
            {
                GameObject go = (GameObject)Instantiate(statsPrefab);
                go.transform.SetParent(this.transform);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(go.transform.position.x, go.transform.position.y, 0);
                go.transform.Find("Username").GetComponent<Text>().text = stat.name;
                go.transform.Find("Time").GetComponent<Text>().text = stat.duration.ToString();
                go.transform.Find("Coin").GetComponent<Text>().text = stat.coin.ToString();
                go.transform.Find("Data").GetComponent<Text>().text = stat.date.ToString();
                go.transform.Find("EndReason").GetComponent<Text>().text = stat.endReason.ToString();
            }
        }
        
        void Update()
        {
        }
    }
}