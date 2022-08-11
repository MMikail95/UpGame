using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    #region Instance
    public static Player instance;
    void InitSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void Awake()
    {
        InitSingleton();
    }

    #region Variables

    public List<Color> colors;
    public Score score;
    public Rigidbody rb;
    public float move;
    public float speed;
    private Color currentColor;
    public int carpmaSayisi = 0;
    public bool isThrown;
    public bool isHit;
    public float minX, maxX;
    [SerializeField] private List<GameObject> _cubeList;

    #endregion

    private void Start()
    {
        Debug.Log("test");
        int index = Random.Range(0, colors.Count);
        currentColor = colors[index];
        GetComponent<Renderer>().material.color = currentColor;
    }

    private void Update()
    {
        if (GameManager.instance.gameStarted)
        {
            Throw();
            Swipe();
        }
    }

    private void Throw()
    {
        #region Position Limit
        Vector3 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
        transform.position = playerPos;
        #endregion
        if (!isThrown && !isHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(0, move, 0);
                isThrown = true;
            }
        }

    }

    private void Swipe()
    {
        if (!isThrown)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Breakable"))
        {
            # region Color Change
            GetComponent<Renderer>().material.color = collision.collider.GetComponent<Renderer>().material.color;
            collision.collider.GetComponent<Renderer>().material.color = currentColor;
            currentColor = GetComponent<Renderer>().material.color;
            #endregion

            isHit = true;
            carpmaSayisi++;

            int blueCount = 0;

            for (int i = 0; i < _cubeList.Count; i++)
            {
                if (_cubeList[i].GetComponent<Renderer>().material.color == Color.blue)
                {
                    Debug.Log(blueCount);
                }
            } 
        }

        if (carpmaSayisi >= 2)
        {
            ResetPlayer();
        }

        else if (collision.gameObject.name == "Plane")
        {
            isHit = false;
            isThrown = false;  
        }
    }

    public void ResetPlayer()
    {
        int index = Random.Range(0, colors.Count);
        currentColor = colors[index];
        transform.position = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
        carpmaSayisi = 0;
        isThrown = false;
    }

}