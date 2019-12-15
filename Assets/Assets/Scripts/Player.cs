using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // get reference to rigidbody
    private Rigidbody2D _rigid;

    [SerializeField]
    private float _jumpforce = 5.0f;
    [SerializeField]
    private bool _grounded = false;
    private bool _resetJumpNeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckGrounded();





    }

    void Movement()
    {
        // horizontal input
        float move = Input.GetAxisRaw("Horizontal");
        // current velocity = new velocity 9x, current y';

        // space key && grounded , jump
        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            _grounded = false;
            _resetJumpNeeded = true;
            Debug.Log("Before routine");
            StartCoroutine("ResetJumpNeededRoutine");
            Debug.Log("after routine");

        }

        _rigid.velocity = new Vector2(move, _rigid.velocity.y);
    }

    void CheckGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + new Vector3(0, transform.position.y + 0.5f, 0), Vector2.down, 0.6f, 1 << 8);
        Debug.DrawRay(transform.position + new Vector3(0, transform.position.y + 0.5f, 0), Vector2.down * 0.6f, Color.green);

        if (hitInfo.collider != null)
        {
            Debug.Log("Hit: " + hitInfo.collider.name);
            Debug.Log("_resetJumpNeeded: " + _resetJumpNeeded);

            if (_resetJumpNeeded == false)
            {
                _grounded = true;
            }

        }
    }

    public IEnumerator ResetJumpNeededRoutine()
    {
        Debug.Log("Routine called");
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }
}
