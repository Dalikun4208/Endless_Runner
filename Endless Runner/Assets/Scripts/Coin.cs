using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject rotationGameObject;
    [SerializeField] float speed;
    private void OnEnable()
    {
        rotationGameObject = GameObject.Find("RotasionGameObject");
        transform.localRotation = rotationGameObject.transform.localRotation;
        speed = rotationGameObject.GetComponent<RotasionGameObject>().Speed;
    }

    private void Update()
    {
        transform.localRotation = rotationGameObject.transform.rotation;
    }

}
