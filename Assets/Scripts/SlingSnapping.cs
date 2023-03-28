using System;
using UnityEngine;
 

public class SlingSnapping : MonoBehaviour
{
    public Collider myCollider;
    public float snapDistance = 1f;
    public Slingshot Slingshot;
    private bool flag;
    public GameObject SlingshotBird;


    private void Start() {
        myCollider = GetComponent<Collider>();
        snapDistance = 1f;
    
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
                    Destroy(myCollider.gameObject);
                    GameObject newbird = Instantiate(SlingshotBird, SlingPoint, Quaternion.identity);
                    Slingshot.Bird = newbird;
                    flag = true;
                }

            }
        }
    }
}

