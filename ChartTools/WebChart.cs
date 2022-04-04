using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.UI.WebControls;

namespace ChartTools
{
    public class WebChart
    {
        /// <summary>
        /// ����һ���D�����(Bitmap) 
        /// </summary>
        /// <param name="G">�L�D���</param>
        /// <param name="BMP">BMP���</param>
        /// <param name="width">Ӱ�񌒶�</param>
        /// <param name="height">Ӱ��߶�</param>
        public static void drawInit(ref Graphics G, ref Bitmap BMP, float width, float height)
        {
            //����BMP���� 
            BMP = new Bitmap(Convert.ToInt32(width), Convert.ToInt32(height), PixelFormat.Format24bppRgb);
            G = Graphics.FromImage(BMP);
            //�Pˢ 
            SolidBrush myBrush = new SolidBrush(Color.FromArgb(0, 224, 224, 224));

            //���ȫ���^�� 
            myBrush.Color = Color.FromArgb(255, 224, 224, 224);
            G.FillRectangle(myBrush, 0, 0, width, height);
        }

        public static void ShowImage(Bitmap BMP, System.Web.UI.WebControls.Image image)
        {
            //ȡ�y������ key
            string ImageKey = "IMG_" + Guid.NewGuid().ToString();
            image.Page.Application[ImageKey] = BMP;
            image.ImageUrl = "~/ImageSerivces.ashx?ImageKey=" + ImageKey;
        }
    }
}
