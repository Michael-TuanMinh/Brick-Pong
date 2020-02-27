using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject breakEffect;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GameObject effect = Instantiate(breakEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f), Quaternion.identity);
            MainModule mainModule = effect.GetComponent<ParticleSystem>().main;
            mainModule.startColor = GetComponentInChildren<SpriteRenderer>().color;

            if(GetComponentInParent<PlayerController>() != null)
            {
                GetComponentInParent<PlayerController>().lives--;
            }
            else
            {
                GetComponentInParent<AI>().lives--;
            }

            Destroy(this.gameObject);
        }
    }



}
