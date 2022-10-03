using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Joke_Dragger : MonoBehaviour
{
    private Vector3 dragOffset;
    private Camera cam;
    private GameObject PlayArea;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float LockOnDistance = 1f;

    [HideInInspector] public R_Joke_Manager JM;
    [HideInInspector] public R_Joke_Card JC;

    public Vector3 HandPosition;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        JM = FindObjectOfType<R_Joke_Manager>();
        JC = GetComponent<R_Joke_Card>();
        PlayArea = JM.PlayArea;
        HandPosition = transform.position;
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    private void OnMouseUp()
    {
        if(Vector3.Distance(PlayArea.gameObject.transform.position, GetMousePos()) <= LockOnDistance)
        {
            //PLAY JOKE CODE HERE
            JM.Punchline(GetComponent<R_Joke_Card>().Punchline);    
            JM.PlayCard(this.gameObject);
        }
        else
        {
            transform.position = HandPosition;
        }
    }
    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
