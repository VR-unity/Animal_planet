using UnityEngine;

public class Stone : MonoBehaviour
{
    public AudioSource StoneCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bird"))
        {
            StoneCollision.Play();
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
}
