using System.Drawing;
using System.Drawing.Imaging;

namespace HeatMapTester
{
  class Program
  {
    static void Main(string[] args)
    {
        Image im = new Bitmap("TestImage/Jenna.jpg");

        float[] px = new float[]{
                                  41,
                                  72,
                                  73,
                                  73,
                                  73,
                                  73,
                                  73
                                  
                              };
        float[] py = new float[]{
                                  41,
                                  72,
                                  73,
                                  73,
                                  73,
                                  73,
                                  73
                                  
                              };


        Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(im,px, py);
                                              
        canvas.Save("Jenna.Heated.Jpg", ImageFormat.Jpeg);
    }
  }
}
