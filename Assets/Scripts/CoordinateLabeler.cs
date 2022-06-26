using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, .5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
        label.enabled = false;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying){
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels(){
        if(Input.GetKeyDown(KeyCode.C)){
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor(){
        if (!gridManager) return;

        Node node = gridManager.GetNode(coordinates);

        if (node == null) return;

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        } else if (node.isPath)
        {
            label.color = pathColor;
        } else if (node.isExplored)
        {
            label.color = exploredColor;
        } else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates(){
        if (!gridManager) return;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize); 
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName() {
        transform.parent.name = coordinates.ToString();
    }
}
