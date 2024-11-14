using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 50f; // D�n�� h�z�, iste�e ba�l� olarak ayarlanabilir

    void Update()
    {
       
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
