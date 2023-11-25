

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    Impossible,
}


public class LevelManager_ColorBird : MonoBehaviour
{
private enum State
{
    WaitingToStart,
    Playing,
    BirdDead,
}
    private static LevelManager_ColorBird instance;

    public static LevelManager_ColorBird Instance()
    {
        return instance;
    }
    private const float PIPE_WIDTH = 3f;
    private const float PIPE_HEAD_HEIGHT = 0.25f;
    private const float CAMERA_ORTHO_SIZE = 5f;
    private const float PIPE_MOVE_SPEED = 2f;
    private const float PIPE_x_DESTROY = -10f;
    private const float PIPE_x_SPAWNPOS = 10f;
     private const float GROUND_x_DESTROY = -50f;
    private const float GROUND_x_SPAWNPOS = 10f;
       private const float ClOUD_x_DESTROY = -10f;
    private const float ClOUD_x_SPAWNPOS = 13f;
    private const float BIRD_XPOS=-6f;
    private List<Transform>groundList;
     private List<Transform>cloudList;
    private List<Pipe> pipeList;
    private int pipeSpwaned=0;
    private int pipePassedCount=0;  
    private float spawnTimer;
    private float pipeSpawnTimerMax=3f ;
    private float cloudSpawnTimer;
    private float gapSize=3f;
    private State state;
 
    private void Awake()
    {
        instance=this;
        pipeList = new List<Pipe>();
        groundList= new List<Transform>();
        cloudList= new List<Transform>();
        SpawnGround();
        SpawnCloud();
        SetDifficulty();
        state=State.WaitingToStart;

    }
    private void Start()
    {
       BirdController_ColorBird.Instance().onDied+=Bird_Ondied;
       BirdController_ColorBird.Instance().onStartPlaying+=Level_OnStartPlaying;
    }

    private void Level_OnStartPlaying(object sender, EventArgs e)
    {
       state=State.Playing;
    }
    private void Bird_Ondied(object sender, EventArgs e)
    {
        state=State.BirdDead;
        GameOverWIndow_ColorBird.Instance().Show();
    }

    private void Update()
    {
        if(state ==State.Playing)
        {

        HandlePiepMovement();
        HandlePipeSpawnTimer(); 
        HandleGroundMovement();
        HandleCloud();
        }
        
    }

    private void HandleCloud()
    {
        cloudSpawnTimer -= Time.deltaTime;
        if (cloudSpawnTimer <= 0)
        {
            float cloudSpawnTimerMax = 5f;
            cloudSpawnTimer = cloudSpawnTimerMax;
            float cloudY = 3f;
            Transform cloudTransform = Instantiate(GetCloudRandom(), new Vector3(ClOUD_x_SPAWNPOS, cloudY, 0), Quaternion.identity);
            cloudList.Add(cloudTransform);
        }
        for (int i = 0; i < cloudList.Count; i++)
        {
            Transform cloud = cloudList[i];
            cloud.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime * 0.5f;
            if (cloud.position.x < ClOUD_x_DESTROY)
            {
                Destroy(cloud.gameObject);
                cloudList.RemoveAt(i);
                i--;
            }

        }
    }
    private Transform GetCloudRandom()
    {
        switch(UnityEngine.Random.Range(0,3))
        {
            default:
            case 0: return GameAssets_ColorBird.Instance().Cloud1;
            case 1: return GameAssets_ColorBird.Instance().Cloud2;
            case 2: return GameAssets_ColorBird.Instance().Cloud3;
        }
    }
    private void SpawnCloud()
    {
         Transform cloudTransform ;
        float cloudY=3f;
        
        cloudTransform= Instantiate(GetCloudRandom(),new Vector3(0,cloudY,0),Quaternion.identity);
        cloudList.Add(cloudTransform);
    }
    private void SpawnGround()
    {
        Transform groundTransform ;
        float groundY=-5f;
        float groundx=31.2f;
        groundTransform= Instantiate(GameAssets_ColorBird.Instance().Ground,new Vector3(0,groundY,0),Quaternion.identity);
        groundList.Add(groundTransform);
         groundTransform= Instantiate(GameAssets_ColorBird.Instance().Ground,new Vector3(groundx,groundY,0),Quaternion.identity);
        groundList.Add(groundTransform);
         groundTransform= Instantiate(GameAssets_ColorBird.Instance().Ground,new Vector3(groundx*2f,groundY,0),Quaternion.identity);
        groundList.Add(groundTransform);
    }
    private void HandleGroundMovement()
    {
       foreach(Transform ground in groundList)
       {
        ground.position+=new Vector3(-1,0,0)*PIPE_MOVE_SPEED*Time.deltaTime;
        if(ground.position.x<GROUND_x_DESTROY)
        {
            float posx=-40f;
            for(int i=0;i<groundList.Count;i++)
            {
                if(groundList[i].position.x>posx)
                {
                    posx=groundList[i].position.x;
                }
            }
            float groundhalf=31.2f;
            ground.position= new Vector3(posx+groundhalf,ground.position.y,ground.position.z);
        }
       }
    }

