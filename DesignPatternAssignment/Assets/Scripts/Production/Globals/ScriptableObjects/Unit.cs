using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit", order = 2)]
public class Unit : ScriptableObject
{
    [SerializeField] private UnitType type = UnitType.Standard;
    [SerializeField] private GameObject Prefab = null;
    [SerializeField] private int maxUnitHealth = 10;
    [SerializeField] private int Damage = 1;
    [SerializeField] private float movementSpeed = 0;


    public UnitType getType { get { return type; } }
    public GameObject getPrefab { get { return Prefab; } }
    public int getHealth {  get { return maxUnitHealth; } }
    public int getDamage {  get { return Damage; } }
    public float getSpeed {  get { return movementSpeed; } }

}
