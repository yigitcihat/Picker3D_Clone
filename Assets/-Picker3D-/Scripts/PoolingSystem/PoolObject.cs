using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [NotNull]
    private Rigidbody Rb
    {
        get => GetComponent<Rigidbody>();
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Rb = value ? value : throw new ArgumentNullException(nameof(value));
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        // Rb.isKinematic = false;
        // transform.SetParent(PoolingSystem.Instance.transform);
    }
    
}
