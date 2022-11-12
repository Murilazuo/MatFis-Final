using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Vector3 rotation;
    [SerializeField] float speed;
    Triangle triangle;
    private void Start()
    {
        triangle = GetComponent<Triangle>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    }
    private void FixedUpdate()
    {
        rotation.x += Input.GetAxisRaw("Horizontal") * speed;
        float maxAngle = 6;

        if (rotation.x < 0) rotation.x = maxAngle;
        if (rotation.x > maxAngle) rotation.x = 0;

        rotation.z += Input.GetAxisRaw("Vertical") * speed;
        if (rotation.z < 0) rotation.z = maxAngle;
        if (rotation.z > maxAngle) rotation.z = 0;

        triangle.SetRotation(rotation);
    }
}
