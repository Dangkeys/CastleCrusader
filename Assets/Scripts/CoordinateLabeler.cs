using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    GridManager gridManager;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            label.enabled = false;
        }
        UpdateObjectName();
        SetLabelColor();
        ToggleLabel();
    }
    void ToggleLabel()
    {
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     label.enabled = !label.IsActive();
        // }
    }

    private void SetLabelColor()
    {
        if(gridManager == null) return;
        Node node = gridManager.GetNode(coordinates);
        if(node == null) return;

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }

    }

    private void DisplayCoordinates()
    {

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;

    }
}
