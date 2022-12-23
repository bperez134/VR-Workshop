using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStats : MonoBehaviour
{
    // Start is called before the first frame update

    public string monsterName;
    public int lvl;
    public int health;
    public int attack;
    public int exp;

    //WeaponStats aggressor;
    public GameObject Canvas;
    public Image healthColor;
    public Slider healthBar;
    
    void Start()
    {
        monsterName = this.gameObject.name.Substring(0, this.gameObject.name.Length - 7);
        //monsterName = this.name;
        MonsterReader.Instance.setVariables(monsterName);

        lvl = MonsterReader.Instance.lvl;
        health = MonsterReader.Instance.health;
        attack = MonsterReader.Instance.attack;
        exp = MonsterReader.Instance.exp;
        healthBar.maxValue = health;
        healthBar.value = health;
        //Debug.Log(monsterName);
    }

    void MStatRetirve(string nameM, int lvlM, int healthM, int attackM, int expM)
    {
        //string name = this.gameObject.name;
        name = nameM;
        lvl = lvlM;
        health = healthM;
        attack = attackM;
        exp = expM;
    }

    void OnCollisionEnter(Collision other)
    {
        
        if(other.gameObject.layer == 12)
        {
            string item = other.gameObject.name.Substring(0, other.gameObject.name.Length - 7);
            //Debug.Log(item);
            Destroy(other.gameObject);
            healthBar.value -= WeaponReader.Instance.getDamage(item);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(healthBar.value <= health/2)
        {
            float g = (healthBar.value) / health * 255;
            healthColor.color = new Color32(255, (byte)g, 0, 255);
        }
        else
        {
            float r =(((health - healthBar.value) + (health / 2))/ health * 255);
            healthColor.color = new Color32((byte)r , 255, 0, 255);
        }
    }


}
