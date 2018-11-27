using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using MazeGenerator;
using Assets.Scripts;

public class CreateMapScript : MonoBehaviour {

    public Tilemap wallMap;
    public Tilemap groundMap;
    public Tile tileWall;
    public Tile tileGround ;
    [Range (10, 300)]
     public int height;
    [Range (10,300)]
    public int width;
    Vector3Int vector;



    void Start () {
        Controller.mazeHandler = new MazeHandler(height, width);
        Controller.height = height;
        Controller.width = width;
        lock (Controller.mazeHandler)
        {
            Controller.mazeHandler.StartGenerate();
        }
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                vector = new Vector3Int(i - height / 2, j - width / 2, 0);
                if (Controller.mazeHandler.maze.map[i, j] == (int)CellType.Pass)
                {
                    groundMap.SetTile(vector, tileGround);
                }
                else
                {
                    wallMap.SetTile(vector, tileWall);
                }
            }
        }
    }
	

}
