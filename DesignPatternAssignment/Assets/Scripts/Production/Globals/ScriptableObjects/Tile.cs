using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile", order = 1)]
public class Tile : ScriptableObject
{
    [SerializeField] private GameObject Prefab = null;
    [SerializeField] private TileType tileType = TileType.Path;
    [SerializeField] private bool Walkable = false;

    public GameObject getPrefab { get { return Prefab; } }
    public TileType getType { get { return tileType; } }
    public bool isWalkable { get { return Walkable; } }

}
