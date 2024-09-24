using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotasionGameObject : MonoBehaviour
{
    [SerializeField] float speed = 50f;

    public float Speed
    {
        get { return speed; }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);

    }
}
