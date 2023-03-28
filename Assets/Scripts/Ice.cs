using UnityEngine;

public class Ice : MonoBehaviour
{
    public GameObject IceShatter;

    void OnCollisionEnter(Collision collision)
    {
        if (GameObject.FindGameObjectWithTag("Building").GetComponent<StartBuildingButton>().startbuilding == false && collision.relativeVelocity.magnitude > 5f && (this.GetComponent<MyNetworkedObject>().control == false))
        {
            Destroy();
        }
    }
    
    void Update() 
    {
        if (GameObject.FindGameObjectWithTag("Building").GetComponent<StartBuildingButton>().startbuilding == true)
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 1;
        }
    }


    private void Destroy()
    {
        GameObject shatter = Instantiate(IceShatter, transform.position, Quaternion.identity);
        GameManager.Instance.AddScore(500, transform.position, Color.white);
        GameManager.Instance.IceDestruction.Play();
        Destroy(shatter, 2);
        Destroy(gameObject);
    }
}
