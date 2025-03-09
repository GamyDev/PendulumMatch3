using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTC.Pool;

public class ParticleDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        NightPool.Despawn(gameObject, 2);
    }

   
}
