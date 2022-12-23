using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReader : MonoBehaviour
{
    public static MonsterReader instance;
    
    public static MonsterReader Instance {get; private set;}
    public string nameM;
    public int lvl;
    public int health;
    public int attack;
    public int exp;

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
    //public MonsterReader monsterStats = new MonsterReader();

    [System.Serializable]
    public class Monster
    {
        public string name;
        public int lvl;
        public int health;
        public int attack;
        public int exp;
    }
    
    [System.Serializable]
    public class MonsterList
    {
        public Monster[] monster;
    }

    public MonsterList monsterList  = new MonsterList();

    // Start is called before the first frame update
    void Start()
    {
        monsterList = JsonUtility.FromJson<MonsterList>(textJSON.text);
        setMonsterList(monsterList);

    }

    public void setMonsterList(MonsterList monsterListIn)
    {
        this.monsterList = monsterListIn;
    }

    public void setVariables(string nameIn)
    {
        Debug.Log("Monster Reader" + monsterList.monster.Length);
        for(int i = 0; i < monsterList.monster.Length; i++)
        {
            //Debug.Log(monsterList.monster[i] + " " + nameIn);
            if(monsterList.monster[i].name.Equals(nameIn))
            {
                //Debug.Log(monsterList.monster[i].name.Equals(nameIn));
                nameM = monsterList.monster[i].name;
                lvl = monsterList.monster[i].lvl;
                health = monsterList.monster[i].health;
                attack = monsterList.monster[i].attack;
                exp = monsterList.monster[i].attack;

            }
        }
    }

}
