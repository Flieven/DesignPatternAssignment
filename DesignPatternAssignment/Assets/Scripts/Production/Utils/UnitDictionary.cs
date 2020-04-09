using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDictionary : MonoBehaviour
{
    [SerializeField] private Unit[] UnitArray = null;

    private Dictionary<UnitType, Unit> unitDictionary = new Dictionary<UnitType, Unit>();

    public void SetupDictionary()
    {
        foreach (Unit unit in UnitArray)
        {
            unitDictionary.Add(unit.getType, unit);
        }
    }

    public Unit getUnit(UnitType unit) { return unitDictionary[unit]; }

}
