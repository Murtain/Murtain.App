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
        /// 图片缩放比例
        /// </summary>
        private const float BITMAP_SCALE = 0.4f;
        /// <summary>
        /// 最大模糊度(在0.0到25.0之间)
        /// </summary>
        private const float BLUR_RADIUS = 25f;


        /// <summary>
        /// 图片模糊处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Blur(Context context, Bitmap image)
        {
            // 计算图片缩小后的长宽
            int width = (int)Math.Round(image.Width * BITMAP_SCALE);
            int height = (int)Math.Round(image.Height * BITMAP_SCALE);

            // 将缩小后的图片做为预渲染的图片。
            Bitmap inputBitmap = Bitmap.CreateScaledBitmap(image, width, height, false);
            // 创建一张渲染后的输出图片。
            Bitmap outputBitmap = Bitmap.CreateBitmap(inputBitmap);

            // 创建RenderScript内核对象
            RenderScript rs = RenderScript.Create(context);
            // 创建一个模糊效果的RenderScript的工具对象
            ScriptIntrinsicBlur blurScript = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));

            // 由于RenderScript并没有使用VM来分配内存,所以需要使用Allocation类来创建和分配内存空间。
            // 创建Allocation对象的时候其实内存是空的,需要使用copyTo()将数据填充进去。
            Allocation tmpIn = Allocation.CreateFromBitmap(rs, inputBitmap);
            Allocation tmpOut = Allocation.CreateFromBitmap(rs, outputBitmap);

            // 设置渲染的模糊程度, 25f是最大模糊度
            blurScript.SetRadius(BLUR_RADIUS);
            // 设置blurScript对象的输入内存
            blurScript.SetInput(tmpIn);
            // 将输出数据保存到输出内存中
            blurScript.ForEach(tmpOut);

            // 将数据填充到Allocation中
            tmpOut.CopyTo(outputBitmap);

            return outputBitmap;
        }
        /// <summary>
        /// 将Drawable对象转化为Bitmap对象
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
