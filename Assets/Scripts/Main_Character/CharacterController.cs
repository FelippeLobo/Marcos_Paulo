using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float maxHealth;
    private float actualHealth;
    private int dorFlexNum;
    public WeaponController[] inventoryWeapons;
    private WeaponController currentWeapon;
    void Start()
    {
        maxHealth = 100;
        actualHealth = maxHealth;
        dorFlexNum = 0;
        currentWeapon = inventoryWeapons[0];
        ActivateCurrentWeaponModel();
    }

    // Update is called once per framevoid Update()
    void Update()
    {
        for (int i = 0; i < inventoryWeapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
            }
        }

        // Troca de armas pela roda do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            int nextWeaponIndex = (dorFlexNum + Mathf.RoundToInt(scroll)) % inventoryWeapons.Length;
            if (nextWeaponIndex < 0)
                nextWeaponIndex += inventoryWeapons.Length;
            SwitchWeapon(nextWeaponIndex);
        }
    }

    void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < inventoryWeapons.Length)
        {
            // Desativa o modelo visual da arma atual
            DeactivateCurrentWeaponModel();

            // Atualiza a arma atual
            currentWeapon = inventoryWeapons[weaponIndex];

            // Ativa o modelo visual da nova arma atual
            ActivateCurrentWeaponModel();
        }
    }

    // Método para ativar o modelo visual da arma atual
    void ActivateCurrentWeaponModel()
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(true);
        }
    }

    // Método para desativar o modelo visual da arma atual
    void DeactivateCurrentWeaponModel()
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }
    }
    // void TrocarParaProximaArma()
    // {
    //     // Encontra o índice da arma atual
    //     int currentIndex = Array.IndexOf(inventoryWeapons, currentWeapon);
    //     // Calcula o próximo índice de arma (cicla para o início se atingir o fim)
    //     int nextIndex = (currentIndex + 1) % inventoryWeapons.Length;
    //     // Define a arma atual como a próxima arma
    //     currentWeapon = inventoryWeapons[nextIndex];
    //     Debug.Log("Arma atual: " + currentWeapon.weaponName); // Apenas para debug, você pode remover isso
    // }

    // void TrocarParaArmaAnterior()
    // {
    //     // Encontra o índice da arma atual
    //     int currentIndex = Array.IndexOf(inventoryWeapons, currentWeapon);
    //     // Calcula o índice da arma anterior (cicla para o final se atingir o início)
    //     int previousIndex = (currentIndex - 1 + inventoryWeapons.Length) % inventoryWeapons.Length;
    //     // Define a arma atual como a arma anterior
    //     currentWeapon = inventoryWeapons[previousIndex];
    //     Debug.Log("Arma atual: " + currentWeapon.weaponName); // Apenas para debug, você pode remover isso
    // }

}
