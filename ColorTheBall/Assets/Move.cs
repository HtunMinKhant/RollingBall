using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public enum MoveType
    {
        Sequence, Random
    };
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float speed = 8;
    int index = 0;
    [SerializeField] MoveType movetype;
    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, patrolPoints.Length);
        transform.position = patrolPoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, patrolPoints[index].position) < 0.1f)
        {
            if (movetype == MoveType.Sequence)
                index = (index + 1) % patrolPoints.Length;
            else if (movetype == MoveType.Random)
                index = Random.Range(0, patrolPoints.Length);
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].position, speed * Time.deltaTime);
    }
}
