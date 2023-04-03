using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;

public class Spawner : MonoBehaviour
{
    public NetworkId NetworkId { get; set; }
    public GameObject prefabitem;
    private NetworkContext context;
    private bool flag;

    private struct Message
    {
        public bool f;
        public Message(bool flag)
        {
            this.f = flag;
        }
    }

     void Start()
    {
        context = NetworkScene.Register(this);
    }



    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        if (data.f){
            // var Clone = Instantiate(prefabitem, new Vector3(-0.3f, 0.3f, -1f), Quaternion.identity);
            GameObject Clone = NetworkSpawnManager.Find(this).SpawnWithPeerScope(prefabitem);
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

    


    public void Spawn(GameObject item)
    {
        flag = true;
        context.SendJson(new Message(flag));

        // GameObject Clone = Instantiate(item, new Vector3(-0.3f, 0.3f, -1f), Quaternion.identity);
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
