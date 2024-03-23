using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 100f; // Ajuste conforme necessário
    private Vector3 shootDirection;

    void Start()
    {
        Destroy(gameObject,10f);
    }

    // Função para definir a direção do disparo
    public void SetDirection(Vector3 direction, float speedModifier)
    {
        shootDirection = direction.normalized;
        //speed *= speedModifier;
    }

    void Update()
    {
        // Move o projétil
        transform.position += shootDirection * speed * Time.deltaTime;
    }

    // Destruir o projétil após algum tempo ou ao colidir
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // Destrua o projétil ao colidir com algo
    }
}
