using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPoint : Point
{

    public float influenceDuration = 10f;

    // Start is called before the first frame update
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().EatSuperPoint(this);  
    }
}
