using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;


public class AddScript : MonoBehaviour
{
    public GameObject andy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //タップの検出
        Touch touch; if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            return; //画面に触れていないor既に触れている最中なら何もしない
                    //タップした座標にAndyを移動。対象は認識した平面 
        TrackableHit hit;
        TrackableHitFlags filter = TrackableHitFlags.PlaneWithinPolygon;
        if (Frame.Raycast(touch.position.x, touch.position.y, filter, out hit))
        {

            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(Camera.main.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) > 0)
            {
                andy.transform.position = hit.Pose.position;
                andy.transform.rotation = hit.Pose.rotation;
                andy.transform.Rotate(0, 180, 0, Space.Self);
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                andy.transform.parent = anchor.transform;
                Destroy(this);
            }
        }
    }
}
