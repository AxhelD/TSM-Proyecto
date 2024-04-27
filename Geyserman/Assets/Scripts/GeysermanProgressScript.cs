using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeysermanProgressScript : MonoBehaviour
{
    public Image iconBarProgress;
    public Image originSite;
    public Image firstSite;
    public Image secondSite;
    public Image bossArea;

    public GameObject originSiteCollider;
    public GameObject firstSiteCollider;
    public GameObject secondSiteCollider;
    public GameObject bossSiteCollider;
    public GameObject player;

    private float distanceToTarget;
    private float distanceToIcon;
    private float distanceToPoint;
    private float currentDistance;

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (player.transform.position.x <= firstSiteCollider.transform.position.x)
        {
            distanceToPlayer(originSite, firstSite, originSiteCollider, firstSiteCollider);
        }
        else if (player.transform.position.x >= firstSiteCollider.transform.position.x && player.transform.position.x <= secondSiteCollider.transform.position.x)
        {
            distanceToPlayer(firstSite, secondSite, firstSiteCollider, secondSiteCollider);
            firstSite.enabled = false;
        }
        else if (player.transform.position.x >= secondSiteCollider.transform.position.x && player.transform.position.x <= bossSiteCollider.transform.position.x)
        {
            distanceToPlayer(secondSite, bossArea, secondSiteCollider, bossSiteCollider);
            secondSite.enabled = false;
        }
        else if (player.transform.position.x >= bossSiteCollider.transform.position.x) 
        {
            bossArea.enabled = false;
        }
      
    }

    void distanceToPlayer(Image siteFromMove, Image siteToMove, GameObject distanceFromSite, GameObject distanceToSite)
    {
        distanceToPoint = Vector3.Distance(distanceFromSite.transform.localPosition, distanceToSite.transform.localPosition);
        distanceToTarget = Mathf.Abs((player.transform.localPosition - distanceToSite.transform.localPosition).x);
        distanceToIcon = Vector3.Distance(siteFromMove.rectTransform.localPosition, siteToMove.rectTransform.localPosition);

        currentDistance = Map(distanceToTarget, distanceToPoint, 0, distanceToIcon, 0);

        if(player.transform.localPosition.x <= distanceToSite.transform.localPosition.x && player.transform.localPosition.x > distanceFromSite.transform.localPosition.x) 
        {
            iconBarProgress.rectTransform.localPosition = new Vector2(siteToMove.rectTransform.localPosition.x - currentDistance,  iconBarProgress.rectTransform.localPosition.y);
        }
    }

    static public float Map(float value, float istart, float istop, float ostart, float ostop)
    {
        return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
    }
}
