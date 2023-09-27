using TMPro;
using UnityEngine;

public class GateManager : MonoBehaviour
{

    public TextMeshPro Gate_no;
    public int randomNumber;
    public bool multiply;
    public bool addition;
    public bool extraction;
    public bool divide;
    void Start()
    {

        if (multiply)
        {
            randomNumber = Random.Range(0, 3);
            Gate_no.text = "X" + (randomNumber +1).ToString();

        }
        if (addition)
        {

            randomNumber = Random.Range(30, 35);
            Gate_no.text = "+" + randomNumber.ToString();

        }

        if (extraction)
        {

            randomNumber = Random.Range(10, 25);
            Gate_no.text = "-" + randomNumber.ToString();

        }

        if (divide)
        {

            randomNumber = Random.Range(1, 3);
            Gate_no.text = "÷" + (randomNumber).ToString();

        }
    }

}
