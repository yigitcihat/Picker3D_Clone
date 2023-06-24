using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    // [NotNull]
    // private Rigidbody Rb
    // {
    //     get => GetComponent<Rigidbody>();
    //     set
    //     {
    //         if (value == null) throw new ArgumentNullException(nameof(value));
    //         Rb = value ? value : throw new ArgumentNullException(nameof(value));
    //     }
    // }
    //
    // private IEnumerator Start()
    // {
    //     Rb.velocity=Vector3.zero;
    //     Rb.mass=1000;
    //     yield return new WaitForSeconds(0.2f);
    //     Rb.velocity=Vector3.zero;
    //     Rb.mass=1;
    //     // transform.SetParent(PoolingSystem.Instance.transform);
    // }
    
}
