using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons; // blades held by the player

    // called via animation event when player dies
    void DropSwords() // Player drops the blades
    {
        foreach(GameObject weapon in weapons)
        {
            weapon.AddComponent<Rigidbody>();
            weapon.AddComponent<BoxCollider>();
            weapon.transform.parent = null;
        }
    }
}
