using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class CharacterCollision : MonoBehaviour
{
    public Rigidbody rb;
    public int numberOfClones;
    [SerializeField] private TextMeshPro CounterTxt;
    public Transform player;
    public Rigidbody parentRigidbody;

    // public Animator animComponent;
    void Start()
    {
        if (transform.parent != null)
        {
            parentRigidbody = transform.parent.GetComponent<Rigidbody>();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
           // animComponent.SetTrigger("Death");
            Destroy(gameObject);
        }

        if (other.gameObject != null && other.transform != null)
        {
            if (other.gameObject.tag == "enemys" && other.transform.parent != null && other.transform.parent.childCount > -1)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "ramp")
        {
            parentRigidbody.AddForce(0, 600 * Time.deltaTime, 0);


        }
    }
}
