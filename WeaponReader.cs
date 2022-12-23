using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReader : MonoBehaviour
{
    public static WeaponReader instance;
    
    public static WeaponReader Instance {get; private set;}
    public string nameW;
    public int damage;

    private void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
    }

    public TextAsset textJSON;

    [System.Serializable]
    public class Weapon
    {
        public string name;
        public int damage;
    }
    
    [System.Serializable]
    public class WeaponList
    {
        public Weapon[] weapon;      
    }

    public WeaponList weaponList  = new WeaponList();

    // Start is called before the first frame update
    void Start()
    {
        weaponList = JsonUtility.FromJson<WeaponList>(textJSON.text);
        setWeaponList(weaponList);
    }

    public void setWeaponList(WeaponList weaponListIn)
    {
        this.weaponList = weaponListIn;
    }

    public void setVariables(string nameIn)
    {
        Debug.Log("WeaponReader" + weaponList.weapon.Length);
        for(int i = 0; i < weaponList.weapon.Length; i++)
        {
            if(weaponList.weapon[i].name.Equals(nameIn))
            {
                damage = weaponList.weapon[i].damage;
                nameW = weaponList.weapon[i].name;
            }
        }

    }

    public int getDamage(string nameIn)
    {
        for(int i = 0; i < weaponList.weapon.Length; i++)
        {
            if(weaponList.weapon[i].name.Equals(nameIn))
            {
                return weaponList.weapon[i].damage;
            }
        }
        return 0;
    }
}

