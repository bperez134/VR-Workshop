using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject mon;
    public int numMob;
    public int count;
    public List<GameObject> mobs;

    private List<MonsterStats> hpBase;
    private MonsterStats hp;

    // Start is called before the first frame update
    void Start()
    {
        mobs = new List<GameObject>();
        hpBase = new List<MonsterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mobs.Count == 3)
        {
            for(int num = 0; num < mobs.Count; num ++)
            {
                if(hpBase[num].healthBar.value <= 0)
                {
                    Destroy(mobs[num]);
                    mobs.RemoveAt(num);
                    hpBase.RemoveAt(num);
                    count -= 1;
                }   
            }
        }

        if(count < numMob)
        {
            GameObject mob = Instantiate(mon) as GameObject;
            mobs.Add(mob);
            hp = mobs[count].GetComponent<MonsterStats>();
            hpBase.Add(hp);
            mob.transform.position = new Vector3(transform.position.x + Random.Range(-3, 3f),
                                                 transform.position.y,
                                                transform.position.z +  Random.Range(-3f, 3f));

            //Debug.Log(hp.healthBar.value);
            count += 1;
        }
    }
}
