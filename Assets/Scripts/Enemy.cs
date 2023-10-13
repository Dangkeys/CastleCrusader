using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 20;
    [SerializeField] int goldPenalty = 20;
    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RewardGold()
    {
        if(bank == null) return;
        bank.Deposit(goldReward);
    }
    public void StealGold()
    {
        if(bank == null) return;
        bank.WithDraw(goldPenalty);
    }
}
