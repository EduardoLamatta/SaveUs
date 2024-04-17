using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEffects : MonoBehaviour
{
    public void MovementsInGame(Vector3 finalPosition, float speedMovement)
    {
        Vector3 direction = (finalPosition - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, finalPosition);

        transform.position += direction * speedMovement * Time.deltaTime;

        if (distance <= speedMovement * Time.deltaTime)
        {
            transform.position = finalPosition;
        }
    }
}
