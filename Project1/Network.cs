using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    public class Network
    {
        public List<Level> levels = new List<Level> { };
        public Network(int[] neuronCounts)
        {
            for(int i = 0; i<neuronCounts.Length-1;i++)
            {
                this.levels.Add(new Level(neuronCounts[i], neuronCounts[i+1]));
            }
        }
        public static float[] FeedForward(float[] givenInputs,Network network)
        {
            float[] outputs = Level.FeedForwardAlg(givenInputs, network.levels[0]);
            for(int i=1; i<network.levels.Count(); i++)
            {
                outputs = Level.FeedForwardAlg(outputs, network.levels[i]);
            }
            return outputs;
        }

    }
/*    https://towardsdatascience.com/building-a-neural-network-framework-in-c-16ef56ce1fef
    https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/*/
    public class Level
    {
        public float[] inputs;
        public float[] outputs;
        public float[] biases;
        public float[][] weights;
        public Level(int inputCount, int outputCount)
        {
            this.inputs=new float[inputCount];
            this.outputs=new float[outputCount];
            this.biases=new float[outputCount];
            this.weights=new float[inputCount][];
            for (int i = 0; i<inputCount; i++)
            {
                this.weights[i]=new float[outputCount];
                //this.weights

            }
            Level.RandomizeLevel(this);
        }
        private static void RandomizeLevel(Level level)
        {
            Random rand = new();
            for (int i = 0; i < level.inputs.Length; i++)
            {
                for (int j = 0; j < level.outputs.Length; j++)
                {
                    level.weights[i][j]=(float)rand.NextDouble()*2-1;
                }
            }
            for (int i = 0; i < level.biases.Length; i++)
            {
                level.biases[i]=(float)rand.NextDouble()*2-1;
            }
        }
        public static float[] FeedForwardAlg(float[] givenInputs, Level level)
        {
            for (int i = 0; i < level.inputs.Length; i++)
            {
                level.inputs[i]=givenInputs[i];
            }
            for (int i = 0; i < level.outputs.Length; i++)
            {
                float sum = 0;
                for (int j = 0; j<level.inputs.Length; j++)
                {
                    sum+=level.inputs[j]*level.weights[j][i];
                }
                if (sum>level.biases[i])
                {
                    level.outputs[i]=1;
                }
                else
                {
                    level.outputs[i]=0;
                }
            }
            return level.outputs;
        }
    }
}


