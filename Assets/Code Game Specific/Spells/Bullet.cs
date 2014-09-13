using UnityEngine;
using System.Collections;

public class Bullet : ManagedObject 
{
    public float Damage;
    public float Speed;
    public Vector3 Velocity;

    public float OutOfRangeMagnitude = 300;

    private bool Initiate = false;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Initiate)
        {
            Initiate = false;
            StartCoroutine(DeactivateWhenOutOfRange());  
        }
        this.transform.position += Velocity * Time.fixedDeltaTime;
	}

    public Bullet Launch(Vector3 origin, Vector3 Direction, float damage = -1, float speed = -1)
    {
        if (damage >= 0)
            Damage = damage;
        if (speed >= 0)
            Speed = speed;
        
        Direction.y = 0;
        Direction.Normalize();

        GameObject bullet = this.Create();
        
        Bullet bul = bullet.GetComponent<Bullet>();
        bul.Velocity = Direction * Speed;
        bul.transform.position = origin;
        bul.Initiate = true;

        return bul;
    }

    private IEnumerator DeactivateWhenOutOfRange()
    {
        while ((transform.position - Camera.main.transform.position).magnitude < OutOfRangeMagnitude)
        {
            if(!gameObject.activeSelf)
                yield break;

            yield return new WaitForSeconds(0.2f);
        }

        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
