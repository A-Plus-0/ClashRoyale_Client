using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float max { get; private set; } = 10f;
    private float _current;
    [SerializeField] private HealthUI _ui;
    [SerializeField] private Destroy _destroy;
    private void Start()
    {
        _current = max;
        _ui.UpdateHealth(max, _current);
    }

    public void ApplyDamage(float value)
    {
        _current -= value;
        if (_current <= 0) { Destroy(); return; }

        Debug.Log($"Объект {name}: было - {_current + value}, стало {_current}");
        UpdateHP();
    }
    private void UpdateHP()
    {
        _ui.UpdateHealth(max, _current);
    }
    [ContextMenu("Destroy")]
    private void Destroy()
    {
        MapInfo.Instance.DeleteFromLists(this.gameObject.GetComponent<IHealth>());
        _destroy.StartDestroy(this);
    }
}

public interface IHealth
{
    Health health { get; }
}
