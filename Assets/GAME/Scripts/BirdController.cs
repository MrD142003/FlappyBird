using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;

    [SerializeField] float bounceForce;
    private Rigidbody2D rigi;
    private GameObject spawner;
    public float flag = 0;
    private bool isAlive;
    [SerializeField] int score = 0;    
    

    // Start is called before the first frame update
    private void Awake()
    {
        isAlive = true;
        rigi = GetComponent<Rigidbody2D>();
        MakeInstance();
        spawner = GameObject.Find("Spawner Pipe");
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        BirdMovement();
    }

    private void BirdMovement()
    {
        if (isAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rigi.velocity = new Vector2(rigi.velocity.x, bounceForce);
            }
        }

       

        if (rigi.velocity.y > 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, 90, rigi.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        } else if (rigi.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else 
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -90, -rigi.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PipeHolder")
        {
            score++;
            if(GamePlayController.instance != null)
            {
                GamePlayController.instance.SetScore(score);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Ground")
        {
            flag = 1;
            if (isAlive)
            {
                isAlive = false;
                Destroy(spawner);
            }

            if (GamePlayController.instance != null)
                GamePlayController.instance._BirdDiedShowPanel(score);
            
        }
    }
}
