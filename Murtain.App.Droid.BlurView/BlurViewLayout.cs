using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;
using Android.Content.Res;
using Android.Graphics.Drawables;
using System.Threading.Tasks;

namespace Murtain.App.Droid.BlurView
{

    public class BlurViewLayout : RelativeLayout
    {

        private const int ALPHA_MAX_VALUE = 255;

        private Context context;

        private ImageView blurImage;
        private ImageView originImage;

        private Bitmap originBitmap;
        private Bitmap blurBitmap;

        private bool isDisableBlur;
        private bool isMove;

        public BlurViewLayout(Context context)
            : base(context)
        {
            init(context);
        }
        public BlurViewLayout(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init(context);
            initAttr(context, attrs);
        }
        public BlurViewLayout(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            init(context);
            initAttr(context, attrs);
        }

        private void init(Context context)
        {
            this.context = context;
            LayoutInflater.From(context).Inflate(Resource.Layout.BlurView, this);
            originImage = (ImageView)FindViewById(Resource.Id.BlurViewOriginImageView);
            blurImage = (ImageView)FindViewById(Resource.Id.BlurViewBlurImageView);
        }
        private void initAttr(Context context, IAttributeSet attrs)
        {

            TypedArray typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.blur_layout);
            Drawable drawable = typedArray.GetDrawable(Resource.Styleable.blur_layout_src);
            isMove = typedArray.GetBoolean(Resource.Styleable.blur_layout_move, false);
            isDisableBlur = typedArray.GetBoolean(Resource.Styleable.blur_layout_disabled, false);

            typedArray.Recycle();

            if (null != drawable)
            {
                originBitmap = BlurBitmap.DrawableToBitmap(drawable);
                Task.Factory.StartNew(() =>
                {
                    blurBitmap = BlurBitmap.Blur(context, originBitmap);
                })
                .ContinueWith(task =>
                {

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            if (!isDisableBlur)
            {
                blurImage.Visibility = ViewStates.Visible;
            }

            if (null != drawable)
            {
                setMove(context, isMove);
            }
        }
        /// <summary>
        /// 设置背景图片移动效果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isMove"></param>
        private void setMove(Context context, bool isMove)
        {
            if (isMove)
            {
                IWindowManager wm = (IWindowManager)context.GetSystemService(Context.WindowService);
                Display display = wm.DefaultDisplay;
                Point point = new Point();
                display.GetSize(point);
                int height = point.Y;
                setBlurHeight(height, originImage);
                setBlurHeight(height, blurImage);
            }
        }
        /// <summary>
        /// 改变图片的高度
        /// </summary>
        /// <param name="height"></param>
        /// <param name="imageView"></param>
        private void setBlurHeight(int height, ImageView imageView)
        {
            ViewGroup.LayoutParams parameters = imageView.LayoutParameters;
            parameters.Width = ViewGroup.LayoutParams.MatchParent;
            parameters.Height = height + 100;
            imageView.RequestLayout();
        }
        /// <summary>
        /// 填充ImageView
        /// </summary>
        private void setImageView()
        {
            blurImage.SetImageBitmap(blurBitmap);
            originImage.SetImageBitmap(originBitmap);
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();

            setImageView();
        }

        /// <summary>
        /// 以代码的方式添加待模糊的图片
        /// </summary>
        /// <param name="blurBitmap"></param>
        public void SetBlurImage(Bitmap blurBitmap)
        {
            if (null != blurBitmap)
            {
                originBitmap = blurBitmap;
                Task.Factory.StartNew(() =>
                {
                    this.blurBitmap = BlurBitmap.Blur(context, blurBitmap);
                })
                .ContinueWith(task =>
                {
                    setImageView();
                    setMove(context, isMove);
                }, TaskScheduler.FromCurrentSynchronizationContext());

            }
        }
        /// <summary>
        /// 以代码的方式添加待模糊的图片
        /// </summary>
        /// <param name="blurDrawable"></param>
        public void SetBlurImage(Drawable blurDrawable)
        {
            if (null != blurDrawable)
            {
                originBitmap = BlurBitmap.DrawableToBitmap(blurDrawable);
                Task.Factory.StartNew(() =>
                {
                    blurBitmap = BlurBitmap.Blur(context, originBitmap);
                })
                .ContinueWith(task =>
                {
                    setImageView();
                    setMove(context, isMove);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        /// <summary>
        /// 设置模糊程度
        /// </summary>
        /// <param name="level"></param>
        public void SetBlurLevel(int level)
        {
            if (level < 0 || level > 100)
            {
                throw new Java.Lang.IllegalStateException("No validate level, the value must be 0~100");
            }
            if (isDisableBlur)
            {
                return;
            }
            originImage.Alpha = (float)(100 - level) / 100;
        }
        /// <summary>
        /// 设置图片上移的距离
        /// </summary>
        /// <param name="hight"></param>
        public void SetBlurTop(int hight)
        {
            originImage.Top = -hight;
            blurImage.Top = -hight;
        }

        /// <summary>
        /// 显示模糊图片
        /// </summary>
        public void ShowBlurView()
        {
            blurImage.Visibility = ViewStates.Visible;
        }
        /// <summary>
        /// 禁用模糊效果
        /// </summary>
        public void DisableBlurView()
        {
            isDisableBlur = true;
            originImage.Alpha = 1;
            blurImage.Visibility = ViewStates.Invisible;
        }
        /// <summary>
        /// 启用模糊效果
        /// </summary>
        public void EnableBlurView()
        {
            isDisableBlur = false;
            blurImage.Visibility = ViewStates.Visible;
        }
    }
}