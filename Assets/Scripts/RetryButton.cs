using UnityEngine;
using UnityEngine.SceneManagement;
using Ubiq.Messaging;


public class RetryButton : MonoBehaviour
{
    private bool Resetflag;
    private NetworkContext context;
    private struct Message
    {
        public bool resetf;
        public Message(bool Resetflag) {
            this.resetf = Resetflag;
        }
    }
    void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        Debug.Log(data.resetf);
        if (data.resetf)
        {
            SceneManager.LoadScene("backup_animalplanet");
            // Resetflag = false;
        }
    }


    public void ResetLevel()
    {
        Resetflag = true;
        context.SendJson(new Message(Resetflag));
        SceneManager.LoadScene("backup_animalplanet");
    }
}
