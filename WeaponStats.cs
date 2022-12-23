using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int damage;
    public string weaponName;

    void Start()
    {   
        //name of the weapon below 
        //removes the "(Clone)" part of the Game Object name
        weaponName = this.gameObject.name.Substring(0, this.gameObject.name.Length - 7);

        WeaponReader.Instance.setVariables(weaponName);
        damage = WeaponReader.Instance.damage;
        //Debug.Log(damage);
    }

    public void WStatRetrieve (int damageW, string nameW)
    {
        //damage.Add(damageW);
        weaponName = nameW;
        //Debug.Log(weaponName + damage);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
