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