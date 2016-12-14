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
using MvvmCross.Droid.Views;
using Murtain.App.Moment.Cross.ViewModels;
using Android.Graphics.Drawables;
using Android.Graphics;
using Murtain.App.Moment.Droid.Views;
using Android.Util;
using Android.Content.Res;
using Com.Baoyz.Swipemenulistview;
using Android.Content.PM;
using static Com.Baoyz.Swipemenulistview.SwipeMenuListView;
using static Android.Widget.AdapterView;
using Java.Lang;

namespace Murtain.App.Moment.Droid.Views
{
    [Activity(Label = "²à»¬É¾³ý")]
    public class SwipeMenuListActivity : MvxActivity<SwipeMenuListViewModel>
        , IOnMenuItemClickListener
        , ISwipeMenuCreator
        , IOnSwipeListener
        , IOnMenuStateChangeListener
        , IOnItemLongClickListener
    {

        private SwipeMenuListView swipeMenuListView;

        public List<ApplicationInfo> LocalApplications;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            LocalApplications = PackageManager.GetInstalledApplications(0).ToList();

            // Create your application here
            SetContentView(Resource.Layout.SwipeMenuListView);

            swipeMenuListView = (SwipeMenuListView)FindViewById(Resource.Id.listView);

            swipeMenuListView.SetMenuCreator(this);
            swipeMenuListView.SetOnMenuItemClickListener(this);
            swipeMenuListView.Adapter = new SwipeMenuListAdapter(LocalApplications);

            // Right
            swipeMenuListView.SetSwipeDirection(SwipeMenuListView.DirectionLeft);
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

    public class SwipeMenuListAdapter : BaseSwipListAdapter
    {
        private List<ApplicationInfo> Data;
        public SwipeMenuListAdapter(List<ApplicationInfo> Data)
        {
            this.Data = Data;
        }
        public override int Count
        {
            get
            {
                return Data.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return Data[position];
        }

        public override long GetItemId(int position)
        {
            return Data.IndexOf(Data[position]);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = (ApplicationInfo)GetItem(position);

            if (convertView == null)
            {
                convertView = View.Inflate(Application.Context, Resource.Layout.SwipeMenuListViewItem, null);
                new SwipeMenuViewHolder(convertView).BindView(item.Icon, item.LabelRes);
            }
            SwipeMenuViewHolder holder = (SwipeMenuViewHolder)convertView.Tag;
            holder.BindView(item.Icon, item.LabelRes);

            return convertView;
        }
        public class SwipeMenuViewHolder : Java.Lang.Object
        {
            private readonly TextView tv_name;
            private readonly ImageView iv_icon;

            public SwipeMenuViewHolder(View itemView)
            {
                tv_name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                iv_icon = (ImageView)itemView.FindViewById(Resource.Id.iv_icon);
                itemView.Tag = this;
            }

            public void BindView(int iconResId, int lableResId)
            {
                //tv_name.SetText(lableResId);
                iv_icon.SetImageResource(iconResId);
            }
        }

    }
}