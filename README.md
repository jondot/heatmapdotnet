# Heatmap.Net
Implementation of a heatmap generation algorithm based on .Net.
Moved in from codeplex.

## Usage
The heat is caclulated based on number of 'hits'. Every hit is represented on an x,y coordinate in an image,
it is divided into 2 arrays of Xs and Ys out of performance considerations.
Usage is simple:

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


    Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(im, px, py);
                                          
    canvas.Save("Jenna.Heated.Jpg", ImageFormat.Jpeg);
    
    
