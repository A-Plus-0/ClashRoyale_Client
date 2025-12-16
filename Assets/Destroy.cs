using System.Collections;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float _delay = 10f;
    [SerializeField] private float _size = 3f;
    [SerializeField] private GameObject _particalSystem;

    private bool _isStarted = false;

    public void StartDestroy(Health dedestroyedObject)
    {
        if (_isStarted) return;
        
        _particalSystem.SetActive(true);
        _particalSystem.transform.parent = null;
        _particalSystem.transform.localScale = Vector3.one * _size;
        _particalSystem.GetComponent<ParticleSystem>().Play();
        Destroy(dedestroyedObject.gameObject);
        _isStarted = true;
        StartCoroutine(DestroyAfterDelay(_particalSystem));
    }

    private IEnumerator DestroyAfterDelay(GameObject dedestroyedEffect)
    {
        yield return new WaitForSeconds(_delay);
        Destroy(dedestroyedEffect);
    }
}
