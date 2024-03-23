using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private int currentAmmo; // munição atual
    private GameObject weaponModel;// Modelo da arma
    private float fireTimer; // Temporizador para controlar a cadência de tiro
    public float fireRate; // Cadência de tiro em segundos
    public string name; // nome da arma
    public int _id; // identificador da arma
    public float damagePerBullet; // dano da arma
    public int maxAmmo; // maximo de munição que se pode obter dessa arma
    public int magazineSize; // tamanho do cartucho da arma
    public GameObject bulletPrefab; // Prefab da bala
    public Transform shootPoint; // O ponto de onde as balas serão disparadas

    void Start()
    {
        currentAmmo = magazineSize; // Inicia com o cartucho cheio
        fireTimer = 0f;
    }

    // public void Arma(int id, string name, int maxAmmo, float damagePerBullet)
    // {
    //     _id = id;
    //     name = name;
    //     maxAmmo = maxAmmo;
    //     currentAmmo = maxAmmo; // Começa com a munição cheia
    //     damagePerBullet = damagePerBullet;
    // }



    void Update()
    {

        // Atualiza o temporizador de cadência de tiro
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime; // Reduz o temporizador com base no tempo decorrido desde o último quadro
        }

        // Verifica se o jogador está segurando o botão esquerdo do mouse para atirar
        if (_id == 1 || _id == 2)
        {
            if (Input.GetMouseButton(0))
            {
                // Chama o método de atirar enquanto o botão do mouse estiver sendo pressionado
                Shoot();
            }
        }
        else
        {
            // Outros tipos de armas podem usar o clique único para atirar
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        // Recarregar com a tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Recarregando...");
            Reload(magazineSize);
        }
    }

    void Shoot()
    {
        if (fireTimer <= 0f && currentAmmo > 0f) // Verifica se já passou tempo suficiente desde o último disparo
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
            projectile.SetDirection(shootDirection, fireRate);

            currentAmmo--;
            //local para atualizar interface visual das informações da arma \/
            Debug.Log(currentAmmo + "//" + magazineSize + " || " + maxAmmo);

            if (currentAmmo <= 0)
            {
                Debug.Log("Recarregando...");
                Reload(magazineSize);
            }

            // Reinicia o temporizador de cadência de tiro
            fireTimer = fireRate;
        }
    }

    //recarrega a arma
    void Reload(int ammo)
    {
        if (maxAmmo > ammo)
        {
            if (_id != 0)
            {
                maxAmmo -= ammo;
            }
            currentAmmo += ammo;
        }
        else if (maxAmmo > 0 && maxAmmo <= magazineSize)
        {
            currentAmmo += ammo;
            maxAmmo = 0;
        }
        else if (maxAmmo <= 0)
        {
            Debug.Log("Sem munição...");
        }
        //carrega a arma, caso a arma nao seja a pistola tambem reduz a munição total

        currentAmmo = Mathf.Clamp(currentAmmo, 0, magazineSize);
        // Adicione efeitos de recarga se necessário
        Debug.Log("Recarregado!");
    }


    //geters e seters
    public int GetID()
    {
        return _id;
    }

    public string GetName()
    {
        return name;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public int GetActualAmmo()
    {
        return currentAmmo;
    }

    public float GetDamagePerBullet()
    {
        return damagePerBullet;
    }

    // Método para definir o modelo da arma
    public void SetWeaponModel(GameObject model)
    {
        weaponModel = model;
    }

    // Método para obter o modelo da arma
    public GameObject GetWeaponModel()
    {
        return weaponModel;
    }
}
