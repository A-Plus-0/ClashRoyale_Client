using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filedImage;
    [SerializeField] private float _defaultWidth;
    
    private void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }

    private void OnValidate()
    {
        _defaultWidth = _filedImage.sizeDelta.x;
    }

    public void UpdateHealth(float max, float current)
    {
        float percent = current / max;

        _filedImage.sizeDelta = new Vector2(_defaultWidth * percent, _filedImage.sizeDelta.y);
    }
}
