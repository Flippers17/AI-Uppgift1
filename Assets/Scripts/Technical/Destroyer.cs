using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    private bool _destroyOnAwake = false;

    [SerializeField]
    private float _destroyTimer = 0.0f;


    [SerializeField]
    private GameObject _gameObjectToSpawn;


    private void Awake()
    {
        if(_destroyOnAwake)
            DoDestroy();
    }


    public void DoDestroy()
    {
        if( _gameObjectToSpawn != null)
        {
            Instantiate(_gameObjectToSpawn, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, _destroyTimer);
    }
}
