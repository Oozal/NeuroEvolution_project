using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public Neuralnetwork myNeuralNetwork;
    float[] input = new float[2];
    Vector3 tempPos;
    //RaycastHit2D hitUp, hitDown;
    public float rayDist;
    public LayerMask wall;
    public bool isChild;
    bool output;
    bool checkPointReached;
    [Header("For Driving Agent")]
    public Transform left_rayPoint, right_rayPoint;
    RaycastHit2D hitLeft, hitRight;

    [Header("Allowed stationary Time")]
    public float destroyTimer;
    public float additionalTime, currentTime,actualTime;
    Vector2 initialPos, currentPos;

    // Start is called before the first frame update
    void Start()
    {

        initialPos = transform.position;
        int numHiddenLayer = Random.Range(1, 5);
        myNeuralNetwork = new Neuralnetwork(numHiddenLayer);
        if (!isChild)
        {
            myNeuralNetwork.initializeWeight();
        }
        else
            myNeuralNetwork.fixingWeight();
        



    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        //if(Vector2.Distance(currentPos,initialPos)>1.0f)
        //{
        //    destroyTimer += 1;
        //    initialPos = currentPos;
        //}

        currentTime += Time.deltaTime;
        actualTime += Time.deltaTime;
        if(currentTime>destroyTimer)
        {
            DestroyPlayer();
        }
        if(actualTime>30)
        {
            DestroyPlayer();
        }
        transform.Translate(transform.right * 8 * Time.deltaTime, Space.World);
        #region BalancingBall
        //hitUp = Physics2D.Raycast(transform.position, Vector2.up, rayDist, wall);
        //float distUp = Vector2.Distance(transform.position, hitUp.point);
        //hitDown = Physics2D.Raycast(transform.position, Vector2.down, rayDist, wall) ;
        //float distDown = Vector2.Distance(transform.position, hitDown.point);
        //input[0] = distUp;
        //input[1] = distDown;
        //output = myNeuralNetwork.calculateValHidden(input);
        //if (output)
        //{
        //    moveUp();

        //}
        //else
        //    MoveDown();
        #endregion
        //hitLeft = Physics2D.Raycast(left_rayPoint.position, left_rayPoint.right, rayDist, wall);
        //hitLeft = Physics2D.Raycast(right_rayPoint.position, right_rayPoint.right, rayDist, wall);

        #region drivingAgent
        hitLeft = Physics2D.Raycast(left_rayPoint.position, left_rayPoint.right, rayDist, wall);
        hitRight = Physics2D.Raycast(right_rayPoint.position, right_rayPoint.right, rayDist, wall);
        if (hitLeft)
        {
            input[0] = Vector2.Angle(right_rayPoint.right, hitLeft.normal);
        }
        else
            input[0] = 0;
        if (hitRight)
        {
            input[1] = Vector2.Angle(right_rayPoint.right, hitRight.normal);
        }
        else
            input[1] = 0;
        float val = myNeuralNetwork.calculateValHidden(input);
        rotate(val);
        #endregion
    }

    void rotate(float angle)
    {
        transform.Rotate(transform.forward, angle);

    }

    
    void moveUp()
    {
        transform.Translate(Vector2.up * 0.1f);
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * 0.1f);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, Vector2.up * rayDist);
        //Gizmos.DrawRay(transform.position, -Vector2.up * rayDist);

        Gizmos.DrawRay(left_rayPoint.position, left_rayPoint.right * rayDist);
        Gizmos.DrawRay(right_rayPoint.position, right_rayPoint.right * rayDist);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("checkPoints"))
        {
            currentTime = 0;
        }
        else
        {
            DestroyPlayer();
        }
       
        
    }

    void DestroyPlayer()
    {
        PlayerInstancer.num_players -= 1;
        PlayerInstancer.bestNetwork = myNeuralNetwork;
        if (PlayerInstancer.num_players <= 0)
        {
            //Debug.Log("create children called");
            PlayerInstancer.playerInstancer_instance.createChildrens();
        }
        Destroy(this.gameObject);
        //Debug.Log(PlayerInstancer.num_players);
    }


}
