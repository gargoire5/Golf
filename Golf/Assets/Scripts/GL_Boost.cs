using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GL_Boost : MonoBehaviour
{
    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    // Update is called once per frame
    void Update()
    {
        if (_rigidbodies.Count != 0)
        {
            foreach (var b in _rigidbodies)
            {
                b.AddForce(0, 0, 0.1f, ForceMode.Impulse);
            }
        }
    }

    public void AddRigidbody(Rigidbody rbToAdd)
    {
        _rigidbodies.Add(rbToAdd);
    }

    public void RemoveRigidbody(Rigidbody rbToAdd)
    {
        _rigidbodies.Remove(rbToAdd);
    }
}
