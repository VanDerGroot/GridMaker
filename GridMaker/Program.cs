using System;
using System.Drawing;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Define the size of the bitmap to create
        int width = 1024;
        int height = 1024;

        // Define the size of each tile in the chess board pattern
        int tileWidth = 65;
        int tileHeight = 32;

        // Define the light and dark colors for the chess board pattern
        Color lightTileColor = Color.LightGray;
        Color darkTileColor = Color.DarkGray;

        // Create a new bitmap with the specified size
        Bitmap bmp = new Bitmap(width, height);

        // Fill the bitmap with the chess board pattern
        FillChessBoardPattern(bmp, tileWidth, tileHeight, lightTileColor, darkTileColor);

        // Save the bitmap to a file
        SaveImage(bmp, "isogrid");
    }

    static void FillChessBoardPattern(Bitmap bmp, int tileWidth, int tileHeight, Color lightTileColor, Color darkTileColor)
    {
        // Define the number of rows and columns of tiles
        int numRows = bmp.Height / (tileHeight - 1);
        int numCols = bmp.Width / (tileWidth - 1);

        // Loop through each row of tiles
        for (int row = 0; row < numRows; row++)
        {
            // Calculate the y-coordinate of the center of this row of tiles
            int centerY = row * (tileHeight - 1) - bmp.Height / 2 + tileHeight / 2;

            // Loop through each column of tiles
            for (int col = 0; col < numCols; col++)
            {
                // Calculate the x-coordinate of the center of this column of tiles
                int centerX = col * (tileWidth - 1) - bmp.Width / 2 + tileWidth / 2;

                // Calculate the coordinates of the four corners of the diamond in the original coordinate system
                int x1 = centerX - tileWidth / 2;
                int y1 = centerY;
                int x2 = centerX;
                int y2 = centerY - tileHeight / 2;
                int x3 = centerX + tileWidth / 2;
                int y3 = centerY;
                int x4 = centerX;
                int y4 = centerY + tileHeight / 2;

                // Calculate the color of this tile
                Color tileColor = ((row + col) % 2 == 0) ? lightTileColor : darkTileColor;

                // Fill the diamond with the appropriate color
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillPolygon(new SolidBrush(tileColor), new Point[] {
                    new Point(x1 + bmp.Width / 2, y1 + bmp.Height / 2),
                    new Point(x2 + bmp.Width / 2, y2 + bmp.Height / 2),
                    new Point(x3 + bmp.Width / 2, y3 + bmp.Height / 2),
                    new Point(x4 + bmp.Width / 2, y4 + bmp.Height / 2)
                });
                }
            }
        }
    }




    static void SaveImage(Bitmap bmp, string filename)
    {
        // Check if the file already exists, and if so, add a numbered suffix to the filename
        int suffix = 0;
        string path = $"{filename}.png";
        while (File.Exists(path))
        {
            suffix++;
            path = $"{filename} ({suffix}).png";
        }

        // Save the bitmap to the file
        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
    }
}
