using TMPro;
using UnityEngine;
using DG.Tweening;
public class PlayerManager : MonoBehaviour
{
    public PlayerManager movement;
    public Rigidbody rb;
    public Transform player;
    private int numberOfClones;
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject Clones;

    [Range(0f, 3f)] [SerializeField] private float DistanceFactor, Radius;

    private bool Death = false;

    [SerializeField] private Transform enemy;
    private bool attack;

    void Start()
    {
        player = transform;
        numberOfClones = transform.childCount - 1;
        CounterTxt.text = numberOfClones.ToString();



    }


    void Update()
    {
        

        if (transform.childCount < 2 && Death == false)
        {
            Debug.Log("öl");
            FindObjectOfType<GameManager>().endGame();
            movement.enabled = false;
            Death = true;
        }

        if (attack)
        {
            if (enemy != null)
            {
                var enemyDirection = new Vector3(enemy.position.x, transform.position.y, enemy.position.z) - transform.position;

                for (int i = 1; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 3f);
                }

                if (enemy.GetChild(1) != null && enemy.GetChild(1).childCount > 1)
                {
                    for (int i = 1; i < transform.childCount; i++)
                    {
                        var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                        if (Distance.magnitude < 10f)
                        {
                            transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                                new Vector3(enemy.GetChild(1).GetChild(0).position.x, transform.GetChild(i).position.y,
                                    enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 1f);
                        }
                    }
                }
                else
                {
                    attack = false;
                    for (int i = 1; i < transform.childCount; i++)
                        transform.GetChild(i).rotation = Quaternion.identity;

                    if (enemy != null)
                    {
                        enemy.gameObject.SetActive(false);
                    }
                    formatClones();
                }
            }
            numberOfClones = transform.childCount - 1;
            CounterTxt.text = numberOfClones.ToString();

        }
     
        else
        {
            
            Transform Player = transform;
            int childCount = Player.childCount - 1;
            CounterTxt.text = childCount.ToString();

            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    rb.AddForce(-70 * Time.deltaTime, 0, 0 , ForceMode.VelocityChange);
                }
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    rb.AddForce(70 * Time.deltaTime, 0, 0 , ForceMode.VelocityChange);
                }
                Debug.Log("dokunuyor");
                Debug.Log(Input.mousePosition.x);
            }
            rb.AddForce(0, 0, 1000f * Time.deltaTime);
/*
            if (Input.GetKey("a"))
            {
                rb.AddForce(-2000f * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("d"))
            {
                rb.AddForce(2000f * Time.deltaTime, 0, 0);
            }*/
        }
    }

    public void formatClones()
    {
        for (int i = 2; i < player.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            var NewPos = new Vector3(x, -0f, -z);
            player.transform.GetChild(i).DOLocalMove(NewPos, 0.5f).SetEase(Ease.OutBack);

        }
    }


    private void MakeClones(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(Clones, transform.position, Quaternion.identity, transform); /* the parent of each stickman will be the players transform*/
        }
        numberOfClones = transform.childCount - 1;
        CounterTxt.text = numberOfClones.ToString();
        formatClones();
    }




    public GameObject parentObject;

    private void DestroyClones(int number)
    {
        int childCount = parentObject.transform.childCount;

        formatClones();
        for (int i = 1; i < 1 + number && i < childCount; i++)
        {
            Transform childTransform = parentObject.transform.GetChild(i);
            Destroy(childTransform.gameObject);
            //Debug.Log("yoket");
            formatClones();

        }


        numberOfClones = childCount - number;
        CounterTxt.text = (numberOfClones).ToString();
        formatClones();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gate")
        {

            //Debug.Log("walked through gate");
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            var GateManager = other.GetComponent<GateManager>();

            if (GateManager.multiply)
            {
                Debug.Log(GateManager.randomNumber);
                Debug.Log(numberOfClones);
                Debug.Log((numberOfClones * GateManager.randomNumber) - GateManager.randomNumber);

                MakeClones((numberOfClones * GateManager.randomNumber));

            }

            if (GateManager.addition)
            {
                MakeClones(GateManager.randomNumber);
            }

            if (GateManager.extraction)
            {
                DestroyClones(GateManager.randomNumber);
            }

            if (GateManager.divide)
            {

                if (GateManager.randomNumber == 1)
                {
                    Debug.Log("Skipping the loop because random number is 1.");
                }
                else
                {
                    //  Debug.Log("numberOfClones: " + numberOfClones);
                    //  Debug.Log("GateManager.randomNumber: " + GateManager.randomNumber);
                    //  Debug.Log(numberOfClones / GateManager.randomNumber);
                    DestroyClones(numberOfClones / GateManager.randomNumber);

                }
            }
        }
        if (other.gameObject.tag == "enemy")
        {
            enemy = other.transform;
            attack = true;
            rb.AddForce(0, 0, -50000f * Time.deltaTime);

            // Check if there is a child at index 1
            if (other.transform.childCount > 1)
            {
                Transform child = other.transform.GetChild(1);

                // Check if the child has the enemyManager component
                enemyManager enemyMgr = child.GetComponent<enemyManager>();

                if (enemyMgr != null)
                {
                    enemyMgr.AttackThem(transform);
                }
                else
                {
                    Debug.LogWarning("enemyManager component not found on the child.");
                }
            }
            else
            {
                Debug.LogWarning("No child at index 1 found.");
            }
        }


    }

}

