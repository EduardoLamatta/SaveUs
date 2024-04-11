using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stranger : SceneEffects
{
    [SerializeField] private Vector2 initPosition;
    [SerializeField] private Vector2 rightPosition;
    void Start()
    {
        initPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveRight();
        }
    }

    private void MoveRight()
    {
        MovementsInGame(initPosition, rightPosition);
    }


}
