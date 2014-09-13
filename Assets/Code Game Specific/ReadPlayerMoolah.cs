using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ReadPlayerMoolah : MonoBehaviour 
{
    public GameObject OneMoolah, FiveMoolah, TenMoolah;
    public IHasMoolah MoolahToRead;
    private int oldMoolah;

    private List<GameObject> gos = new List<GameObject>();

    //// Use this for initialization
    //void Start () 
    //{ 
	    
    //}
	
	// Update is called once per frame
	void Update ()
    {
        if(MoolahToRead.Moolah != oldMoolah)
        {
            ReplaceSprites(MoolahToRead.Moolah);
        }
        oldMoolah = MoolahToRead.Moolah;
	}

    private void ReplaceSprites(int p)
    {
        int tens = p / 10;
        int rest = p - tens *10;
        int fives = rest / 5;
        rest -= fives * 5;
        int ones = rest;

        #region Destroy Old

        int count = gos.Count;

        for (int i = 0; i < count; i++)
        {
            int index = count - i-1;
            GameObject cur = gos[index];
            gos.RemoveAt(index);
            GameObject.Destroy(cur);
        }

        #endregion

        #region Create New

        Create(tens, TenMoolah);
        Create(fives, FiveMoolah);
        Create(ones, OneMoolah);

        #endregion


        //Debug.Log("Tens: " + tens + " Fives: " + fives + " Ones: " + ones);
    }

    private void Create(int amount, GameObject sprite)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject sprNew = Object.Instantiate(sprite) as GameObject;
            sprNew.transform.parent = gameObject.transform;
            gos.Add(sprNew);
        }
    }
}
