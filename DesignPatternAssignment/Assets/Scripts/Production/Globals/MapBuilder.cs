using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    private TileDictionary tileDictionary;

    private int[,] map;
    private List<Vector2Int> Walkables = new List<Vector2Int>();
    private Vector2Int StartVector;
    private Vector2Int EndVector;

    private GameObject Towers, Paths, Obstacles, Specials;

    public void BuildMap()
    {
        Setup();
        Build();
    }

    private void Build()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Tile tempTile = tileDictionary.getTile(TileMethods.TypeById[map[x, y]]);
                GameObject tempObj = Instantiate(tempTile.getPrefab, new Vector3Int(x * 2, 0, y * 2), Quaternion.Euler(0, 90, 0));

                if(tempTile.getType == TileType.Start) { StartVector = new Vector2Int(x, y); }
                else if(tempTile.getType == TileType.End) { EndVector = new Vector2Int(x,y); }
                
                if(tempTile.isWalkable) { Walkables.Add(new Vector2Int(x, y)); }
                //Debug.Log(tempTile.name);

                switch(tempTile.getType)
                {
                    case TileType.End: tempObj.transform.parent = Specials.transform; break;
                    case TileType.Obstacle: tempObj.transform.parent = Obstacles.transform; break;
                    case TileType.Path: tempObj.transform.parent = Paths.transform; break;
                    case TileType.Start: tempObj.transform.parent = Specials.transform; break;
                    case TileType.TowerOne: tempObj.transform.parent = Towers.transform; break;
                    case TileType.TowerTwo: tempObj.transform.parent = Towers.transform; break;
                }

            }
        }
        //Debug.Log("Walkable Tiles: " + Walkables.Count);
    }

    private void Setup()
    {
        Towers = new GameObject("Towers");
        Towers.transform.parent = transform;

        Paths = new GameObject("Paths");
        Paths.transform.parent = transform;

        Obstacles = new GameObject("Obstacles");
        Obstacles.transform.parent = transform;

        Specials = new GameObject("Specials");
        Specials.transform.parent = transform;
    }

    public int[,] setMap { set { map = value; } }

    public Vector2Int getStart {  get { return StartVector; } }
    public Vector2Int getEnd {  get { return EndVector; } }
    public List<Vector2Int> getPath { get { return Walkables; } }

    public TileDictionary setTileDictionary {  set { tileDictionary = value; } }
}
