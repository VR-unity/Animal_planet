using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemPrefab;

    public void Spawn(GameObject item)
    {
        Instantiate(item, new Vector3(-0.3f, 0.3f, -1f), Quaternion.identity);
    }
}
