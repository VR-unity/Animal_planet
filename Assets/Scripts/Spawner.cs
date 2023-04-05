using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;

public class Spawner : MonoBehaviour, INetworkSpawnable
{
    public NetworkId NetworkId { get; set; }
    public GameObject prefabitem;
    private NetworkContext context;
    

     void Start()
    {
        context = NetworkScene.Register(this);
    }


    public void Spawn(GameObject item)
    {
        GameObject Clone = NetworkSpawnManager.Find(this).SpawnWithPeerScope(item);
        Slingshot slingshot = GameObject.Find("sling").GetComponent<Slingshot>();
        Clone.GetComponent<MyNetworkedObject>().slingshot = slingshot;
        Clone.GetComponent<SlingSnapping>().Slingshot = slingshot;
        if (Clone.CompareTag("sheep"))
        {
            Clone.GetComponent<SlingSnapping>().SlingshotBird = GameObject.Find("slingshot sheep");
        }else if (Clone.CompareTag("duck"))
        {
            Clone.GetComponent<SlingSnapping>().SlingshotBird = GameObject.Find("slingshot DUCK");
        }else if (Clone.CompareTag("cat"))
        {
            Clone.GetComponent<SlingSnapping>().SlingshotBird = GameObject.Find("slingshot cat");
        }else if (Clone.CompareTag("penguin"))
        {
            Clone.GetComponent<SlingSnapping>().SlingshotBird = GameObject.Find("slingshot penguin");
        }
    }
}
