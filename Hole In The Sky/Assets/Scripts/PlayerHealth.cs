using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //gdy otrzymuje obrazenia kamera zaczyna zmniejszac
    //swoje pole widzenia i daje czerwona poswiate
    //oraz jezeli sie da rozmazuje obraz

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
