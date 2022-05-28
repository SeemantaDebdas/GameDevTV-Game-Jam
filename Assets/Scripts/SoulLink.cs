using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLink : MonoBehaviour
{
    [SerializeField] float linkRadius = 1f;

    public event Action AllSoulsLinked;

    LinkableEntity[] linkableEntities;
    Dictionary<LinkableEntity, bool> hasLinked;
    bool isLinked = false;
    LinkableEntity currentEntity = null;
    LineRenderer lineRenderer = null;

    public bool IsLinked {get {return isLinked;}}
    public Dictionary<LinkableEntity, bool> HasLinked { get {return hasLinked;}}    

    private void Start()
    {
        linkableEntities = FindObjectsOfType<LinkableEntity>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        hasLinked = new Dictionary<LinkableEntity, bool>();

        for(int i= 0;i<linkableEntities.Length;i++)
        {
            hasLinked.Add(linkableEntities[i], false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isLinked)
            DetachLink();
        if (isLinked) return;

        LinkableEntity closestLinkableEntity = FindClosestLinkableEntity();

        if(Vector2.Distance(transform.position, closestLinkableEntity.transform.position) <= linkRadius)
        {
            if (closestLinkableEntity.TryGetComponent<AIController>(out var entityEnemy))
            {
                if (entityEnemy.IsDead)
                {
                    ShowConnectionAvailability(closestLinkableEntity);
                    if (Input.GetKeyDown(KeyCode.T))
                        EstablishLink(closestLinkableEntity);
                }
            }
            else
            {
                if (closestLinkableEntity.GetComponent<LinkableEntity>().enabled)
                {
                    ShowConnectionAvailability(closestLinkableEntity);
                    if (Input.GetKeyDown(KeyCode.T))
                        EstablishLink(closestLinkableEntity);
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }

    LinkableEntity FindClosestLinkableEntity()
    {
        float minDistanceToEntity = int.MaxValue;
        LinkableEntity closestLinkableEntity = null;
        for(int i = 0; i < linkableEntities.Length; i++)
        {
            float distanceToEntity = Vector2.Distance(transform.position, linkableEntities[i].transform.position);
            if (distanceToEntity < minDistanceToEntity)
            {
                minDistanceToEntity = distanceToEntity;
                closestLinkableEntity = linkableEntities[i];
            }
        }
        return closestLinkableEntity;
    }

    private void DetachLink()
    {
        isLinked = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        
        currentEntity.transform.parent = null;
        currentEntity.GetComponent<CircleCollider2D>().enabled = true;

        GetComponentInChildren<SpriteRenderer>().enabled = true;
        lineRenderer.enabled = true;
    }

    private void EstablishLink(LinkableEntity entity)
    {
        isLinked = true;
        hasLinked[entity] = true;

        if (CheckAllSoulsLinked())
            AllSoulsLinked?.Invoke();

        gameObject.layer = LayerMask.NameToLayer("DetectionLayer");
        currentEntity = entity;
        transform.position = entity.transform.position;
        
        entity.transform.parent = transform;
        entity.GetComponent<CircleCollider2D>().enabled = false;    
        
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        lineRenderer.enabled = false;
    }

    bool CheckAllSoulsLinked()
    {
        foreach (var pair in hasLinked)
        {
            if (pair.Value == false && !pair.Key.CompareTag("Player"))
                return false;
        }
        return true;
    }

    private void ShowConnectionAvailability(LinkableEntity entity)
    {
        print("Line Renderer called");
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, entity.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, linkRadius);
    }
}
