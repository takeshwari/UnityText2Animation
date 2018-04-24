using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI; 
public class Controller : MonoBehaviour {

    GameObject subject;
    GameObject target;
    GameObject second_target; 
    public InputField input;
    public string sentence;
    Parser parser;
    Camera cam;
    public float moveSpeed = 10f;
    Vector3 tempPos;
    Vector3 targetPos;
    Vector3 cameraPos;
    Animation anim; 


    void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        parser = new Parser();

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    public void getInput()

    {
        sentence = input.text; 
        Debug.Log(sentence);

        string result = parser.Tags(sentence);
        Debug.Log(result);
        activatePrefab(); 
    }

    public void activatePrefab()
    {
        Debug.Log(parser.subj); 
          subject =
          Instantiate(Resources.Load(parser.subj.ToLower()),
         new Vector3(50, 0, -10),
          Quaternion.identity) as GameObject;

        cam.transform.LookAt(subject.transform);

        target =
         Instantiate(Resources.Load(parser.target.ToLower()),
         new Vector3(65, 10, -10),
         Quaternion.identity) as GameObject;

        target.transform.localScale += new Vector3(3F, 3F, 3F);



        Debug.Log(parser.target); 
    }

    void Update()

    {

   


        if (subject != null)
        {
            transform.position = subject.transform.position + 70 * Vector3.back;
            cameraPos = subject.transform.position;
            cameraPos.y = subject.transform.position.y;
            cameraPos.x = subject.transform.position.x +3;
            cameraPos.z = subject.transform.position.z - 78;
            cam.transform.LookAt(cameraPos);

            tempPos = subject.transform.position;
            targetPos = target.transform.position;


            if (tempPos.x <  targetPos.x -3)
            {
                tempPos.x += 0.09f;
                subject.transform.position = tempPos;

            }

            else if (tempPos.x >= tempPos.x -3)
            {
               
                //anim = subject.GetComponent<Animation>();
              //  anim.AddClip()

            }
     
        }
     

    }

}
