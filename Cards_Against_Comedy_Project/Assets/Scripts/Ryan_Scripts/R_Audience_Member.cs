using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Audience_Member : MonoBehaviour
{
    public JokeGenre Genre;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Color DarkColor, DeadpanColor, SatireColor, PunColor;
    public void Start()
    {
        if(Genre == JokeGenre.Dark)
        {
            renderer.color = DarkColor;
        }
        else if (Genre == JokeGenre.Deadpan)
        {
            renderer.color = DeadpanColor;
        }
        else if (Genre == JokeGenre.Satire)
        {
            renderer.color = SatireColor;
        }
        else if (Genre == JokeGenre.Puns)
        {
            renderer.color = PunColor;
        }
    }
}

public enum JokeGenre
{
    Dark = 0,
    Deadpan = 1,
    Satire = 2,
    Puns = 3

    //Dark > Deadpan > Satire > puns > dark etc
}
