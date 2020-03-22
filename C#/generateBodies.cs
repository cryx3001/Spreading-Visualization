using UnityEngine;
using Random = UnityEngine.Random;

public class generateBodies : MonoBehaviour
{
    public int sizeBody;
    public GameObject Body;
    public GameObject LimitBar;
    public int numberBodies;
    
    void Start()
    {
        Screen.SetResolution(800,800,false);
        CreateBodies(numberBodies);
    }

    private void CreateBodies(int number)
    {
        float w = Screen.width, h = Screen.height ;
        Vector3 camBord = Camera.main.ScreenToWorldPoint(new Vector3(w, h, 0));
        print(camBord);
        
        for (int i = 0; i <= number; i++)
        {
            Vector3 vec = new Vector3(Random.Range(-camBord[0], camBord[0]), Random.Range(-camBord[1], camBord[1]), 0);
            GameObject bodyClone = Instantiate(Body, vec, Quaternion.identity);
            bodyClone.transform.localScale = new Vector3(sizeBody, sizeBody, 1);
            bodyClone.name = "Body"+ i ;

        }
        
        GameObject limitBarClone = Instantiate(LimitBar);
    }
}
