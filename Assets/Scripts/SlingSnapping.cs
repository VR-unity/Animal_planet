using System;
using UnityEngine;
using Ubiq.Messaging;
 

public class SlingSnapping : MonoBehaviour
{
    public Collider myCollider;
    public float snapDistance = 1f;
    public Slingshot Slingshot;
    private bool flag;
    public GameObject SlingshotBird;
    private bool creat;
    private NetworkContext context;
    private struct Message
    {
        public bool creatf;
        public Message(bool creat) {
            this.creatf = creat;
        }
    }


    private void Start() {
        context = NetworkScene.Register(this);
        myCollider = GetComponent<Collider>();
        snapDistance = 1f;
    
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        if (data.creatf)
        {
            GameObject newbird = Instantiate(SlingshotBird, Slingshot.Hook.transform.position, Quaternion.identity);
            Slingshot.Bird = newbird;
            flag = true;
            Destroy(myCollider.gameObject);
        }
    }
    

    public void Update()
    {
        if (flag == false)
        {
            Vector3 SlingPoint = Slingshot.Hook.transform.position;
            Vector3 myClosestPoint = myCollider.ClosestPoint(SlingPoint);
            float offset = Vector3.Distance(myClosestPoint, SlingPoint);

            if (offset < snapDistance)
            {
                if ((myCollider.CompareTag("sheep")||myCollider.CompareTag("duck")||myCollider.CompareTag("cat")||myCollider.CompareTag("penguin"))&&(Slingshot.Bird == null))
                {
                    // Destroy(myCollider.gameObject);
                    creat = true;
                    context.SendJson(new Message(creat));
                    GameObject newbird = Instantiate(SlingshotBird, SlingPoint, Quaternion.identity);
                    Slingshot.Bird = newbird;
                    flag = true;
                    Destroy(myCollider.gameObject);
                }
                    

            }
        }
    }
}

