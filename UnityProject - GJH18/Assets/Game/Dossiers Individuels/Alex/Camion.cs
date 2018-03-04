using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camion : MonoBehaviour {

    public int amountOfSheep = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<SheepComponent>() != null)
        {
            Debug.Log("TODO: +1 Sheep saved !");
            Debug.Log("TODO: delete sheep");
            Game.Instance.carManager.ExitCar(gameObject);
        }
    }
}
