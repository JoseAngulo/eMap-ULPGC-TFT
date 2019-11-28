using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesCalculator : MonoBehaviour
{
    public float miny;
    public float minx;
    public float maxy;
    public float maxx;



    void Start()
    {
        Debug.Log("=== DISTANCIAS === \n");
        Debug.Log("distancia Y = " + (maxy - miny));
        
        Debug.Log("distancia / 2 Y = " + (maxy - miny)/2);
        Debug.Log("distancia X = " + (maxx + maxy));
        Debug.Log("distancia / 2 X = " + (maxx - minx)/2);

        Debug.Log("=== PUNTOS MEDIOS === \n");

        Debug.Log("punto medio Y = " + (miny + ((maxy - miny)) / 2));

        

        Debug.Log("punto medio X = " + (minx + ((maxx - minx)) / 2));

        float puntoMedioX = minx + (maxx - minx) / 2;
        float puntoMedioY = miny + (maxy - miny) / 2;
        Debug.Log("=== COORDENADAS DE SUBMAPAS === \n");

    Debug.Log("Map_0");
        Debug.Log("MinY: " + puntoMedioY);
        Debug.Log("MinX: " + minx);
        Debug.Log("MaxY: " + maxy);
        Debug.Log("MaxX: " + puntoMedioX);

    Debug.Log("Map_1");
        Debug.Log("MinY: " + puntoMedioY);
        Debug.Log("MinX: " + puntoMedioX);
        Debug.Log("MaxY: " + maxy);
        Debug.Log("MaxX: " + maxx);

    Debug.Log("Map_2");
        Debug.Log("MinY: " + miny);
        Debug.Log("MinX: " + minx);
        Debug.Log("MaxY: " + puntoMedioY);
        Debug.Log("MaxX: " + puntoMedioX);

     Debug.Log("Map_3");
        Debug.Log("MinY: " + miny);
        Debug.Log("MinX: " + puntoMedioX);
        Debug.Log("MaxY: " + puntoMedioY);
        Debug.Log("MaxX: " + maxx);





        /*
        distancia / 2 Y = 0.00815
        distancia X = 0.0143
        distancia / 2 X = 0.00715

        punto medio Y = 28.07615
        punto medio X = -15.45145*/




    }

}
