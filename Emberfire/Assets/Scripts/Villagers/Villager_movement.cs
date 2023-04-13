using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bounds2DExt
{
    public static Bounds Get2DBounds(this Bounds aBounds)
    {
        var ext = aBounds.extents;
        ext.z = float.PositiveInfinity;
        aBounds.extents = ext;
        return aBounds;
    }
}

public class Villager_movement : Interactable
{

    private Vector3 direction_vector;
    public Transform myTransform;
    public Rigidbody2D myrigidbody;
    public Animator anim;
    public float Speed = 5f;
    public Collider2D bounds;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myrigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + direction_vector * Speed * Time.deltaTime;
        
        //bounds.bounds.Contains(temp)
        if (bounds.bounds.Get2DBounds().Contains(temp))
        {
            myrigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0,4);
        switch (direction)
        {
            case 0:
                // walking to the right
                direction_vector = Vector3.right;
                break;
            case 1:
                // walking to the left
                direction_vector = Vector3.left;
                break;
            case 2:
                // walking to the up
                direction_vector = Vector3.up;
                break;
            case 3:
                // walking to the down
                direction_vector = Vector3.down;
                break;
            default:
                break;
        }
        Update_Animation();
    }

    void Update_Animation()
    {
        anim.SetFloat("Horizontal", direction_vector.x);
        anim.SetFloat("Vertical", direction_vector.y);
    }
}
