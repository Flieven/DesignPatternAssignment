using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDictionary : MonoBehaviour
{
    [SerializeField] private Tile[] TileArray = null;

    private Dictionary<TileType, Tile> tileDictionary = new Dictionary<TileType, Tile>();

    public void SetupDictionary()
    {
        foreach(Tile tile in TileArray)
        {
            tileDictionary.Add(tile.getType, tile);
        }
    }

    public Tile getTile(TileType tile) { return tileDictionary[tile]; }

}
