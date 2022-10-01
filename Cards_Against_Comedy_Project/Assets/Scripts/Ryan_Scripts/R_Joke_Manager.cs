using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class R_Joke_Manager : MonoBehaviour
{
    public R_Audience_Manager AM;
    public int MaxAudienceSwap = 2;

    public List<GameObject> AllJokes;
    public List<GameObject> TotalRemainingJokes;
    public List<GameObject> UsedJokes;
    public List<GameObject> JokesInHand;

    public int MaxJokesInHand = 2;

    [Header("Values for cards in play")]
    [SerializeField] private GameObject CardParent;
    [SerializeField] private byte cardOffset = 1;
    public GameObject PlayArea;

    private void Start()
    {
        TotalRemainingJokes.AddRange(AllJokes);

        for(int i = 0; i < MaxJokesInHand; i++)
        {
            int CardNum = Random.Range(0, TotalRemainingJokes.Count);
            GameObject CardObj = Instantiate(TotalRemainingJokes[CardNum]);
            CardObj.transform.SetParent(CardParent.transform, false);
            JokesInHand.Add(CardObj);
            CardObj.GetComponent<R_Joke_Card>().ThisJokePrefab = TotalRemainingJokes[i];
            TotalRemainingJokes.RemoveAt(CardNum);

            JokesInHand[i].transform.position = new Vector3(-cardOffset + (i * cardOffset * 2), gameObject.transform.position.y, 0);
        }
    }

    public void ResetHand()
    {
        if(TotalRemainingJokes.Count == 0)
        {
            TotalRemainingJokes.AddRange(UsedJokes);
            UsedJokes.Clear();
        }

        int CardNum = Random.Range(0, TotalRemainingJokes.Count);
        GameObject CardObj = Instantiate(TotalRemainingJokes[CardNum]);
        CardObj.transform.SetParent(CardParent.transform, false);
        JokesInHand.Add(CardObj);
        CardObj.GetComponent<R_Joke_Card>().ThisJokePrefab = TotalRemainingJokes[CardNum];
        TotalRemainingJokes.RemoveAt(CardNum);

        for (int i = 0; i < JokesInHand.Count; i++)
        {
            JokesInHand[i].transform.position = new Vector3(-cardOffset + (i * cardOffset * 2), gameObject.transform.position.y, 0);
        }
    }

    public void PlayCard(GameObject PlayedCard)
    {
        JokesInHand.Remove(PlayedCard);
        UsedJokes.Add(PlayedCard.GetComponent<R_Joke_Card>().ThisJokePrefab);
        Destroy(PlayedCard);

        int DarkAudience = 0;
        int DeadpanAudience = 0;
        int SatireAudience = 0;
        int PunAudience = 0;

        foreach(GameObject member in AM.CurrentAudience)
        {
            JokeGenre Genre = member.GetComponent<R_Audience_Member>().Genre;

            if(Genre == JokeGenre.Dark)
            {
                DarkAudience++;
            }
            else if(Genre == JokeGenre.Deadpan)
            {
                DeadpanAudience++;
            }
            else if(Genre == JokeGenre.Satire)
            {
                SatireAudience++;
            }
            else if(Genre == JokeGenre.Puns)
            {
                PunAudience++;
            }
        }

        //Now we take the joke genre of the card played...
        JokeGenre genre = PlayedCard.GetComponent<R_Joke_Card>().Genre;

        if(genre == JokeGenre.Dark) { AM.AudienceSatisfaction += DarkAudience - DeadpanAudience; }
        else if (genre == JokeGenre.Deadpan) { AM.AudienceSatisfaction += DeadpanAudience - SatireAudience; }
        else if (genre == JokeGenre.Satire) { AM.AudienceSatisfaction += SatireAudience - PunAudience; }
        else if (genre == JokeGenre.Puns) { AM.AudienceSatisfaction += PunAudience - DarkAudience; }

        //Now that the joke has affected the audience, we roll need to do a few things. Get new joke card and update hand order & shuffle out some audience members...
        int numNewAudience = Random.Range(1, MaxAudienceSwap);
        AM.ShiftAudience(numNewAudience);
        ResetHand();
    }
}
