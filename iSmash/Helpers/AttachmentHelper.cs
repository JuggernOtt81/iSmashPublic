using System.IO;

namespace iSmash.Helpers
{
    public class AttachmentHelper
    {
        public static string ShowIcon(string filePath)
        {
            string iconPath;
            switch (Path.GetExtension(filePath))
            {
                case ".pdf":
                    iconPath = "/Icons/pdf.png";
                    break;

                case ".txt":
                    iconPath = "/Icons/txt.png";
                    break;

                case ".png":
                    iconPath = "/Icons/png.png";
                    break;

                case ".jpg":
                    iconPath = "/Icons/png.png";
                    break;

                default:
                    iconPath = "/Icons/unknownIcon.png";
                    break;

            }

            return iconPath;
        }
    }
}