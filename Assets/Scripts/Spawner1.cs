using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    public GameObject itemPrefab;

    public void Spawn1(GameObject item)
    {
        Instantiate(item, new Vector3(3.5f, 0.2f, 1.5f),Quaternion.identity);

    }
}
