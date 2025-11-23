using System;

namespace Recognizer;

internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        
        var sy = Transpose(sx);
        var kernelSize = sx.GetLength(0);
        var offset = kernelSize / 2;
        
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (x >= offset && x < width - offset && 
                    y >= offset && y < height - offset)
                {
                    var gx = Convolve(g, sx, x, y);
                    
                    var gy = Convolve(g, sy, x, y);
                    
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
                else
                {
                    result[x, y] = 0;
                }
            }
        return result;
    }
    
    private static double[,] Transpose(double[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var result = new double[cols, rows];
        
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[j, i] = matrix[i, j];
                
        return result;
    }
    
    private static double Convolve(double[,] image, double[,] kernel, int x, int y)
    {
        double sum = 0;
        var kernelSize = kernel.GetLength(0);
        var offset = kernelSize / 2;
        
        for (int i = 0; i < kernelSize; i++)
            for (int j = 0; j < kernelSize; j++)
            {
                var imageX = x + i - offset;
                var imageY = y + j - offset;
                sum += image[imageX, imageY] * kernel[i, j];
            }
                
        return sum;
    }
}
