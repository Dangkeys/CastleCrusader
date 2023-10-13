using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceble;
    public bool IsPlaceble {get {return isPlaceble;}}
    [SerializeField] Tower towerPrefab;

    private void OnMouseDown() {
        if(isPlaceble)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceble = !isPlaced;
        }
    }
}
