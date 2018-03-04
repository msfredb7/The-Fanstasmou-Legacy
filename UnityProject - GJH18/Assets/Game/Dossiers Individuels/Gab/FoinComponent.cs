using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FoinComponent : MonoBehaviour
{

    public Transform tr;

    public GameObject m_PlusPrefab;
    GameObject m_PlusSprite;

    bool m_contact;

    SpawnerSheep m_SpawnS;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start()
    {
        m_contact = false;
        if (Game.Instance != null)
        {
            m_SpawnS = Game.Instance.SheepSpawn;
        }
        else
        {
            m_SpawnS = transform.Find("SpawnZoneSheep").GetComponent<SpawnerSheep>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sheep") && m_contact == false)
        {
            m_contact = true;

            m_SpawnS.SpawnPrefAtLocation(tr.position);

            m_PlusSprite = Instantiate(m_PlusPrefab, tr.position, Quaternion.identity);

            Sequence m_PlusSheep = DOTween.Sequence();

            m_PlusSheep.Append(m_PlusSprite.transform.DOMoveY(m_PlusSprite.transform.position.y + 1, 2.0f).SetEase(Ease.OutQuint));

            //m_PlusSheep.AppendInterval(0.5f);
            //m_PlusSheep.Append(m_PlusSprite.transform.DOMoveY(m_PlusSprite.transform.position.y + 10.0f, 1.0f));

            m_PlusSheep.Insert(1.1f, m_PlusSprite.GetComponent<SpriteRenderer>().DOFade(0.0f, 1.0f));

            m_PlusSheep.Play();

            gameObject.GetComponentInParent<SpawnerFoin>().FoinRemove(tr);

            Destroy(gameObject);
        }
    }
}
