using System.Collections;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Spawning;
using Ubiq.XR;

public class Bird : MonoBehaviour
{
    public Rigidbody Rb;
    public AudioSource Slingshot;
    public AudioSource SlingshotRelease;
    public AudioSource Flying;
    public AudioSource BirdCollision;
    public float ReleaseTime = 0.5f;
    public float DestructionTime = 5f;
    private bool _isPressed;
    private bool _isFired;
    private NetworkContext context;
    private bool collisionflag;
    public bool releaseflag;
    private bool slingshotflag;

    private struct Message
    {
        public bool collisionf;
        public bool releasef;
        public bool slingshotf;
        public Message(bool collisionflag, bool releaseflag, bool slingshotflag) {
            this.collisionf = collisionflag;
            this.releasef = releaseflag;
            this.slingshotf = slingshotflag;
        }
    }

    void Start()
    {
        context = NetworkScene.Register(this);
    }
    
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();

        if (data.collisionf)
        {
            GetComponent<TrailRenderer>().enabled = false;
            BirdCollision.Play();
            GameManager.Instance.AddScore(Random.Range(5, 25) * 10, transform.position, Color.white);
            collisionflag = false;
        }
        if (data.releasef)
        {
            // _isPressed = false;
            Rb.isKinematic = false;

            GameManager.Instance.ActiveTurn = true;

            GetComponent<TrailRenderer>().enabled = true;
            _isFired = true;
            SlingshotRelease.Play();
            StartCoroutine(Release());
            releaseflag = false;
        }
        if (data.slingshotf)
        {
            // _isPressed = true;
            Rb.isKinematic = true;
            Slingshot.Play();
            slingshotflag = false;
        }
    }

    void Update()
    {
       
        // Debug.Log(_isPressed);
        if (_isPressed && !_isFired && !GameManager.Instance.IsLevelCleared)
        {
            Vector3 mousePosition = Input.mousePosition;
            
            Vector3 dir = transform.position - Camera.main.transform.position;
            
            Vector3 normardir = Vector3.Project(dir, Camera.main.transform.forward);
            
            // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, normardir.magnitude));
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 1.5f));
            if (worldPosition.y >= 0.02f && worldPosition.y <= 8f)
            {
            Rb.position = worldPosition;
            }
        }
    }

    void OnMouseDown()
    {
        // Debug.Log(_isFired);
        if (_isFired || GameManager.Instance.IsLevelCleared)
        {
            return;
        }

        slingshotflag = true;
        releaseflag = false;
        collisionflag = false;
        context.SendJson(new Message(collisionflag, releaseflag, slingshotflag));
        _isPressed = true;
        Rb.isKinematic = true;
        Slingshot.Play();
    }

    void OnMouseUp()
    {
        if (_isFired || GameManager.Instance.IsLevelCleared)
        {
            return;
        }

        _isPressed = false;
        Rb.isKinematic = false;

        GameManager.Instance.ActiveTurn = true;

        GetComponent<TrailRenderer>().enabled = true;
        _isFired = true;
        releaseflag = true;
        collisionflag = false;
        slingshotflag = false;
        context.SendJson(new Message(collisionflag, releaseflag, slingshotflag));
        SlingshotRelease.Play();
        StartCoroutine(Release());
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<TrailRenderer>().enabled = false;
        if (!collision.collider.CompareTag("Ground"))
        {
            // GameObject feathers = Instantiate(Feathers, transform.position, Quaternion.identity);
            // Destroy(feathers, 2);
            if (!BirdCollision.isPlaying)
            {
                collisionflag = true;
                releaseflag = false;
                slingshotflag = false;
                context.SendJson(new Message(collisionflag, releaseflag, slingshotflag));
                BirdCollision.Play();
            }
            GameManager.Instance.AddScore(Random.Range(5, 25) * 10, transform.position, Color.white);
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(ReleaseTime);
        Destroy(GetComponent<SpringJoint>());
        // GameObject.Find("sling").GetComponent<Slingshot>().Bird = null;
        Destroy(this.gameObject.GetComponent<MyNetworkedObject>());
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(DestructionTime);

        // GameManager.Instance.SetNewBird();
        GameManager.Instance.BirdDestroy.Play();
        // Instantiate(FeatherExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
