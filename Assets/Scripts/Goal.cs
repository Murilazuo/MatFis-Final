using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Color rightMat, wrongMat;
    Material mat;

    Triangle triangle;
    static Triangle player;

    private void Start()
    {
        triangle = GetComponent<Triangle>();
        
        if(!player)
            player = FindObjectOfType<Player>().GetComponent<Triangle>();
        
        mat = GetComponent<MeshRenderer>().material;

        Vector3 randomRotation = new(Random.Range(0,6),0, Random.Range(0, 6));

        triangle.SetRotation(randomRotation);
    }

    private void FixedUpdate()
    {
        if(triangle.Equals(player))
            mat.color = rightMat;
        else
            mat.color = wrongMat;
    }
}
