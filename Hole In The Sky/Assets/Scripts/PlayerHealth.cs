using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    private int currentHp = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _damage)
    {
        currentHp -= _damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("U ded lul");
    }
    
    //gdy otrzymuje obrazenia kamera zaczyna zmniejszac
    //swoje pole widzenia i daje czerwona poswiate
    //oraz jezeli sie da rozmazuje obraz
    

}
