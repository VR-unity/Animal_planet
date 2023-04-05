using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;

public class Spawner1 : MonoBehaviour, INetworkSpawnable
{
    public NetworkId NetworkId { get; set; }
    public GameObject itemPrefab;
    private NetworkContext context;

     void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void Spawn1(GameObject item)
    {
        GameObject Clone = NetworkSpawnManager.Find(this).SpawnWithPeerScope(item);
    }
}
