using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private int _id;
    private string _name;
    private int _maxAmmo;
    private int _actualAmmo;
    private float damagePerBullet;

    private GameObject weaponModel;

    public Arma(int id, string name, int maxAmmo, float damagePerBullet)
    {
        _id = id;
        _name = name;
        _maxAmmo = maxAmmo;
        _actualAmmo = maxAmmo; // Começa com a munição cheia
        _damagePerBullet = damagePerBullet;
    }

    public void Atirar()
    {
        if (_actualAmmo > 0)
        {
            // Implemente a lógica para disparar a arma aqui
            _actualAmmo--;
        }
        else
        {
            // Implemente a lógica para recarregar ou lidar com a falta de munição
            Debug.Log("Sem munição!");
        }
    }

    public void Recarregar(int ammo)
    {
        _actualAmmo += ammo;
        _actualAmmo = Mathf.Clamp(_actualAmmo, 0, _maxAmmo); // Garante que a munição não ultrapasse o máximo
    }
    // Métodos para acessar informações da arma
    public int GetID()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }

    public int GetActualAmmo()
    {
        return _actualAmmo;
    }

    public float GetDamagePerBullet()
    {
        return _damagePerBullet;
    }

    // Método para definir o modelo da arma
    public void SetWeaponModel(GameObject model)
    {
        _weaponModel = model;
    }

    // Método para obter o modelo da arma
    public GameObject GetWeaponModel()
    {
        return _weaponModel;
    }
}
