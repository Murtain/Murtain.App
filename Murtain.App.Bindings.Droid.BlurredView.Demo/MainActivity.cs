using Android.App;
using Android.Widget;
using Android.OS;
using Com.Qiushui.Blurredview;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Content.PM;
using System.Linq;
using Android.Graphics.Drawables;
using System;
using static Android.Support.V7.Widget.RecyclerView;

namespace Murtain.App.Bindings.Droid.BlurredView.Demo
{
    [Activity(Label = "BlurView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private BlurredViewLayout blurViewLayout;
        private RecyclerView recyclerView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Window.SetFlags(Android.Views.WindowManagerFlags.Fullscreen, Android.Views.WindowManagerFlags.Fullscreen);

            this.blurViewLayout = FindViewById<BlurredViewLayout>(Resource.Id.BlurView);
            this.recyclerView = FindViewById<RecyclerView>(Resource.Id.RecyclerView);

            this.recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            this.recyclerView.SetAdapter(new RecyclerAdapter(this, PackageManager.GetInstalledApplications(0).ToList()));
            this.recyclerView.SetOnScrollListener(new BlurViewOnScrollListener(this.blurViewLayout));
        }
    }
    public class BlurViewOnScrollListener : OnScrollListener
    {
        private Context context;
        private int scrollerY;
        private int alpha;
        private BlurredViewLayout blurViewLayout;

        public BlurViewOnScrollListener(BlurredViewLayout blurViewLayout)
        {
            this.blurViewLayout = blurViewLayout;
        }
        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            scrollerY += dy;
            if (Math.Abs(scrollerY) > 1000)
            {
                blurViewLayout.SetBlurredTop(100);
                alpha = 100;
            }
            else
            {
                blurViewLayout.SetBlurredTop(scrollerY / 10);
                alpha = Math.Abs(scrollerY) / 10;
            }
            blurViewLayout.SetBlurredLevel(alpha);
        }
    }
    public class RecyclerAdapter : RecyclerView.Adapter
    {
        private readonly List<ApplicationInfo> data;
        private readonly Context context;
        private RecyclerChildViewHolder viewHolder;

        public RecyclerAdapter(Context context, List<ApplicationInfo> data)
        {
            this.context = context;
            this.data = data;
        }


        public override int ItemCount => data.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            var item = (ApplicationInfo)this.data[position];
            var icon = item.LoadIcon(this.context.PackageManager);
            var lable = item.LoadLabel(this.context.PackageManager);
            viewHolder = (RecyclerChildViewHolder)holder;
            viewHolder.BindView(icon, lable);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(this.context).Inflate(Resource.Layout.MainRecyclerViewItemView, parent, false);
            return new RecyclerChildViewHolder(view);
        }
    }
    public class RecyclerChildViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView recyclerItemTextView;
        private readonly ImageView recyclerItemImageView;

        public RecyclerChildViewHolder(View itemView)
            : base(itemView)
        {
            this.recyclerItemTextView = (TextView)itemView.FindViewById(Resource.Id.MainRecyclerViewItemTextView);
            this.recyclerItemImageView = (ImageView)itemView.FindViewById(Resource.Id.MainRecyclerViewItemImageView);
        }


        public void BindView(Drawable icon, string lable)
        {
            recyclerItemTextView.Text = lable;
            recyclerItemImageView.SetImageDrawable(icon);
        }
    }
}

