using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour, iDamageable
{
    [SerializeField] private Unit unitData = null;

    [SerializeField] private List<Vector2Int> Path;
    [SerializeField] private int currentPos = 0;

    [SerializeField] private int Health = 0;

    public void ResetUnit(List<Vector2Int> newPath)
    {
        CancelInvoke();
        Health = unitData.getHealth;
        Path = newPath;
        currentPos = 0;
        transform.position = new Vector3(Path[currentPos].x* (int)transform.parent.GetComponentInParent<WorldManager>().getDisplacement, transform.position.y, Path[currentPos].y* (int)transform.parent.GetComponentInParent<WorldManager>().getDisplacement);
        InvokeRepeating(nameof(MoveOnPath), 0, unitData.getSpeed);
    }

    private void MoveOnPath()
    {
        if (currentPos <= Path.Count - 2)
        {
            currentPos++;
            transform.position = new Vector3(Path[currentPos].x * 2, transform.position.y, Path[currentPos].y * 2);
        }
        else 
        {
            transform.GetComponentInParent<UnitManager>().FinishedPath(unitData.getDamage);
            DisableUnit();
        }
    }

    private void DisableUnit()
    {
        transform.parent.GetComponent<UnitManager>().removeFromActiveList(this.gameObject);
        gameObject.SetActive(false);
        CancelInvoke();
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0) { DisableUnit(); }
    }

    public Unit getUnit { get { return unitData; } }
}
