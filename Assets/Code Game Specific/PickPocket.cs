using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IHasMoolah))]
public class PickPocket : MonoBehaviour 
{
    //public float PickPocketWindow = 0.3f;

    public float PickPocketBeforeBounce = 0.1f;
    public float PickPocketAfterBounce = 0.15f;

    //public List<AudioClip> PickPocketSoundFX;

    public List<string> PickPocketClipNames = new List<string>();
    public List<AudioSample> samples;

    private bool pressed;
    private bool wantToPickPocket = false;
    private bool collided;
    private int bouncesAllowed;
    private IHasMoolah wallet;
    private string validTag = "IHasMoolah";

    void Start () 
    {
        wallet = GetComponent<IHasMoolah>();
        samples = new List<AudioSample>();
        foreach (string str in PickPocketClipNames)
        {
            samples.Add(AudioManager.FindSampleFromCurrentLibrary(str));
        }
    }

    #region Input

    public void SetInput(bool actionPressed)
    {
        pressed = actionPressed;
        if(pressed)
            StartInputValidDelay();
    }

    #endregion

    private void DoPickPocket(IHasMoolah otherGuysMoolah)
    {
        int stealAmount =otherGuysMoolah.Moolah;
 
        if(stealAmount>0)
        {
            wallet.Moolah += stealAmount;
            otherGuysMoolah.Moolah = 0;
            int randomIndex = Random.Range(0,PickPocketClipNames.Count);

            AudioManager.Play(samples[randomIndex],transform.position);
        }

        Debug.Log("PickPocket Succes: Stole " + stealAmount);

    }

    #region InputDelay

    private IEnumerator newValidInputDelayCR()
    {
        //float pickPocketInputDelayWindow = 0.5f * PickPocketWindow;
        float start = Time.timeSinceLevelLoad;

        pressed = false;
        wantToPickPocket = true;

        while (Time.timeSinceLevelLoad - start < PickPocketBeforeBounce)
        {
            if (pressed || !wantToPickPocket)
                yield break;
            yield return null;
        }

        wantToPickPocket = false;
    }

    #endregion

    #region BounceDelay

    private IEnumerator newValidBounceDelayCR(IHasMoolah otherMoolah)
    {
        //float pickPocketInputDelayWindow = 0.5f * PickPocketWindow;
        float start = Time.timeSinceLevelLoad;

        while (Time.timeSinceLevelLoad - start < PickPocketAfterBounce)
        {
            if(wantToPickPocket)
            {
                DoPickPocket(otherMoolah);
                wantToPickPocket = false;
                yield break;
            }
            yield return null;
        }
    }

    #endregion

    #region Collision Handling

    #region Triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(validTag))
            bouncesAllowed = 1;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bouncesAllowed = 0;
    }

    #endregion

    #region Collision

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (bouncesAllowed > 0 && other.gameObject.CompareTag(validTag))
        {
            Debug.Log("ValidBounce");
            other.gameObject.GetComponent<CharacterController>().SetBounce();
            IHasMoolah otherMoolah = other.gameObject.GetComponent<IHasMoolah>();
            // Handle Bounce
            newValidBounceDelay(otherMoolah);
            bouncesAllowed = 0;
        }
    }

    #endregion

    #endregion

    #region Coroutine helpers
    private void newValidBounceDelay(IHasMoolah otherMoolah)
    {
        StartCoroutine(newValidBounceDelayCR(otherMoolah));
    }
    private void StartInputValidDelay()
    {
        StartCoroutine(newValidInputDelayCR());
    }
    #endregion
}
