using UnityEngine;
using System.Collections;

public class IAlert : MonoBehaviour 
{
    public GameObject AlertSpriteGameObject;
    [ReadOnly]
    public bool Alerted;
    
    public void Start()
    {
        SetAlertedBool(false);
    }

    public void SetAlerted(float seconds = 20.0f)
    {
        StartCoroutine(SetAlertedSimpleCR(seconds));
    }

    private IEnumerator SetAlertedSimpleCR(float seconds)
    {
        SetAlertedBool(true);
        yield return new WaitForSeconds(seconds);
        SetAlertedBool(false);
    }
	
    public void StopAlerted()
    {
        StopAllCoroutines();
        SetAlertedBool(false);
    }

    private void SetAlertedBool(bool alerted)
    {
        Alerted = alerted;
        AlertSpriteGameObject.SetActive(Alerted);
    }
}
