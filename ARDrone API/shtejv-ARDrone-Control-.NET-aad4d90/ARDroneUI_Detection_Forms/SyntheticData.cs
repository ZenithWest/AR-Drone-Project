using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Windows.Forms;

namespace ARDroneUI_Detection_Forms
{
    class SyntheticData
    {
        public Matrix<float> state;
        public Matrix<float> transitionMatrix;
        public Matrix<float> measurementMatrix;
        public Matrix<float> processNoise;
        public Matrix<float> measurementNoise;
        public Matrix<float> errorCovariancePost;

        public SyntheticData()
        {
            state = new Matrix<float>(4, 1);
            state[0, 0] = Cursor.Position.X; // x-pos
            state[1, 0] = Cursor.Position.Y; // y-pos
            state[2, 0] = 0f; // x-velocity
            state[3, 0] = 0f; // y-velocity
            transitionMatrix = new Matrix<float>(new float[,]
                {
                    {1, 0, 1, 0},
                    {0, 1, 0, 1},
                    {0, 0, 1, 0},
                    {0, 0, 0, 1}
                });
            measurementMatrix = new Matrix<float>(new float[,] 
                { 
                    { 1, 0, 0, 0 }, 
                    { 0, 1, 0, 0 } 
                });
            measurementMatrix.SetIdentity();
            processNoise = new Matrix<float>(4, 4);
            processNoise.SetIdentity(new MCvScalar(1.0e-4));
            measurementNoise = new Matrix<float>(2, 2);
            measurementNoise.SetIdentity(new MCvScalar(1.5e-1));
            errorCovariancePost = new Matrix<float>(4, 4);
            errorCovariancePost.SetIdentity();
        }

        public Matrix<float> GetMeasurement()
        {
            Matrix<float> measurementNoise = new Matrix<float>(2, 1);
            measurementNoise.SetRandNormal(new MCvScalar(), new MCvScalar(Math.Sqrt(measurementNoise[0, 0])));
            return measurementMatrix * state + measurementNoise;
        }

        public void GoToNextState()
        {
            Matrix<float> processNoise = new Matrix<float>(4, 1);
            processNoise.SetRandNormal(new MCvScalar(), new MCvScalar(processNoise[0, 0]));
            state = transitionMatrix * state + processNoise;
        }
    }
}