using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesSwitcher : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void ShowGameScenes()
    {
        Application.LoadLevel(0);
    }
    public void ShowStartScenes()
    {
        Application.LoadLevel(1);
    }
    public void ShowRcordsScenes()
    {
        Application.LoadLevel(2);
    }
}