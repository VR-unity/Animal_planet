using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;

public class Spawner : MonoBehaviour
{
    public GameObject prefabitem;
    private NetworkContext context;
    private bool flag;
    public int count;

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
            count += 1;
            var Clone = Instantiate(prefabitem, new Vector3(-0.3f, 0.3f, -1f), Quaternion.identity);
            Clone.name = count.ToString();
        }
    }

    
    // private void FixedUpdate()
    // {
    //     Debug.Log(flag);
    //     if(flag == true)
    //     {
    //         context.SendJson(new Message(flag, prefabitem));
    //     }

    // }


    public void Spawn(GameObject item)
    {
        flag = true;
        context.SendJson(new Message(flag));
        count += 1;
        var Clone = Instantiate(item, new Vector3(-0.3f, 0.3f, -1f), Quaternion.identity);
        Clone.name = count.ToString();
    }
}
