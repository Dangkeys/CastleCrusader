using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float buildDelay = 1f;
    [SerializeField] int cost = 75;
    private void Start()
    {
        StartCoroutine(Build());
    }
    public bool CreateTower(Tower towerPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) return false;
        if (bank.CurrentBalance >= cost)
        {
            Instantiate(towerPrefab, position, Quaternion.identity);
            bank.WithDraw(cost);
            return true;
        }
        return false;
    }
    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }

    }
    void Update()
    {

    }
}
