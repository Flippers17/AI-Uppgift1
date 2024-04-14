using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField]
    private bool _destroyNonPlayerObjects = true;

    [SerializeField]
    private LayerMask _nonPlayerMask;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.TeleportPlayerToCheckpoint();
        }
        else if((1 << other.gameObject.layer & _nonPlayerMask) != 0)
        {
            Destroy(other.gameObject);
        }
    }
}
