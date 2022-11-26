using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Color rightMat, wrongMat;
    [SerializeField] float time;
    Material mat;

    Triangle triangle;
    static Triangle player;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        triangle = GetComponent<Triangle>();
        
        if(!player)
            player = FindObjectOfType<Player>().GetComponent<Triangle>();
        
        mat = GetComponent<MeshRenderer>().material;

        RandomRotation();
    }

    void RandomRotation()
    {
        Vector3 randomRotation = new(Random.Range(0, 6), 0, Random.Range(0, 6));

        triangle.SetRotation(randomRotation);

        gameManager.AddTime();
        gameManager.AddScore(10);
    }

    private void FixedUpdate()
    {
        if (triangle.Equals(player))
        {
            Invoke(nameof(RandomRotation), time);
            mat.color = rightMat;
        }
        else
        {
            mat.color = wrongMat;
            CancelInvoke(nameof(RandomRotation));
        }
    }
}
