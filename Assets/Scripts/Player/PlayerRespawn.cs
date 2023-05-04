using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    [SerializeField] private int RespawnCount;

    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private int deathCount = 1;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
        //
        GameObject.Find("Lcount").GetComponentInChildren<Text>().text = System.Convert.ToString(RespawnCount);
        GameObject.Find("Rcount").GetComponentInChildren<Text>().text = System.Convert.ToString(RespawnCount);
        GameObject.Find("GemCountText").GetComponentInChildren<Text>().text = System.Convert.ToString(0);
    }

    public void CheckRespawn()
    {
        string Rcount = GameObject.Find("Rcount").GetComponentInChildren<Text>().text;
        int Rcount_int = System.Convert.ToInt32(Rcount);

        if (deathCount > Rcount_int)
        {
            deathCount = 1;
            uiManager.GameOver();

            return;
        }
        else
        {
            string Lcount = GameObject.Find("Lcount").GetComponentInChildren<Text>().text;
            int Lcount_int = System.Convert.ToInt32(Lcount);
            Lcount_int--;

            GameObject.Find("Lcount").GetComponentInChildren<Text>().text = System.Convert.ToString(Lcount_int);
            deathCount++;
        } 
           

        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        Respawn();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position; //Move player to checkpoint location
        playerHealth.Respawn(); //Restore player health and reset animation
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}