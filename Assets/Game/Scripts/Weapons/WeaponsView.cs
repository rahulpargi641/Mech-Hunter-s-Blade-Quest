using System.Collections.Generic;
using UnityEngine;

public class WeaponsView : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    // called via animation event
    void DropSwords()
    {
        foreach(GameObject weapon in weapons)
        {
            weapon.AddComponent<Rigidbody>();
            weapon.AddComponent<BoxCollider>();
            weapon.transform.parent = null;
        }
    }
}
