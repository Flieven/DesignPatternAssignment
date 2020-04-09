using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private float spawnTimer = 1.0f;
    private ObjectPool StandardPool;
    private ObjectPool HeavyPool;

    [SerializeField] private List<GameObject> activeUnits = new List<GameObject>();

    [SerializeField] private int totalNumStandardEnemies = 0;
    [SerializeField] private int totalNumHeavyEnemies = 0;
    [SerializeField] private int totalNumEnemies = 0;

    private UnitDictionary unitDictionary = null;

    private List<Vector2Int> currentPath;

    public void RunWave(int standardEnemies, int heavyEnemies)
    {
        CancelInvoke();

        if(StandardPool == null) { StandardPool = new ObjectPool(1, unitDictionary.getUnit(UnitMethods.TypeById[1]).getPrefab, 1, this.transform); }
        if(HeavyPool == null) { HeavyPool = new ObjectPool(1, unitDictionary.getUnit(UnitMethods.TypeById[0]).getPrefab, 1, this.transform); }

        totalNumStandardEnemies = standardEnemies;
        totalNumHeavyEnemies = heavyEnemies;
        totalNumEnemies = standardEnemies + heavyEnemies;

        InvokeRepeating(nameof(CheckEnemySpawning), 0, spawnTimer);
    }

    public void CheckEnemySpawning()
    {
        if(totalNumEnemies > 0)
        {
            int RandomEnemy = Random.Range(0, 2);

            switch (RandomEnemy)
            {
                case 0:
                    if (totalNumStandardEnemies > 0) { SpawnEnemy(0); }
                    else { SpawnEnemy(1); };
                    break;
                case 1:
                    if (totalNumHeavyEnemies > 0) { SpawnEnemy(1); }
                    else { SpawnEnemy(0); };
                    break;
            }
        }
    }

    private void SpawnEnemy(int type)
    {
        switch(type)
        {
            case 0:
                Debug.Log("Standard stack size: " + StandardPool.stackSize);
                totalNumStandardEnemies--;
                GameObject spawnedStandard = StandardPool.Rent(false);
                activeUnits.Add(spawnedStandard);
                UnitBehaviour standardBehaviour = spawnedStandard.GetComponent<UnitBehaviour>();
                standardBehaviour.ResetUnit(currentPath);
                spawnedStandard.SetActive(true);
                break;

            case 1:
                Debug.Log("Heavy stack size: " + HeavyPool.stackSize);
                totalNumHeavyEnemies--;
                GameObject spawnedHeavy = HeavyPool.Rent(false);
                activeUnits.Add(spawnedHeavy);
                UnitBehaviour heavyBehaviour = spawnedHeavy.GetComponent<UnitBehaviour>();
                heavyBehaviour.ResetUnit(currentPath);
                spawnedHeavy.SetActive(true);
                break;
        }

        totalNumEnemies--;
    }

    public void GameOver()
    {
        CancelInvoke();
        foreach (GameObject unit in activeUnits)
        {
            unit.GetComponent<UnitBehaviour>().CancelInvoke();
        }
        activeUnits.Clear();
    }

    public void removeFromActiveList(GameObject obj) 
    { 
        if(activeUnits.Contains(obj)) { activeUnits.Remove(obj); }

        if( totalNumEnemies == 0 && activeUnits.Count == 0) { transform.parent.GetComponent<WorldManager>().IncrementWave(); }
    }

    public void FinishedPath(int Damage)
    { transform.parent.GetComponent<WorldManager>().DamagePlayer(Damage); }

    public UnitDictionary setUnitDictionary { set { unitDictionary = value; } }

    public List<Vector2Int> setPath {  set { currentPath = value; } }
}
