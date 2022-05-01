using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public Rigidbody2D rb;

    [SerializeField]
    // private float knockbackStrength;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

    //     if(rb != null)
    //     {
    //         Vector3 direction = collision.transform.position - transform.position;
    //         direction.y = 0;
    //         rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
    //     }
    // }

    public IEnumerator KnockbackAction(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        while(knockDur > timer)
        {
            timer+=Time.deltaTime;

            rb.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }

        yield return 0;
    }
}
