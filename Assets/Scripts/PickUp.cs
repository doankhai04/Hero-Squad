using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUp;
    [SerializeField] AudioClip keyPickUp;
    [SerializeField] AudioClip heartPickUp;
    bool wasPickedUp = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero" && !wasPickedUp)
        {
            wasPickedUp = true;
            if (gameObject.tag == "Coins")
            {
               GameSession.instance.AddScore(10);
                AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position);
            }
            if (gameObject.tag == "Keys")
            {
                GameSession.instance.AddKey();
                AudioSource.PlayClipAtPoint(keyPickUp, Camera.main.transform.position);
            }
            if(gameObject.tag == "Heart")
            {
                GameSession.instance.RecoverBlood();
                AudioSource.PlayClipAtPoint(heartPickUp, Camera.main.transform.position);

            }
            Destroy(gameObject);
        }
    }
}
