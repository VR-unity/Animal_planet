using UnityEngine;

public class Pig : MonoBehaviour
{
    public GameObject Smoke;

    void OnCollisionEnter(Collision collision)
    {
        if (GameObject.FindGameObjectWithTag("Building").GetComponent<StartBuildingButton>().startbuilding == false && collision.relativeVelocity.magnitude > 4f && (this.GetComponent<MyNetworkedObject>().control == false))
        {
            Destroy();
        }
        // else if (GameObject.FindGameObjectWithTag("Building").GetComponent<StartBuildingButton>().startbuilding == true && collision.relativeVelocity.magnitude > 0)
        // {
        //     this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ;
        // }
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
        GameManager.Instance.PigHit.Play();
        GameManager.Instance.PigDestroy.Play();
        GameObject smoke = Instantiate(Smoke, transform.position, Quaternion.identity);
        GameManager.Instance.AddScore(5000, transform.position, Color.cyan);
        Destroy(smoke, 3);
        Destroy(gameObject);
    }
}