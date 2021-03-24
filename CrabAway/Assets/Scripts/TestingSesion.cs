using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TestingSesion : MonoBehaviour
{
    public NavMeshAgent plAgent;
    public Text speedText;

    private float speed = 1f;
    

    private void Start()
    {
        StartCoroutine(ChangePlayerMoveSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed " + speed;
    }

    IEnumerator ChangePlayerMoveSpeed()
    {
        yield return new WaitForSeconds(30);
            plAgent.speed = 0.5f;
            speed = 2f;
            speedText.color = Color.red;
            yield return new WaitForSeconds(1f);
            speedText.color = Color.black;

        yield return new WaitForSeconds(30);
            plAgent.speed = 1f;
            speed = 3f;
            speedText.color = Color.red;
            yield return new WaitForSeconds(1f);
            speedText.color = Color.black;

        yield return new WaitForSeconds(30);
            plAgent.speed = 1.5f;
            speed = 4f;
            speedText.color = Color.red;
            yield return new WaitForSeconds(1f);
            speedText.color = Color.black;
    }
}
