using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private bool attaking;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        //Move spikehead to destination if attacking
        if (attaking)
            transform.Translate(speed * Time.deltaTime * destination);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        //Check if spikehead sees player in all 4 dir
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attaking)
            {
                attaking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position; //Set destination as current position so it doesnt move
        attaking = false;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D (collision);
        Stop();//Stop skihead once he his something
    }

}
