using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IKillboxable
{
    public void Kill();
}

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
            if(other.TryGetComponent(out IKillboxable killboxable))
            {
                killboxable.Kill();
            }
            else
               Destroy(other.gameObject);
        }
    }
}
