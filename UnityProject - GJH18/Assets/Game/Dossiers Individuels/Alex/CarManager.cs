using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CarManager : MonoBehaviour {

    public GameObject carPrefab;

    public float startSpawningCarsAt = 1f;
    public float delayBetweenCarSpawns = 1f;
    public float carStayDuration = 1f;
    public float enteringAnimDuration = 1f;

    private List<GameObject> cars;
    private List<bool> carsExiting;

    void Start()
    {
        this.DelayedCall(delegate ()
        {
            SpawnCarsLoop();
        },startSpawningCarsAt);
    }

    void SpawnCarsLoop()
    {
        IntroCars(delegate ()
        {
            this.DelayedCall(delegate ()
            {
                ExitCars(delegate ()
                {
                    this.DelayedCall(delegate ()
                    {
                        SpawnCarsLoop();
                    }, delayBetweenCarSpawns);
                });
            }, carStayDuration);
        });
    }

    void IntroCars(Action onComplete)
    {
        cars = new List<GameObject>();
        carsExiting = new List<bool>();
        Sequence sqc = DOTween.Sequence();
        for (int i = 0; i < Game.Instance.map.carSpawnPoint.Count; i++)
        {
            GameObject newCar = Instantiate(carPrefab, Game.Instance.map.carSpawnPoint[i].position, Quaternion.identity);
            cars.Add(newCar);
            carsExiting.Add(false);
            sqc.Join(newCar.transform.DOMove(Game.Instance.map.carEnterPoint[i].position,enteringAnimDuration));
        }
        Debug.Log("Son de camion ici");
        sqc.OnComplete(delegate ()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                Debug.Log("Ouverture de la porte du char " + i);
            }
            onComplete();
        });
    }

    void ExitCars(Action onComplete)
    {
        Sequence sqc = DOTween.Sequence();
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i] == null || carsExiting[i])
                continue;


            Debug.Log("Fermture de la porte du char " + i);
            sqc.Join(cars[i].transform.DOMove(Game.Instance.map.carSpawnPoint[i].position, enteringAnimDuration));
            carsExiting[i] = true;
        }
        sqc.OnComplete(delegate ()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i] == null)
                    continue;
                Destroy(cars[i]);
                cars[i] = null;
            }
            onComplete();
        });
    }

    public void ExitCar(GameObject car)
    {
        if (car == null)
            return;
        Debug.Log("Fermture de la porte d'un char");
        int destinationIndex = 0;
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i] == null)
                continue;
            if (car == cars[i])
                destinationIndex = i;
        }
        if (carsExiting[destinationIndex])
            return;
        carsExiting[destinationIndex] = true;
        car.transform.DOMove(Game.Instance.map.carSpawnPoint[destinationIndex].position, enteringAnimDuration).OnComplete(delegate () {
            Destroy(car);
            cars[destinationIndex] = null;
        });
    }
}
