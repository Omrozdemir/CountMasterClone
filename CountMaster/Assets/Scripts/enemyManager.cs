using UnityEngine;
using TMPro;
using DG.Tweening;
public class enemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject Clones;
    [Range(0f, 2f)] [SerializeField] private float DistanceFactor, Radius;

    public Transform enemy;
    public bool attack;

    private int numberOfClones;

    void Start()
    {
        for (int i = 0; i < Random.Range(20,40); i++)
        {
            Instantiate(Clones, transform.position, new Quaternion(0f,100f,0f,1f) , transform); /* the parent of each stickman will be the players transform*/
        }
        CounterTxt.text = (transform.childCount - 1).ToString();


        formatClones();

    }

    public void formatClones()
    {
        for (int i = 2; i < transform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            var NewPos = new Vector3(x, -0f, -z);
            transform.transform.GetChild(i).localPosition = NewPos;

        }
    }

    // Update is called once per frame
    void Update()
    {
        numberOfClones = transform.childCount;
        CounterTxt.text = numberOfClones.ToString();

        if (attack && transform.childCount > 0 && enemy != null && enemy.childCount > 1)
        {
            var enemyPos = new Vector3(enemy.position.x, transform.position.y, enemy.position.z);
            var enemyDirection = enemy.position - transform.position;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up),
                    Time.deltaTime * 3f);

                var distance = enemy.GetChild(1).position - transform.GetChild(i).position;

                if (distance.magnitude < 30f)
                {
                    transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                        enemy.GetChild(1).position, Time.deltaTime * 1f);
                }
            }
        }
    }

    public void AttackThem(Transform enemyForce)
    {
        enemy = enemyForce;
        attack = true;
        Debug.Log("attack");
        /*
        for (int i = 0; i < transform.childCount; i++)
        {
            
        }
        */
    }
}
