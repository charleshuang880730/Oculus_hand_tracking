using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// structure
[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool debugMode = true;
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;

    // Start is called before the first frame update
    void Start()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
    }

    // Update is called once per frame
    void Update()
    {
        if(debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }
        Gesture currentgesture = Recognize();
        bool hasRecognized = !currentgesture.Equals(new Gesture());

        if(hasRecognized && !currentgesture.Equals(previousGesture))
        {
            Debug.Log("NEW GESTURE FOUND : " + currentgesture.name);
            previousGesture = currentgesture;
            currentgesture.onRecognized.Invoke();
        }

    }

     void Save()
     {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach(var bone in fingerBones)
        {
            // finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerDatas = data;
        gestures.Add(g);
     }

     Gesture Recognize()
     {
        Gesture currentgesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance (currentData, gesture. fingerDatas [i]);

                if(distance > threshold)
                {
                    isDiscarded = true;
                    break;
                }

                sumDistance += distance;
            }

            if(!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentgesture = gesture;
            }
        }
        return currentgesture;
    }

    void left_hand_moving()
    {
        List<Vector3> indexFingerPositions = new List<Vector3>();
        List<Vector3> pinkyFingerPositions = new List<Vector3>();

        foreach(var bone in fingerBones)
        {
            // finger position relative to root
            Debug.Log(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        // 獲取食指的各個關節點的座標
        Vector3 index1Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index1].Transform.position;
        Vector3 index2Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index2].Transform.position;
        Vector3 index3Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index3].Transform.position;

        // 獲取小拇指的各個關節點的座標
        Vector3 pinky0Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky0].Transform.position;
        Vector3 pinky1Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky1].Transform.position;
        Vector3 pinky2Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky2].Transform.position;
        Vector3 pinky3Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky3].Transform.position;

        // 將這些座標添加到相應的列表中
        indexFingerPositions.Add(index1Position);
        indexFingerPositions.Add(index2Position);
        indexFingerPositions.Add(index3Position);

        pinkyFingerPositions.Add(pinky0Position);
        pinkyFingerPositions.Add(pinky1Position);
        pinkyFingerPositions.Add(pinky2Position);
        pinkyFingerPositions.Add(pinky3Position);
    }

    void right_hand_moving()
    {
        List<Vector3> indexFingerPositions = new List<Vector3>();
        List<Vector3> pinkyFingerPositions = new List<Vector3>();

        foreach(var bone in fingerBones)
        {
            // finger position relative to root
            Debug.Log(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        // 獲取食指的各個關節點的座標
        Vector3 index1Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index1].Transform.position;
        Vector3 index2Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index2].Transform.position;
        Vector3 index3Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index3].Transform.position;

        // 獲取小拇指的各個關節點的座標
        Vector3 pinky0Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky0].Transform.position;
        Vector3 pinky1Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky1].Transform.position;
        Vector3 pinky2Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky2].Transform.position;
        Vector3 pinky3Position = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Pinky3].Transform.position;

        // 將這些座標添加到相應的列表中
        indexFingerPositions.Add(index1Position);
        indexFingerPositions.Add(index2Position);
        indexFingerPositions.Add(index3Position);

        pinkyFingerPositions.Add(pinky0Position);
        pinkyFingerPositions.Add(pinky1Position);
        pinkyFingerPositions.Add(pinky2Position);
        pinkyFingerPositions.Add(pinky3Position);
    }

}
