using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;
using Ubiq.XR;

public class MyNetworkedObject : MonoBehaviour, IGraspable
{
    // public NetworkId NetworkId { get; set; }
    private bool owner;
    public bool control;
    private Hand controller;
    Vector3 lastPosition;
    private NetworkContext context;
    Quaternion lastRotation;
    public Slingshot slingshot;
    private struct Message
    {
        public Vector3 position;
        public Quaternion rotation;

        public bool o;
        public Message(Transform transform, bool owner) {
            this.position = transform.position;
            this.rotation = transform.rotation;
            this.o = owner;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        if (data.o)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        Debug.Log(data.o);
        if ((data.o || owner || slingshot.Bird != null))
        {
            transform.position = data.position;
            transform.rotation = data.rotation;
            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }  
    }

    private void FixedUpdate()
    {
        if(lastPosition != transform.position || lastRotation != transform.rotation)
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            context.SendJson(new Message(transform, owner));
        }
    }

    private void LateUpdate() {
        if (controller)
        {
            transform.position = controller.transform.position;
            transform.rotation = controller.transform.rotation;
        }
    }

    void IGraspable.Grasp(Hand controller)
    {
        owner = true;
        control = true;
        this.controller = controller;
    }

    void IGraspable.Release(Hand controller)
    {
        owner = false;
        control = false;
        this.controller = null;
    }
}
