using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 100;
    
    private void Update()
    {
        var t = transform;  
        
        t.position += t.up * speed * Time.deltaTime;
    }
}