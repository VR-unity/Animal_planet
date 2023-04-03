using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class StartBuildingButton : MonoBehaviour
{
    public bool startbuilding;
    private NetworkContext context;
     private struct Message
    {
        public bool building;
        public Message(bool buildflag) {
            this.building = buildflag;
        }
    }

    void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        // Debug.Log(data.resetf);
        if (data.building)
        {
            startbuilding = true;
        }else{
            startbuilding = false;
        }
    }
    public void building()
    {
        if (startbuilding == false)
        {
            startbuilding = true;
        }
        else{
            startbuilding = false;
        }
        // Debug.Log(startbuilding);
        context.SendJson(new Message(startbuilding));
        
    }
    
}
