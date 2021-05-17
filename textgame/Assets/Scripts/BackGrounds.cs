using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] backGrounds;
    public float fparallax = 0.4f;//�����ƶ�
    public float layerFraction = 5f;//ÿ���ƶ�
    public float fSmooth = 5f;//���ڲ�ֵƽ���ƶ�
    Transform cam;
    Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        float fParrlaxX = (previousCamPos.x - cam.position.x) * fparallax;
        for (int i = 0; i < backGrounds.Length; i++)
        {
            float FNewX = backGrounds[i].position.x + fParrlaxX * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(FNewX, backGrounds[i].position.y, backGrounds[i].position.z);
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newPos, fSmooth * Time.deltaTime);

        }
        previousCamPos = cam.position;
    }
}
