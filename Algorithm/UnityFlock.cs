using UnityEngine;
using System.Collections;

/// <summary>
/// 鱼的集群算法
/// </summary>
public class UnityFlock : MonoBehaviour
{
    [Tooltip("最小速度")]
    public float minSpeed = 0.1f;
    [Tooltip("转向速度")]
    public float turnSpeed = 20.0f;
    [Tooltip("倒数为随机力的频率")]
    public float randomFreq = 1f;
    [Tooltip("施加随机力的大小")]
    public float randomForce = 0.2f;
    [Tooltip("跟随Leader的力的大小")]
    public float toOriginForce = 50.0f;
    [Tooltip("跟随力的范围")]
    public float toOriginRange = 100.0f;
    [Tooltip("重力的大小")]
    public float gravity = 0f;

    //游戏物体间的最小距离
    public float avoidanceRadius = 50.0f;

    
    public float avoidanceForce = 20.0f;

    //跟随速度
    public float followVelocity = 4.0f;

    //和Leader保持的最小距离
    public float followRadius = 40.0f;     

    //跟随的leader
    private Transform origin;
    //飞行速度
    private Vector3 velocity;
    //标准化速度
    private Vector3 normalizedVelocity;
    //随机推力
    private Vector3 randomPush;
    //原始推力
    private Vector3 originPush;
    private Transform[] objects;           
    //同一个集群组中的其他对象的UnityFlock集合
    private UnityFlock[] otherFlocks;

    void Start()
    {
        #region 初始化randomFreq，Leader
        //计算真实的随机力施加频率，1秒randomFreq次
        randomFreq = 1.0f / randomFreq;

        //跟随的Leader为父对象
        origin = transform.parent;
        #endregion

        #region 初始化objects和originPush数组
        //获得同一个集群内的对象
        if (origin)
        {
            otherFlocks = transform.parent.GetComponentsInChildren<UnityFlock>();
            objects = new Transform[otherFlocks.Length];
        }

        for (int i = 0; i < otherFlocks.Length; i++)
        {
            objects[i] = otherFlocks[i].transform;
        }
        #endregion

        //避免随父物体一起移动,将此物体移动到leader同一级
        transform.parent = transform.parent.parent;

        //开始一个协程用于计算随机施加的力
        StartCoroutine(UpdateRandom());
    }

    IEnumerator UpdateRandom()
    {
        while (true)
        {
            //用一个园内随机力的方向*一个随机力的大小
            randomPush = Random.insideUnitSphere * randomForce;
            //randomPush的更新时间间隔
            yield return new WaitForSeconds(randomFreq + Random.Range(-randomFreq / 2.0f, randomFreq / 2.0f));
        }
    }

    void Update()
    {
        //speed的标量
        float speed = velocity.magnitude;
        //初始化平均速度
        Vector3 avgVelocity = Vector3.zero;
        //初始化平均位置
        Vector3 avgPosition = Vector3.zero;
        //集群内其余对象的数量
        float count = 0;

        float f = 0.0f;
        float d = 0.0f;
        Vector3 myPosition = transform.position;
        Vector3 forceV;
        //该对象到平均对象的向量
        Vector3 toAvg;
        //最终的速度方向
        Vector3 wantedVel;

        for (int i = 0; i < objects.Length; i++)
        {
            Transform trans = objects[i];
            if (trans !=this.transform )
            {
                Vector3 otherPosition = trans.position;

                //通过平均位置来计算合力
                avgPosition += otherPosition;
                count++;

                //其它对象移动到本对象的向量
                forceV = myPosition - otherPosition;

                //当前对象与数组内其他对象的距离
                d = forceV.magnitude;

                //Add push value if the magnitude is less than follow radius to the leader
                if (d < followRadius)
                {
                    //calculate the velocity based on the avoidance distance between flocks 
                    //if the current magnitude is less than the specified avoidance radius
                    if (d < avoidanceRadius)
                    {
                        f = 1.0f - (d / avoidanceRadius);

                        if (d > 0)
                            avgVelocity += (forceV / d) * f * avoidanceForce;
                    }

                    //保持和leader的距离
                    f = d / followRadius;
                    UnityFlock tempOtherFlock = otherFlocks[i];
                    avgVelocity += tempOtherFlock.normalizedVelocity * f * followVelocity;
                }
            }
        }

        if (count > 0)
        {
            //计算队列力
            avgVelocity /= count;

            //计算聚合的方向
            toAvg = (avgPosition / count) - myPosition;
        }
        else
        {
            toAvg = Vector3.zero;
        }

        //Directional Vector to the leader
        forceV = origin.position - myPosition;
        d = forceV.magnitude;
        f = d / toOriginRange;

        //Calculate the velocity of the flock to the leader
        if (d > 0)
            originPush = (forceV / d) * f * toOriginForce;

        if (speed < minSpeed && speed > 0)
        {
            velocity = (velocity / speed) * minSpeed;
        }

        wantedVel = velocity;

        //想要移动的方向
        wantedVel -= wantedVel * Time.deltaTime;
        //随机力的方向
        wantedVel += randomPush * Time.deltaTime;
        //推向领头鱼的方向
        wantedVel += originPush * Time.deltaTime;
        //范围内的所有群集平均方向
        wantedVel += avgVelocity * Time.deltaTime;
        //聚合速度的方向
        wantedVel += toAvg.normalized * gravity * Time.deltaTime;

        //最终鱼的旋转方向和速度方向
        velocity = Vector3.RotateTowards(velocity, wantedVel, turnSpeed * Time.deltaTime, 100.00f);

        //旋转向当前的速度方向
        transform.rotation = Quaternion.LookRotation(velocity);

        //向合力的方向移动
        transform.Translate(velocity * Time.deltaTime, Space.World);

        //更新标准化速度
        normalizedVelocity = velocity.normalized;
    }
}