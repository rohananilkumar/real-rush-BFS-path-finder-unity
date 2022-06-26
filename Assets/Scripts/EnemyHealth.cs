using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] int difficultyRound = 1;
    int currentHitPoints = 0;

    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other) {
        Debug.Log("HIT");
        currentHitPoints--;
        if(currentHitPoints<=0){
            gameObject.SetActive(false);
            enemy.RewardGold();
            maxHitPoints += difficultyRound;
        }
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
}
