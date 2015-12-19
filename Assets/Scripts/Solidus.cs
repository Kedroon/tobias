using UnityEngine;
using System.Collections;

public class Solidus : MonoBehaviour {



    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = false;
            Controller.estate = true;

        }
    
    
    }


    
    
    
}
