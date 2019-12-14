using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // get reference to rigidbody
    private Rigidbody2D _rigid;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal input
        float horizzontalInput = Input.GetAxis("Horizontal");
        // current velocity = new velocity 9x, current y';

        _rigid.velocity = new Vector2(horizzontalInput, _rigid.velocity.y);

    }
}