    private void HandlePipeSpawnTimer()
    {
        spawnTimer-=Time.deltaTime;
        if(spawnTimer<0)
        {
            spawnTimer=pipeSpawnTimerMax;
            float heightLimit=0.5f;
            float minHeight=gapSize*.5f+heightLimit;
            float totalHeight=CAMERA_ORTHO_SIZE*2f;
            float maxHeight=totalHeight-gapSize*0.5f;
            float height=UnityEngine.Random.Range(minHeight,maxHeight);
           CreateGraphPipe(height,gapSize, PIPE_x_SPAWNPOS);
        }
    }

    private void HandlePiepMovement()
    {
        for (int i = 0; i < pipeList.Count; i++)
        {
            Pipe piep = pipeList[i];
            bool isRightOfBird=piep.GetXPostion()>BIRD_XPOS;
            piep.Move();
            if(isRightOfBird && piep.GetXPostion()<=BIRD_XPOS && piep.isBottom())
            {
                pipePassedCount++;
               AudioManager_ColorBird.instance.PlaySFX(4);
            }
            if (piep.GetXPostion() <= PIPE_x_DESTROY)
            {
                piep.DestroyPipe();
                pipeList.Remove(piep);
                i--;
            }

        }

    }

    private void CreatePipe(float Height, float xPostion, bool creatBottom)
    {
        Transform pipebody = Instantiate(GameAssets_ColorBird.Instance().bodyPipe);
        Transform pipeHead = Instantiate(GameAssets_ColorBird.Instance().headPipe);
        float pipeHeadYPos, pipeBodyYPos;
        if (creatBottom)
        {
            pipeHeadYPos = -CAMERA_ORTHO_SIZE + Height - PIPE_HEAD_HEIGHT * 0.5f;
            pipeBodyYPos = -CAMERA_ORTHO_SIZE;
        }
        else
        {
            pipeHeadYPos = +CAMERA_ORTHO_SIZE - Height + PIPE_HEAD_HEIGHT * 0.5f;
            pipeBodyYPos = +CAMERA_ORTHO_SIZE;
            pipebody.localScale = new Vector3(0.3f, -1, 1);
        }
        pipeHead.position = new Vector3(xPostion, pipeHeadYPos);

        pipebody.position = new Vector3(xPostion, pipeBodyYPos);

        SpriteRenderer pipeBodySprire = pipebody.GetComponent<SpriteRenderer>();
        pipeBodySprire.size = new Vector2(PIPE_WIDTH, Height);
        BoxCollider2D pipeCollider = pipebody.GetComponent<BoxCollider2D>();
        pipeCollider.size = new Vector2(PIPE_WIDTH, Height);
        pipeCollider.offset = new Vector2(0f, Height * 0.5f);
        Pipe pipe = new Pipe(pipeHead, pipebody,creatBottom);
        pipeList.Add(pipe);

    }
    private void CreateGraphPipe(float gapY, float gapSize, float xPostion)
    {
        CreatePipe(gapY - gapSize * 0.5f, xPostion, true);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * 0.5f, xPostion, false);
        pipeSpwaned++;
        SetDifficulty();
    }

    private Difficulty SetDifficulty()
    {
        if (pipeSpwaned >= 20)
        {
            pipeSpawnTimerMax=2.5f;
            gapSize = 2.5f;
            return Difficulty.Medium;
        }
        else if (pipeSpwaned >= 35)
        {
            pipeSpawnTimerMax=2f;
            gapSize = 1.75f;
            return Difficulty.Hard;
        }
        else if (pipeSpwaned >= 45)
        {
            pipeSpawnTimerMax=1.5f;
            gapSize = 1f;
            return Difficulty.Impossible;
        }
        else
        {
            pipeSpawnTimerMax=3f;
            gapSize=3f;
            return Difficulty.Easy;
        }

    }
    public int GetPipeSpwaned()
    {
       
        return pipeSpwaned;
    }
    public int GetPipePassedCount()
    {   
        return pipePassedCount;
    }
    private class Pipe
    {
        private Transform pipeHeadTransform;
        private Transform pipebodyTransform;
        private bool creatBottom;
        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform,bool creatBottom)
        {
            this.pipebodyTransform = pipeBodyTransform;
            this.pipeHeadTransform = pipeHeadTransform;
            this.creatBottom=creatBottom;
        }
        public void Move()
        {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
            pipebodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;

        }
        public float GetXPostion()
        {
            return pipeHeadTransform.position.x;
        }
        public void DestroyPipe()
        {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipebodyTransform.gameObject);

        }
        public bool isBottom()
        {
            return creatBottom;
        }
    }
    

}






