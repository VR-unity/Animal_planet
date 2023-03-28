using UnityEngine;

public class Wood : MonoBehaviour
{
	public GameObject WoodShatter;
    public AudioSource WoodCollision;


    void OnCollisionEnter(Collision collision)
	{
        if (collision.collider.CompareTag("sheep")||collision.collider.CompareTag("duck")||collision.collider.CompareTag("cat")||collision.collider.CompareTag("penguin"))
        {
            WoodCollision.Play();
        }
		if (GameObject.FindGameObjectWithTag("Building").GetComponent<StartBuildingButton>().startbuilding == false && collision.relativeVelocity.magnitude > 8.5f && (this.GetComponent<MyNetworkedObject>().control == false))
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
        GameManager.Instance.WoodDestruction.Play();
        GameObject shatter = Instantiate(WoodShatter, transform.position, Quaternion.identity);
        GameManager.Instance.AddScore(500, transform.position, Color.white);
        Destroy(shatter, 2);
		Destroy(gameObject);
	}
}