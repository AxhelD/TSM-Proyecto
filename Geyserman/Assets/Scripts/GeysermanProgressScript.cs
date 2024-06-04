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
    public Image thirdSite;

    public float originSiteEnd;
    public float firstSiteBegining;
    public float firstSiteEnd;
    public float secondSiteBegining;
    public float secondSiteEnd;
    public float thirdSiteBegining;

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
        if (player.transform.position.x > originSiteEnd && player.transform.position.x < firstSiteBegining)
        {
            distanceToPlayer(originSite, firstSite, originSiteEnd, firstSiteBegining);
        }
        else if (player.transform.position.x > firstSiteEnd && player.transform.position.x < secondSiteBegining)
        {
            distanceToPlayer(firstSite, secondSite, firstSiteEnd, secondSiteBegining);
        }
        else if (player.transform.position.x > secondSiteEnd && player.transform.position.x < thirdSiteBegining)
        {
            distanceToPlayer(secondSite, thirdSite, secondSiteEnd, thirdSiteBegining);
        }
    }

    void distanceToPlayer(Image siteFromMove, Image siteToMove, float distanceFromSite, float distanceToSite)
    {
        distanceToPoint = Mathf.Abs(distanceToSite - distanceFromSite);
        distanceToTarget = Mathf.Abs(player.transform.localPosition.x - distanceToSite);
        distanceToIcon = Vector3.Distance(siteFromMove.rectTransform.localPosition, siteToMove.rectTransform.localPosition);

        currentDistance = Map(distanceToTarget, distanceToPoint, 0, distanceToIcon, 0);
        
        iconBarProgress.rectTransform.localPosition = new Vector2(siteToMove.rectTransform.localPosition.x - currentDistance,  iconBarProgress.rectTransform.localPosition.y);

    }

    static public float Map(float value, float istart, float istop, float ostart, float ostop)
    {
        return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
    }
}
