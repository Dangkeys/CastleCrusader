using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float moveDelay = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            yield return new WaitForSeconds(moveDelay);
            transform.position = waypoint.transform.position;
            Debug.Log(waypoint.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
