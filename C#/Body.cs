using UnityEngine;
using Random = UnityEngine.Random;

public class Body : MonoBehaviour
{
    private int type;
    private int tickSpeed = 0;
    private float[] speed = new[] {0f, 0f};
    private bool isActive;
    public float inactive_prob;
    public SpriteRenderer sprite;



    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (name == "Body0")
            ChangeType(this, 1);
        else
            ChangeType(this, 0);

        float roll = Random.Range(0, 100);
        if (roll >= inactive_prob | type == 1) 
            isActive = true;
        else
            isActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MoveBody(0.05f);
        }
    }

    private float[] GetPos()
    {
        return new[] {transform.position[0], transform.position[1]};
    }

    private void SetPos(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    private void MoveBody(float maxspeed)
    {
        float[] pos = GetPos();
        int maxTick = Random.Range(60, 100);
        
        if (tickSpeed >= maxTick)
        {
            speed = new[] {Random.Range(-maxspeed, maxspeed), Random.Range(-maxspeed, maxspeed)};
            tickSpeed = 0;
        }

        transform.position = new Vector3(pos[0] + speed[0],pos[1] + speed[1], 0);
        CheckPosition();
        tickSpeed++;
    }

    private void CheckPosition()
    {
        float[] pos = GetPos();
        float w = Screen.width, h = Screen.height ;
        Vector3 camBord = Camera.main.ScreenToWorldPoint(new Vector3(w, h, 0));
        float xDist = camBord[0] - pos[0], yDist = camBord[1] - pos[1];


        if (pos[0] <= -camBord[0])
            SetPos(pos[0] + 1.41f*camBord[0], pos[1]);
        
        else if (pos[0] >= camBord[0]*0.59f)
            SetPos(-pos[0] + xDist, pos[1]);
        
        else if (pos[1] <= -camBord[1])
            SetPos(pos[0], pos[1] + 2*camBord[1]);
        
        else if (pos[1] >= camBord[1])
            SetPos(pos[0], -pos[1]+yDist);
        
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject obj = col.gameObject;
        Body colData = obj.GetComponent<Body>();
        
        if (colData.name.Contains("Body") & (colData.type == 1 ^ type == 1))
        {
            ChangeType(this, 1);
            ChangeType(colData, 1);
        }
    }
        

    private void ChangeType(Body obj, int n)
    {
        switch (n)
        {
            case 0:
                obj.type = 0;
                obj.sprite.color = new Color(0, 1, 0);
                break;

            case 1:
                obj.type = 1;
                obj.sprite.color = new Color(1, 0, 0);
                break;

            case 2:
                obj.type = 2;
                obj.sprite.color = new Color(1, 0.39f, 0);
                break;
        }
    }
    
}
