using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Joke_Manager : MonoBehaviour
{
    public List<GameObject> TotalRemainingJokes;
    public List<GameObject> UsedJokes;
    public List<GameObject> JokesInHand;

    public int MaxJokesInHand = 2;

    [Header("Values for cards in play")]
    [SerializeField] private GameObject CardParent;
    [SerializeField] private byte cardOffset = 1;

    private void Start()
    {
        for(int i = 0; i < MaxJokesInHand; i++)
        {
            int CardNum = Random.Range(0, TotalRemainingJokes.Count);
            GameObject CardObj = Instantiate(TotalRemainingJokes[CardNum]);
            CardObj.transform.SetParent(CardParent.transform, false);
            JokesInHand.Add(CardObj);
            TotalRemainingJokes.RemoveAt(CardNum);

            JokesInHand[i].transform.position = new Vector3(-cardOffset + (i * cardOffset * 2), gameObject.transform.position.y, 0);
        }
    }
}
