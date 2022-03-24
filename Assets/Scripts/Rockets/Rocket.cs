using System;
using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour
{
    public float speed = 100;

    public float lifeTime = 35f;

    public float leftTime = 0;

    private Rigidbody _rigidbody;

    [Inject]
    private ExplosionSpawner _explosionSpawner;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.up * speed;
    }


    private void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionStay(Collision c)
    {
        Debug.Log(c.gameObject.name);
        c.gameObject.SendMessageUpwards("Explode", SendMessageOptions.DontRequireReceiver);

        Explode();
    }

    public void Explode()
    {
        gameObject.SetActive(false);

        _explosionSpawner.Spawn(transform.position);
    }

    public class Factory : PlaceholderFactory<Rocket> { }
}