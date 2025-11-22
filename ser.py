namespace Recognizer;

public static class GrayscaleTask
{
    public static double[,] ToGrayscale(Pixel[,] original)
    {
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        double[,] grayscale = new double[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Pixel pixel = original[x, y];
                double brightness = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255.0;
                grayscale[x, y] = brightness;
            }
        }
        
        return grayscale;
    }
}
