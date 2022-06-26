using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable{
        get{
            return isPlaceable;
        }
    }

    GridManager gridManager;
    PathFinder pathFinder;

    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyRecievers();
            }

        }
    }

}
