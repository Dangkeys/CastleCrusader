using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TreeCollision : MonoBehaviour
{
    [SerializeField] GameObject hitFX;
    private void OnParticleCollision(GameObject other)
    {
        //random hitSFX
        Instantiate(hitFX, transform.position, Quaternion.identity);
    }
}
