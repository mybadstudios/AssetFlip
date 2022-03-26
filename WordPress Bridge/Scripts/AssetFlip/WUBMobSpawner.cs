using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WUBMobSpawner : MonoBehaviour
{
    [SerializeField] Transform m_Sphere;
    [SerializeField] int m_MaxMob = 5, m_SpawnOnStart = 5;
    [SerializeField] float m_TimeBetweenSpawns = 10f;
    [SerializeField] float m_RaycastHeight = 50f;
    [SerializeField] LayerMask m_LayerMask;

    [SerializeField] Transform[] m_SpawnablePrefabs;

    List<Transform> m_Spawned = new List<Transform>();
    Dictionary<string, List<Transform>> m_Keep;

    int SpawnedCount => m_Spawned.Count;
    float SphereX { get; set; }
    float SphereZ { get; set; }
    float RayCastHeight { get; set ; }
    Vector3 Down { get; set;  }

    void Start()
    {
        SphereX = m_Sphere.localScale.x;
        SphereZ = m_Sphere.localScale.z;
        RayCastHeight = m_RaycastHeight + m_Sphere.transform.position.y ;
        Down = transform.TransformDirection(Vector3.down) * (RayCastHeight + 0.1f);

        m_Keep = new Dictionary<string, List<Transform>>();
        foreach (var pf in m_SpawnablePrefabs)
            m_Keep[pf.name] = new List<Transform>();

        for (int i = 0; i < m_SpawnOnStart; i++)
            SpawnRandom();

        StartCoroutine(CountMembers());
    }

    void SpawnRandom()
    {
        if (SpawnedCount >= m_MaxMob) return;
        int selected = Random.Range(0, m_SpawnablePrefabs.Length);
        string nm = m_SpawnablePrefabs[selected].name;
        Transform mob;
        if (m_Keep[nm].Count > 0)
        {
            mob = m_Keep[nm][0];
            m_Keep[nm].RemoveAt(0);
        }
        else
        {
            mob = Instantiate(m_SpawnablePrefabs[selected]);
        }
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(-SphereX, SphereX) + m_Sphere.transform.position.x;
            float z = Random.Range(-SphereZ, SphereZ) + m_Sphere.transform.position.z;
            var origin = new Vector3(x, RayCastHeight, z);
            var results = Physics.RaycastAll(origin, Down, RayCastHeight + 1f);

            if (results.Length > 0)
            {
                if (m_LayerMask == 0 || (results[0].transform.gameObject.layer & m_LayerMask) > 0)
                {
                    mob.transform.parent = null;
                    mob.position = results[0].point;
                    mob.transform.parent = transform;
                    mob.gameObject.SetActive(true);
                    m_Spawned.Add(mob);
                    mob.GetComponent<IWUBMobEntity>().Init(this);
                    return;
                }
            }
        }
        m_Keep[nm].Add(mob);
    }

    IEnumerator CountMembers()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_TimeBetweenSpawns/2f);
            if (SpawnedCount < m_MaxMob)
            { 
                Invoke("SpawnRandom", Random.Range( m_TimeBetweenSpawns / 2f, m_TimeBetweenSpawns) );
                yield return new WaitForSeconds(1f); //not strictly required but why not?
            }
        }
    }

    //optionally have your game object call this function rather than Destroy to keep it instantated for reuse...
    //optionally just call Destroy, entire up to you...
    public void OnCreatureDied(Transform mob)
    {
        if (!m_Spawned.Contains(mob)) return;
        string nm = mob.name.Replace("(Clone)","");
        m_Spawned.Remove(mob);
        m_Keep[nm].Add(mob);
        mob.gameObject.SetActive(false);
    }
}
