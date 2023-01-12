using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FishGame
{
    internal class Network
    {
        private List<Level> levels = new List<Level> { };
        public Network(int[] neuronCounts)
        {
            for(int i = 0; i<neuronCounts.Length-1;i++)
            {
                this.levels.Add(new Level(neuronCounts[i], neuronCounts[i+1]));
            }
        }
        private static float[] FeedForward(float[] givenInputs,Network network)
        {
            float[] outputs = Level.FeedForwardAlg(givenInputs, network.levels[0]);
            for(int i=1; i<network.levels.Count(); i++)
            {
                outputs = Level.FeedForwardAlg(outputs, network.levels[i]);
            }
            return outputs;
        }

    }

    class Level
    {
        private float[] inputs;
        private float[] outputs;
        private float[] biases;
        private float[][] weights;
        private Random rand = new();
        public Level(int inputCount, int outputCount)
        {
            this.inputs=new float[inputCount];
            this.outputs=new float[outputCount];
            this.biases=new float[outputCount];

            for (int i = 0; i<inputCount; i++)
            {
                this.weights[i]=new float[outputCount];
            }

            for (int i = 0; i < inputCount; i++)
            {

            }

        }
        private static void RandomizeLevel(Level level)
        {
            for (int i = 0; i < level.inputs.Length; i++)
            {
                for (int j = 0; j < level.outputs.Length; j++)
                {
                    level.weights[i][j]=(float)level.rand.NextDouble()*2-1;
                }
            }
            for (int i = 0; i < level.biases.Length; i++)
            {
                level.biases[i]=(float)level.rand.NextDouble()*2-1;
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


