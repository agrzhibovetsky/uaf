using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace UaFootball.WebApplication
{
    /// <summary>
    /// Image resizing, cropping etc utilities
    /// </summary>
    public class BitmapHelper
    {
        /// <summary>
        /// Resize an image and save result
        /// </summary>
        /// <param name="sourceFilePath">Original file path</param>
        /// <param name="destinationFilePath">Destination file path</param>
        /// <param name="maxHeight">Max height of resized image</param>
        /// <param name="maxWidth">Max width of resized image (if max height is not specified)</param>
        /// <returns>Value indicating success or failure of operation</returns>
        public bool ResizeImage(string sourceFilePath, string destinationFilePath, int? maxHeight, int? maxWidth)
        {
            if (sourceFilePath != null && destinationFilePath != null && (maxHeight.HasValue || maxWidth.HasValue))
            {
                Image originalImg = Image.FromFile(sourceFilePath);

                float aspectRatio = (float)originalImg.Width / originalImg.Height;
                int originalWidth = originalImg.Width;
                int originalHeight = originalImg.Height;
                bool needsResize = true;
                int newWidth =0, newHeight = 0;

                if (maxHeight.HasValue)
                {
                    if (originalHeight <= maxHeight.Value) needsResize = false;
                    else
                    {
                        newHeight = maxHeight.Value;
                        newWidth = (int)Math.Round(newHeight * aspectRatio);
                    }
                }
                else
                {
                    if (originalWidth <= maxWidth.Value) needsResize = false;
                    else
                    {
                        newWidth = maxWidth.Value;
                        newHeight = (int)Math.Round(newWidth / aspectRatio);
                    }
                }

                try
                {
                    if (needsResize)
                    {
                        Bitmap bmp = new Bitmap(originalImg, newWidth, newHeight);
                        Graphics canvas = Graphics.FromImage(bmp);
                        canvas.DrawImage(bmp, 0, 0);

                        if (sourceFilePath.ToLower().IndexOf(".jpg") > 0)
                            bmp.Save(destinationFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        else
                            bmp.Save(destinationFilePath);

                        originalImg.Dispose();
                        canvas.Dispose();
                        bmp.Dispose();
                    }
                    else
                    {
                        originalImg.Dispose();
                        System.IO.File.Copy(sourceFilePath, destinationFilePath);
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}