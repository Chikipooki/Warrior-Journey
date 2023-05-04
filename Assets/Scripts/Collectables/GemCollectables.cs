using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCollectables : MonoBehaviour
{
    [SerializeField] private float gemValue;

    [Header("SFX")]
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            string gemCount = GameObject.Find("GemCountText").GetComponentInChildren<Text>().text;
            int gemCount_int = System.Convert.ToInt32(gemCount);
            gemCount_int++;

            GameObject.Find("GemCountText").GetComponentInChildren<Text>().text = System.Convert.ToString(gemCount_int);

            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(gemValue);
            gameObject.SetActive(false);
        }
    }
}
