using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponentInParent<Chunk>() != null)
        {
            Chunk chunk = other.GetComponentInParent<Chunk>();
            chunk.TryUnlock();
        }
    }
}
