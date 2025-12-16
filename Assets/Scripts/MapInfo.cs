using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    #region SingletonOneScene
    public static MapInfo Instance { get; private set; }
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    #endregion

    [SerializeField] private List<Tower> _enemyTowers = new List<Tower>();
    [SerializeField] private List<Tower> _playerTowers = new List<Tower>();

    [SerializeField] private List<Unit> _enemyUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerUnits = new List<Unit>();

    private void Start()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        foreach (Unit unit in units)
        {
            if (unit.isEnemy)
            {
                if (_enemyUnits.Contains(unit) == false)
                    _enemyUnits.Add(unit);
            }
            else
            {
                if(_playerUnits.Contains(unit) == false)
                    _playerUnits.Add(unit);
            }
        }
    }

    public void DeleteFromLists(IHealth towerOrEnemy)
    {
        switch (towerOrEnemy)
        {
            case Tower tower:
                if (_enemyTowers.Remove(tower)) return;
                if (_playerTowers.Remove(tower)) return;
                break;
            case Unit unit:
                if (_enemyUnits.Remove(unit)) return;
                if (_playerUnits.Remove(unit)) return;
                break;
            default:
                Debug.LogWarning("MapInfo: неизвестный тип объекта");
                break;
        }
    }

    public bool TryGetNearestUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance)
    {
        List<Unit> units = enemy ? _enemyUnits : _playerUnits;
        unit = GetNearest(currentPosition, units, out distance);

        return unit;
    }

    public Tower GetNearestTower(in Vector3 currentPosition, bool enemy)
    {
        List<Tower> towers = enemy ? _enemyTowers : _playerTowers;
        return GetNearest(in currentPosition, towers, out float distance);
    }

    private T GetNearest<T>(in Vector3 currentPosition, List<T> objects, out float distance) where T : MonoBehaviour
    {
        distance = float.MaxValue;
        if (objects.Count <= 0) return null;

        distance = Vector3.Distance(currentPosition, objects[0].transform.position);
        T nearest = objects[0];

        for (int i = 0; i < objects.Count; i++)
        {
            float tempDistance = Vector3.Distance(currentPosition, objects[i].transform.position);
            if (tempDistance > distance) continue;

            distance = tempDistance;
            nearest = objects[i];
        }

        return nearest;
    }
}
