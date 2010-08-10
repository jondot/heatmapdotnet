using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace HeatMap.NET
{
  public class HeatMap
  {
    public static Image GenerateHeatMap(Image baseImage, float[] X, float[] Y)
    {
      // Create canvas the size of the page
      Image canvas = new Bitmap(baseImage.Width, baseImage.Height);

      // Load the dot-Image
        Image pt = Resources.heatdot;

      // Initialize Graphics object to work on the canvas
      Graphics g = Graphics.FromImage(canvas);
      g.Clear(Color.White);


      for (int i = 0; i < X.Length; i++)
      {
        g.DrawImage(pt, X[i] - pt.Width / 2, Y[i] - pt.Height / 2);
      }


      // Create a new ImageAttributes object to manipulate the Image
      ImageAttributes imageAttributes = new ImageAttributes();
      ColorMap[] remapTable = new ColorMap[255];

      // Replace OldColor with a NewColor for all color-codes from 0,0,0 to 75, 75, 75 (RGB) 
      // (From black to dark-gray)
      for (int i = 0; i < 75; i++)
      {
        ColorMap c = new ColorMap();
        c.OldColor = Color.FromArgb(i, i, i);
        c.NewColor = Color.FromArgb(255 - i, 0, 0);
        remapTable[i] = c;
      }

      // Replace OldColor with a NewColor for all color-codes from 75, 75, 75 to 200, 200, 200 (RGB) 
      // (From dark-gray to gray)
      for (int i = 75; i < 200; i++)
      {
        ColorMap c = new ColorMap();
        c.OldColor = Color.FromArgb(i, i, i);
        c.NewColor = Color.FromArgb(0, 255 - i, 0);
        remapTable[i] = c;
      }

      // Replace OldColor with a NewColor for all color-codes from 200, 200, 200 to 255, 255, 255 (RGB) 
      // (From gray to light-gray - before it gets white!)
      for (int i = 200; i < 255; i++)
      {
        ColorMap c = new ColorMap();
        c.OldColor = Color.FromArgb(i, i, i);
        c.NewColor = Color.FromArgb(0, 0, i - 100);
        remapTable[i] = c;
      }

      // Set the RemapTable on the ImageAttributes object.
      imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

      // Draw Image with the new ImageAttributes
      g.DrawImage(canvas, new Rectangle(0, 0, canvas.Width, canvas.Height), 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, imageAttributes);

      // Replace the white color with the same color as the edge of all the dots. 
      ImageAttributes ia = new ImageAttributes();
      ColorMap[] cm = new ColorMap[1];
      ColorMap cw = new ColorMap();
      cw.OldColor = Color.White;
      cw.NewColor = Color.FromArgb(0, 0, 0);
      cm[0] = cw;

      // Set the RemapTable on the new ImageAttributes object.
      ia.SetRemapTable(cm, ColorAdjustType.Bitmap);

      // Draw the Image again, with the new ImageAttributes.
      g.DrawImage(canvas, new Rectangle(0, 0, canvas.Width, canvas.Height), 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, ia);

      // Setting transparency!
      // Create a new color matrix and set the alpha value to 0.5
      ColorMatrix cam = new ColorMatrix();
      cam.Matrix00 = cam.Matrix11 = cam.Matrix22 = cam.Matrix44 = 1;
      cam.Matrix33 = Convert.ToSingle(0.5);

      // Create a new image attribute object and set the color matrix to the one just created
      ImageAttributes iaa = new ImageAttributes();
      iaa.SetColorMatrix(cam);

      // Draw the original image with the image attributes specified
      g.DrawImage(canvas, new Rectangle(0, 0, canvas.Width, canvas.Height), 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, iaa);

      //overlay heatmap
      Graphics g2 = Graphics.FromImage(baseImage);
      g2.DrawImage(canvas, new Rectangle(0, 0, canvas.Width, canvas.Height), 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, iaa);

      g2.Dispose();
      g.Dispose();

      return baseImage;
    }

  }
}
