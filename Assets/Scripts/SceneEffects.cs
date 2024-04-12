using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneEffects : MonoBehaviour
{
    public void MovementsInGame(Vector3 finalPosition, float velocity)
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, velocity);
    }

    public void Transparency()
    {

    }



}
