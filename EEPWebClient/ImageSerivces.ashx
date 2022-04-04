<%@ WebHandler Language="C#" Class="ImageSerivces" %>

using System;
using System.Web;

public class ImageSerivces : IHttpHandler 
{    
    public void ProcessRequest(HttpContext context)
    {
        //�O���W���� 
        context.Response.ContentType = "image/Jpeg";

        //�� key ץ�� session �еĈD�n 
        System.Drawing.Bitmap BMP;
        string key = context.Request["ImageKey"].ToString();
        //�xȡ�D�n 
        BMP = context.Application[key] as System.Drawing.Bitmap;
        //ጷ�ӛ���w 
        context.Application[key] = null;

        //̎��D��Ʒ�| 
        System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        System.Drawing.Imaging.ImageCodecInfo ici = null;
        //�ҳ�Encoder 
        foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs)
        {
            if (codec.MimeType == "image/jpeg")
            {
                ici = codec;
            }
        }
        //���� - ��Ʒ�|�D�n 
        System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters();
        ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);

        if (ici == null | ep == null)
        {
            //����-��Ʒ�| 
            BMP.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        else
        {
            //����-��Ʒ�| 
            BMP.Save(context.Response.OutputStream, ici, ep);
        }
        //ጷ� 
        BMP.Dispose();
        //�Y�� 
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}