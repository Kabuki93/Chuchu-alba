using System.Collections.Generic;
using ElseForty.splineplus.animation;
using ElseForty.splineplus.animation.api;
using ElseForty.splineplus.animation.model;
using ElseForty.splineplus.core.api;
using UnityEngine;

public class RobotManager : MonoBehaviour 
{
    public BaseFollowerClass baseFollowerClass;
    public List<RobotModel> Robots = new List<RobotModel>();
    public static RobotManager Instance { get; private set; }


    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        for (int i = 0; i < Robots.Count; i++)
        {
            var robot = Robots[i];
            var baseFollower = baseFollowerClass.GetByGameObject(robot.gameObject);
            if (baseFollower == null) Debug.Log("Can't find follower");
            else robot.baseFollower = baseFollower;

            robot.collisionRange = Random.Range(2, 5);
            robot.baseFollower.SetSpeed(Random.Range(3.5f, 8.5f));
        }
    }

    //Robots collision logic 
    void Update()
    {
        for (int i = 0; i < Robots.Count; i++)
        {
            var robot = Robots[i];
            robot.blocked = false;
            if (Physics.Raycast(robot.gameObject.transform.position, robot.gameObject.transform.forward, out RaycastHit hit, robot.collisionRange))
            {
                if (hit.collider.CompareTag("Block"))
                {
                    var go = hit.collider.gameObject;
                    var barrier = BarrierManager.Instance.FindBarrier(go);
                    if (!barrier.isOpen)
                    {
                        robot.blocked = true;
                    }

                }
            }

            if (robot.blocked) BaseFollowersAPI.Stop(robot.baseFollower);
            else BaseFollowersAPI.Animate(robot.baseFollower);
            var branch = baseFollowerClass.splinePlusClass.GetBranch(robot.baseFollower.GetBranch());

            //check if robot has reached the end of the branch, if yes bring it back to the start
            if (robot.baseFollower.GetDistance() >= branch.GetBranchLength())
            {
                robot.baseFollower.SetDistance(0);
            }
        }
    }
  
}

[System.Serializable]
public class RobotModel
{
    public GameObject gameObject;
    public float collisionRange;
    [HideInInspector] public BaseFollowerModel baseFollower;
    [HideInInspector] public bool blocked;
}