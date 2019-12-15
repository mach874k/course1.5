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
    [SerializeField]
    private float _speed = 5.0f;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
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
        Flip(move);

        // space key && grounded , jump
        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            _grounded = false;
            _resetJumpNeeded = true;
            StartCoroutine("ResetJumpNeededRoutine");

        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);
    }

    void CheckGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.green);

        if (hitInfo.collider != null)
        {

            if (_resetJumpNeeded == false)
            {
                _grounded = true;
            }

        }
    }

    void Flip(float move){
        if(move > 0 ){
            _playerSprite.flipX = false;

        } else if (move < 0) {
            _playerSprite.flipX = true;
        }
    }

    public IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }
}
