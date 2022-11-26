using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Vector3 rotation;
    [SerializeField] float speed;
    [SerializeField] float accereration;
    [SerializeField] float terminalVelocity;
    float _speed;
    Triangle triangle;
    private void Start() => triangle = GetComponent<Triangle>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if ((Input.GetButton("Horizontal") ||
            Input.GetButton("Vertical")) && _speed <= terminalVelocity)
            _speed += accereration;
        else
            _speed = speed;

        rotation.x += Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float maxAngle = 6.3f;

        if (rotation.x < 0) rotation.x = maxAngle;
        if (rotation.x > maxAngle) rotation.x = 0;

        rotation.z += Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        if (rotation.z < 0) rotation.z = maxAngle;
        if (rotation.z > maxAngle) rotation.z = 0;

        triangle.SetRotation(rotation);
    }
}
