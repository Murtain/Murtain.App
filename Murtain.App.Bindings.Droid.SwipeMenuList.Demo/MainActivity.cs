using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using static Android.Widget.AdapterView;
using static Com.Baoyz.SuperSwipeMenuListView.SwipeMenuListView;
using Com.Baoyz.SuperSwipeMenuListView;
using Android.Content.PM;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Util;
using Java.Lang;
using Murtain.App.Bindings.Droid.SwipeMenuList.Demo;
using System.Linq;

namespace Murtain.App.Bindings.Droid.SwipeMenuList.Demo
{
    [Activity(Label = "Murtain.App.Bindings.Droid.SwipeMenuList.Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
        , IOnMenuItemClickListener
        , ISwipeMenuCreator
        , IOnSwipeListener
        , IOnMenuStateChangeListener
        , IOnItemLongClickListener
    {
        int count = 1;

        private SwipeMenuListView swipeMenuListView;
        private List<ApplicationInfo> applications;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            this.applications = PackageManager.GetInstalledApplications(0).ToList();


            this.swipeMenuListView = (SwipeMenuListView)FindViewById(Resource.Id.SwipeMenuListView);

            this.swipeMenuListView.SetMenuCreator(this);
            this.swipeMenuListView.SetOnMenuItemClickListener(this);
            this.swipeMenuListView.Adapter = new SwipeMenuListAdapter<ApplicationInfo>(this, this.applications);

            // Right
            this.swipeMenuListView.SetSwipeDirection(SwipeMenuListView.DirectionLeft);
        }

        public void Create(SwipeMenu menu)
        {
            // create "open" item
            SwipeMenuItem openItem = new SwipeMenuItem(Application.Context);
            // set item background
            openItem.Background = new ColorDrawable((Color.Rgb(0xC9, 0xC9, 0xCE)));
            // set item width
            openItem.Width = Dp2Px(90);
            // set item title
            openItem.Title = "Open";
            // set item title fontsize
            openItem.TitleSize = 18;
            // set item title font color
            openItem.TitleColor = Color.White;
            // add to menu
            menu.AddMenuItem(openItem);

            // create "delete" item
            SwipeMenuItem deleteItem = new SwipeMenuItem(Application.Context);
            // set item background
            deleteItem.Background = new ColorDrawable(Color.Rgb(0xF9, 0x3F, 0x25));
            // set item width
            deleteItem.Width = Dp2Px(90);
            // set item title
            openItem.Title = "Delete";
            // set item title fontsize
            openItem.TitleSize = 18;
            // set item title font color
            openItem.TitleColor = Color.White;
            // set a icon
            //deleteItem.Icon = (Resource.Drawable.SwipeMenuDeleteIcon);
            // add to menu
            menu.AddMenuItem(deleteItem);
        }

        public bool OnItemLongClick(AdapterView parent, View view, int position, long id)
        {
            Toast.MakeText(this, $"on item long click ! position {position} id  {id}", ToastLength.Short).Show();
            return false;
        }

        public void OnMenuClose(int position)
        {
            Toast.MakeText(this, $"on menu close ! position {position}", ToastLength.Short).Show();
        }

        public bool OnMenuItemClick(int position, SwipeMenu menu, int index)
        {
            Toast.MakeText(this, $"on menu item click ! position {position} index {index}", ToastLength.Short).Show();
            return false;
        }

        public void OnMenuOpen(int position)
        {
            Toast.MakeText(this, $"on menu open ! position {position}", ToastLength.Short).Show();
        }

        public void OnSwipeEnd(int position)
        {
            Toast.MakeText(this, $"on swipe end ! position {position}", ToastLength.Short).Show();
        }

        public void OnSwipeStart(int position)
        {
            Toast.MakeText(this, $"on swipe start ! position {position}", ToastLength.Short).Show();
        }

        private int Dp2Px(int dp)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Application.Context.Resources.DisplayMetrics);
        }

    }

    public class SwipeMenuListAdapter<T> : BaseSwipListAdapter where T : Java.Lang.Object
    {
        private readonly List<T> data;
        private readonly Context context;
        public SwipeMenuListAdapter(Context context, List<T> data)
        {
            this.context = context;
            this.data = data;
        }


        public override int Count => this.data.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return this.data[position];
        }

        public override long GetItemId(int position)
        {
            return this.data.IndexOf(this.data[position]);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = (ApplicationInfo)GetItem(position);
            var icon = item.LoadIcon(this.context.PackageManager);
            var lable = item.LoadLabel(this.context.PackageManager);
            if (convertView == null)
            {
                convertView = View.Inflate(this.context, Resource.Layout.MainSwipeMenuListViewItemView, null);
                new SwipeMenuListItemViewHolder(convertView).BindView(icon,lable );
            }
            SwipeMenuListItemViewHolder holder = (SwipeMenuListItemViewHolder)convertView.Tag;
            holder.BindView(icon, lable);

            return convertView;
        }
    }

    public class SwipeMenuListItemViewHolder : Java.Lang.Object
    {
        private readonly TextView swipeMenuListItemTextView;
        private readonly ImageView swipeMenuListItemImageView;

        public SwipeMenuListItemViewHolder(View itemView)
        {
            swipeMenuListItemTextView = (TextView)itemView.FindViewById(Resource.Id.SwipeMenuListItemTextView);
            swipeMenuListItemImageView = (ImageView)itemView.FindViewById(Resource.Id.SwipeMenuListItemImageView);
            itemView.Tag = this;
        }

        public void BindView(Drawable icon, string lable)
        {
            swipeMenuListItemTextView.Text = lable;
            swipeMenuListItemImageView.SetImageDrawable(icon);
        }
    }
}

