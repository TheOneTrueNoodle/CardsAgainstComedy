using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
    
public class R_Audience_Manager : MonoBehaviour
{
    public List<GameObject> CurrentAudience;
    public int AudienceAmount = 10;
    public GameObject AudienceMemberPrefab;

    public TMP_Text SatisfactionDisplay;
    public int AudienceSatisfaction = 10;

    private void Start()
    {
        for (int i = 0; i < AudienceAmount; i++)
        {
            GameObject newMember = Instantiate(AudienceMemberPrefab);
            newMember.GetComponent<R_Audience_Member>().Genre = (JokeGenre)Random.Range(0, 4);
            newMember.transform.SetParent(transform, false);
            CurrentAudience.Add(newMember);
            CurrentAudience[i].transform.position = new Vector3(transform.position.x + (i * 0.7f * 2), gameObject.transform.position.y, 0);
        }
    }

    private void Update()
    {
        SatisfactionDisplay.text = "Satisfaction: " + AudienceSatisfaction.ToString();

        if(AudienceSatisfaction <= 0)
        {
            GameOver();
        }
    }

    public void ShiftAudience(int numNewAudience)
    {
        for(int i = 0; i < numNewAudience; i++)
        {
            int leavingMemberNum = Random.Range(0, AudienceAmount);
            GameObject LeavingMember = CurrentAudience[leavingMemberNum];
            CurrentAudience.RemoveAt(leavingMemberNum);
            Destroy(LeavingMember);
            GameObject newMember = Instantiate(AudienceMemberPrefab);
            newMember.GetComponent<R_Audience_Member>().Genre = (JokeGenre)Random.Range(0, 3);
            CurrentAudience.Add(newMember);
        }

        for(int i = 0; i < CurrentAudience.Count; i++)
        {
            CurrentAudience[i].transform.position = new Vector3(transform.position.x + (i * 0.7f * 2), gameObject.transform.position.y, 0);
        }
    }

    private void GameOver()
    {

    }
}
