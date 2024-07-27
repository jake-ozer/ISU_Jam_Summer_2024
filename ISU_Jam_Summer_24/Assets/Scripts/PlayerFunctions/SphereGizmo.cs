using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGizmo : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
