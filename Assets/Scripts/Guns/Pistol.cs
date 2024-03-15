using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab da bala
    public Transform shootPoint; // O ponto de onde as balas serão disparadas
    public int maxAmmo = 13; // Máximo de munição antes de recarregar
    private int currentAmmo; // Munição atual

    void Start()
    {
        currentAmmo = maxAmmo; // Inicia com a munição máxima
    }

    void Update()
    {
        // Disparo com clique do mouse ou toque na tela
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        
        // Recarregar com a tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        // Defina a máscara de camada para ignorar a camada do jogador. Suponha que o jogador esteja na camada 2, Ignore Raycast.
        int layerMask = 1 << 2;
        layerMask = ~layerMask;

        Ray rayFromAim = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        // Use a máscara de camada como um argumento adicional para Physics.Raycast
        if (Physics.Raycast(rayFromAim, out hit, Mathf.Infinity, layerMask))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = rayFromAim.GetPoint(1000);
        }

        Vector3 shootDirection = (targetPoint - shootPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(shootDirection);

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, rotation);
        Projectile projectile = bullet.GetComponent<Projectile>();
        projectile.SetDirection(shootDirection);

        currentAmmo--;

        if (currentAmmo <= 0)
        {
            Debug.Log("Recarregando...");
            Reload();
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        // Adicione efeitos de recarga se necessário
        Debug.Log("Recarregado!");
    }
}
