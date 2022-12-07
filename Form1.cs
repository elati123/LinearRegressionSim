using ScottPlot;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace LinearRegressionSim
{

    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            
        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {
            // Create some linear but noisy data
            double[] ys = DataGen.NoisyLinear(null, pointCount: 100, noise: 30);
            double[] xs = DataGen.Consecutive(ys.Length);
            double x1 = xs[0];
            double x2 = xs[xs.Length - 1];

            formsPlot1.Plot.Title("Linear Regression\n");
            formsPlot1.Plot.AddScatter(xs, ys, lineWidth: 0);
            formsPlot1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // grad descent algorithm ffrom stackoverflow answer LINQ version
            double theta1calc(double[] x, double[] y, double theta1, double theta2 )
            {
                double learningRate = 0.01;
                
                double theta1Derivative = (1.0 / x.Length) * x.Zip(y, (x, y) => (theta2 * x + theta1) - y).Sum();
                double theta2Derivative = (1.0 / x.Length) * x.Zip(y, (x, y) => ((theta2 * x + theta1) - y) * x).Sum();

                
                theta1 = theta1 - learningRate * theta1Derivative;
                
                return theta1;
            }
            double theta2calc(double[] x, double[] y,double theta1, double theta2)
            {
                double learningRate = 0.01;

                double theta1Derivative = (1.0 / x.Length) * x.Zip(y, (x, y) => (theta2 * x + theta1) - y).Sum();
                double theta2Derivative = (1.0 / x.Length) * x.Zip(y, (x, y) => ((theta2 * x + theta1) - y) * x).Sum();

                theta2 = theta2 - learningRate * theta2Derivative;
                theta1 = theta1 - learningRate * theta1Derivative;

                return theta2;
            }

            double[] ys = DataGen.NoisyLinear(null, pointCount: 100, noise: 30);
            double[] xs = DataGen.Consecutive(ys.Length);
            double x1 = xs[0];
            double x2 = xs[xs.Length - 1];
            int iterations = 200;
            Random rnd = new Random();
            double theta1 = rnd.Next(0, 100);
            double theta2 = rnd.Next(0, 100);

            for(int i = 0; i < iterations; i++) {
                theta1 = theta1calc(xs, ys, theta1, theta2);
                theta2 = theta1calc(xs, ys, theta1, theta2);
                String label = String.Format("y-intercept: ", theta1, "slope: ", theta2);
                formsPlot1.Plot.Title(label);
                formsPlot1.Plot.AddLine(theta1, theta2, (x1, x2));
                formsPlot1.Refresh();
                Thread.Sleep(100);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
           
       
        }
        private void formsPlot2_Load(object sender, EventArgs e)
        {
            // Create some linear but noisy data
            double[] ys = DataGen.NoisyLinear(null, pointCount: 100, noise: 30);
            double[] xs = DataGen.Consecutive(ys.Length);
            double x1 = xs[0];
            double x2 = xs[xs.Length - 1];
            var model = new ScottPlot.Statistics.LinearRegressionLine(xs, ys);


            formsPlot2.Plot.Title("Linear Regression\n" +
                 $"Y = {model.slope:0.0000}x + {model.offset:0.0} " +
                 $"(R² = {model.rSquared:0.0000})");
            formsPlot2.Plot.AddScatter(xs, ys, lineWidth: 0);
            formsPlot2.Plot.AddLine(model.slope, model.offset, (x1, x2), lineWidth: 2);
            formsPlot2.Refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}