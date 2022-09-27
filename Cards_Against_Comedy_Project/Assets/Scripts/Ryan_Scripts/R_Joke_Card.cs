using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_Joke_Card : MonoBehaviour
{
    [TextArea(3, 6)]
    public string JokeSetup;
    [TextArea(3,6)]
    [SerializeField] private string Punchline;
    [SerializeField] private TMP_Text TextBox;
    public GameObject ThisJokePrefab;

    private void Start()
    {
        TextBox.text = '"' + JokeSetup + '"';
    }
}

