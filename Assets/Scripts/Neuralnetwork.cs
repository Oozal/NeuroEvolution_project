using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuralnetwork
{
    float[] givenInputs;
    public float[,] weights;
    public float[] outputWeight;
    float[] temphiddenLayerVal;
    float output;
    public int num_hiddenLayers;
    public Neuralnetwork(int NumHiddenlayers)
    {
        num_hiddenLayers = NumHiddenlayers;
        weights = new float[2, NumHiddenlayers];
        outputWeight = new float[NumHiddenlayers];
        temphiddenLayerVal = new float[num_hiddenLayers];

    }

    public void initializeWeight()
    {
        for(int i=0;i<2;i++)
        {
            for(int j=0;j<num_hiddenLayers;j++)
            {
                float weight = Random.Range(-1.0f, 1.0f);
                weights[i, j] = weight;
                //Debug.Log(weight);
            }
        }
        for (int i = 0; i < num_hiddenLayers; i++)
        {
            outputWeight[i] = Random.Range(-1.0f, 1.1f);
        }
        
    }

    public void fixingWeight()
    {

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < num_hiddenLayers; j++)
            {
                float fixed_val = Random.Range(-0.1f, +0.1f);
                weights[i, j] += fixed_val;
                //Debug.Log(weight);
            }
        }
        for (int i = 0; i < num_hiddenLayers; i++)
        {
            float fixed_val = Random.Range(-0.1f, +0.1f);
            outputWeight[i] += fixed_val;
        }
    }


    public float calculateValHidden(float[] input)
    {

        for(int j=0;j<num_hiddenLayers;j++)
        {
            float a = 0;
            for(int i=0;i<input.Length;i++)
            {
                a += weights[i,j] * input[i];
            }
            temphiddenLayerVal[j] = a;
            //bug.Log(a);
        }
        output = 0;
        return calculateOutput();
    }

    public float calculateOutput()
    {
        
        for(int i=0;i<num_hiddenLayers;i++)
        {
            output += temphiddenLayerVal[i] * outputWeight[i];
            //Debug.Log(output);
        }
        return output;
       
    }
    
}
