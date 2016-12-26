using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Renderscripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Murtain.App.Droid.BlurView
{
    public class BlurBitmap
    {
        /// <summary>
        /// ͼƬ���ű���
        /// </summary>
        private const float BITMAP_SCALE = 0.4f;
        /// <summary>
        /// ���ģ����(��0.0��25.0֮��)
        /// </summary>
        private const float BLUR_RADIUS = 25f;


        /// <summary>
        /// ͼƬģ������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Blur(Context context, Bitmap image)
        {
            // ����ͼƬ��С��ĳ���
            int width = (int)Math.Round(image.Width * BITMAP_SCALE);
            int height = (int)Math.Round(image.Height * BITMAP_SCALE);

            // ����С���ͼƬ��ΪԤ��Ⱦ��ͼƬ��
            Bitmap inputBitmap = Bitmap.CreateScaledBitmap(image, width, height, false);
            // ����һ����Ⱦ������ͼƬ��
            Bitmap outputBitmap = Bitmap.CreateBitmap(inputBitmap);

            // ����RenderScript�ں˶���
            RenderScript rs = RenderScript.Create(context);
            // ����һ��ģ��Ч����RenderScript�Ĺ��߶���
            ScriptIntrinsicBlur blurScript = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));

            // ����RenderScript��û��ʹ��VM�������ڴ�,������Ҫʹ��Allocation���������ͷ����ڴ�ռ䡣
            // ����Allocation�����ʱ����ʵ�ڴ��ǿյ�,��Ҫʹ��copyTo()����������ȥ��
            Allocation tmpIn = Allocation.CreateFromBitmap(rs, inputBitmap);
            Allocation tmpOut = Allocation.CreateFromBitmap(rs, outputBitmap);

            // ������Ⱦ��ģ���̶�, 25f�����ģ����
            blurScript.SetRadius(BLUR_RADIUS);
            // ����blurScript����������ڴ�
            blurScript.SetInput(tmpIn);
            // ��������ݱ��浽����ڴ���
            blurScript.ForEach(tmpOut);

            // ��������䵽Allocation��
            tmpOut.CopyTo(outputBitmap);

            return outputBitmap;
        }
        /// <summary>
        /// ��Drawable����ת��ΪBitmap����
        /// </summary>
        /// <param name="drawable"></param>
        /// <returns></returns>
        public static Bitmap DrawableToBitmap(Drawable drawable)
        {
            Bitmap bitmap;

            if (drawable is BitmapDrawable) {
                BitmapDrawable bitmapDrawable = (BitmapDrawable)drawable;
                if (bitmapDrawable.Bitmap != null)
                {
                    return bitmapDrawable.Bitmap;
                }
            }

            if (drawable.IntrinsicWidth <= 0 || drawable.IntrinsicHeight <= 0)
            {
                bitmap = Bitmap.CreateBitmap(1, 1,Bitmap.Config.Argb8888); // Single color bitmap will be created of 1x1 pixel
            }
            else
            {
                bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight,
                        Bitmap.Config.Argb8888);
            }

            Canvas canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);
            return bitmap;
        }
    }
}
