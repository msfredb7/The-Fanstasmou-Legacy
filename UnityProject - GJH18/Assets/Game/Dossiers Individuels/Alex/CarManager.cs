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
    public float doorAnimDuration = 1;

    private List<GameObject> cars;
    private List<bool> carsExiting;

    void Start()
    {
        Game.OnGameStart += delegate ()
        {
            this.DelayedCall(delegate ()
            {
                SpawnCarsLoop();
            }, startSpawningCarsAt);
        };
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
            newCar.GetComponent<Camion>().popUpLocation = Game.Instance.map.moutonPopUp[i].gameObject;
            if (i == 2)
                newCar.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(-1,1,1);
            cars.Add(newCar);
            carsExiting.Add(false);

            float rotationAngle = ((Vector2)(Game.Instance.map.carEnterPoint[i].position - newCar.transform.position)).ToAngle();
            Vector3 rotation = newCar.transform.forward * rotationAngle;
            newCar.transform.rotation = Quaternion.Euler(rotation);

            sqc.Join(newCar.transform.DOMove(Game.Instance.map.carEnterPoint[i].position, enteringAnimDuration).OnComplete(delegate () {
                newCar.GetComponentInChildren<Animator>().enabled = true;
                this.DelayedCall(delegate () {
                    newCar.GetComponent<BoxCollider2D>().isTrigger = true;
                },doorAnimDuration);
            }));
        }
        Game.Instance.sfx.PlayTruckArrive();
        sqc.OnComplete(delegate ()
        {
            onComplete();
        });
    }

    void ExitCars(Action onComplete)
    {
        float evacuationSpeed = 0;
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i] == null || carsExiting[i])
                continue;
            if (cars[i].GetComponent<BoxCollider2D>().isTrigger == false)
            {
                this.DelayedCall(delegate ()
                {
                    if(cars[i] != null)
                    {
                        cars[i].GetComponentInChildren<Animator>().SetBool("closed", true);
                        evacuationSpeed = cars[i].GetComponent<Camion>().EvacSpeed;
                    }
                }, cars[i].GetComponent<Camion>().EvacSpeed);
            }
            else
            {
                cars[i].GetComponent<BoxCollider2D>().isTrigger = false;
                cars[i].GetComponentInChildren<Animator>().SetBool("closed", true);
            }
        }
        this.DelayedCall(delegate () {
            Sequence sqc = DOTween.Sequence();
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i] == null || carsExiting[i])
                    continue;

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
        }, doorAnimDuration + evacuationSpeed);
    }

    public void ExitCar(GameObject car)
    {
        if (car == null)
            return;
        if (car.GetComponent<BoxCollider2D>().isTrigger == false)
        {
            this.DelayedCall(delegate () {
                car.GetComponentInChildren<Animator>().SetBool("closed", true);
                this.DelayedCall(delegate ()
                {
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
                }, doorAnimDuration);
            }, car.GetComponent<Camion>().EvacSpeed);
        } else
        {
            car.GetComponent<BoxCollider2D>().isTrigger = false;
            car.GetComponentInChildren<Animator>().SetBool("closed", true);
        }
            
    }
}
