using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // Reset MoveDelta
        moveDelta = new Vector3(x, y, 0);
        float scale = Mathf.Abs(transform.localScale.x);
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(scale, scale, scale);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1*scale, scale, scale);

        // Make sure we can move in that direction
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, 
            new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
            new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
