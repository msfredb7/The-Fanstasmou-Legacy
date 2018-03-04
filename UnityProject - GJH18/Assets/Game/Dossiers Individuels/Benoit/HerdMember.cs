using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HerdMember : MonoBehaviour
{
    public static float maxDistance = 1.0f;
    public SpriteRenderer endangeredVisuals;
    public Transform headLocation;

    private Voisin voisin;
    private Transform endangeredVisuals_TR;

    private bool UIenabled = true;

    [HideInInspector]
    private Herd herd = null;

    public void Start()
    {
        Game.OnGameStart += () => DisableUI();

        voisin = GetComponent<Voisin>() as Voisin;
        herd = HerdList.Instance.NewHerd(this);
        endangeredVisuals_TR = endangeredVisuals.transform;
        endangeredVisuals_TR.SetParent(transform.parent);
    }


    public List<HerdMember> GetcloseSheeps()
    {
        List<HerdMember> members = new List<HerdMember>();
        List<Voisin> lVoisins = voisin.GetVoisinsInRange(maxDistance);

        for (int i = 0; i < lVoisins.Count; i++)
        {
            HerdMember hMember = lVoisins[i].gameObject.GetComponent<HerdMember>() as HerdMember;
            if (hMember)
                members.Add(hMember);
        }

        return members;
    }


    public void Update()
    {
        //Debug.DrawLine(transform.position, (Vector3)herd.GetMiddle(), Color.red);

        var endangered = herd.MemberCount() <= WolfBehavior.maxSheepEaten;
        endangeredVisuals.enabled = endangered;
        if (endangered)
        {
            endangeredVisuals_TR.position = headLocation.position;
        }
    }

    public void SetHerd(Herd newHerd)
    {
        if (newHerd == herd)
            return;

        if (herd != null)
            herd.RemoveMember(this);
        herd = newHerd;
    }
    public Herd GetHerd()
    {
        return herd;
    }

    public void OnDestroy()
    {
        if (endangeredVisuals_TR != null && Application.isPlaying)
            Destroy(endangeredVisuals_TR.gameObject);
    }

    public void EnableUI()
    {
        endangeredVisuals.SetAlpha(1.0f);
       
    }

    public void DisableUI()
    {
        endangeredVisuals.SetAlpha(0.0f);
    }

    public void Evac(Action OnComplete)
    {
        List<PlayerInfo> bergers = Game.Instance.GetDoggies();

        float minDistance = float.MaxValue;
        PlayerInfo closestPlayer = null;
        for (int i = 0; i < bergers.Count; i++)
        {
            float dist = ((Vector2)bergers[i].transform.position - (Vector2)transform.position).magnitude;
            if (dist < minDistance)
                closestPlayer = bergers[i];
        }
        if(closestPlayer != null && Rounds.Instance != null)
        {
            Rounds.Instance.AddSheepRescued(1, closestPlayer);
            OnComplete();
            herd.RemoveMember(this);
            Destroy(gameObject);
        }
    }
}
