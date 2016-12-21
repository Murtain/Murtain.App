using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Com.Github.Nuptboyzhb.Lib;
using static Com.Github.Nuptboyzhb.Lib.SuperSwipeRefreshLayout;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Murtain.App.Bindings.Droid.SuperSwipeRefresh.Demo;
using Android.Util;

namespace Murtain.App.Bindings.Droid.SuperSwipeRefresh.Demo
{
    [Activity(Label = "SuperSwipeRefresh", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
        , IOnPullRefreshListener
        , IOnPushLoadMoreListener
    {
        private List<string> data = new List<string>();

        private LinearLayoutManager linearLayoutManager;
        private SuperSwipeRefreshLayout superSwipeRefreshLayout;

        private RecyclerView recyclerView;
        private SuperSwipeRefreshRecyclerAdapter<string> recyclerAdapter;

        private ProgressBar superSwipeLayoutHeaderProgressBar;
        private TextView superSwipeLayoutHeaderTextView;
        private ImageView superSwipeLayoutHeaderImageView;

        private ProgressBar superSwipeLayoutFooterProgressBar;
        private TextView superSwipeLayoutFooterTextView;
        private ImageView superSwipeLayoutFooterImageView;

        public void OnLoadMore()
        {
            this.superSwipeLayoutFooterTextView.Text = "加载中...";
            this.superSwipeLayoutFooterImageView.Visibility = ViewStates.Gone;
            this.superSwipeLayoutFooterProgressBar.Visibility = ViewStates.Visible;

            new Handler().PostDelayed(() =>
            {

                this.superSwipeLayoutFooterImageView.Visibility = ViewStates.Visible;
                this.superSwipeLayoutFooterProgressBar.Visibility = ViewStates.Gone;
                this.superSwipeRefreshLayout.SetLoadMore(false);

                this.recyclerAdapter.NotifyDataSetChanged();
            }, 5000);

            this.BuildDatas();
        }

        public void OnPullDistance(int distance)
        {
            //throw new NotImplementedException();
        }

        public void OnPullEnable(bool enable)
        {
            this.superSwipeLayoutHeaderTextView.Text = enable ? "释放立即刷新..." : "下拉刷新";
            this.superSwipeLayoutHeaderImageView.Visibility = ViewStates.Visible;
            this.superSwipeLayoutHeaderImageView.Rotation = enable ? 180 : 0;
        }

        public void OnPushDistance(int distance)
        {
            //throw new NotImplementedException();
        }

        public void OnPushEnable(bool enable)
        {
            this.superSwipeLayoutFooterTextView.Text = enable ? "释放立即加载..." : "上拉加载更多";
            this.superSwipeLayoutFooterImageView.Visibility = ViewStates.Visible;
            this.superSwipeLayoutFooterImageView.Rotation = enable ? 0 : 180;
        }

        public void OnRefresh()
        {
            this.superSwipeLayoutHeaderTextView.Text = "正在刷新...";
            this.superSwipeLayoutHeaderImageView.Visibility = ViewStates.Gone;
            this.superSwipeLayoutHeaderProgressBar.Visibility = ViewStates.Visible;

            new Handler().PostDelayed(() =>
            {
                this.superSwipeRefreshLayout.Refreshing = false;
                this.superSwipeLayoutHeaderProgressBar.Visibility = ViewStates.Gone;

                this.recyclerAdapter.NotifyDataSetChanged();
            }, 2000);


            this.BuildDatas();
        }

        private void BuildDatas()
        {
            for (int i = 1; i <= 50; i++)
            {
                this.data.Add("SUPER SWIPE REFRESH LAYOUT ITEM  " + (this.data.Count + 1));
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            this.BuildDatas();

            Log.Error("SuperSwipeRefreshLayoutDemo", string.Join(",", this.data));

            linearLayoutManager = new LinearLayoutManager(this);

            superSwipeRefreshLayout = (SuperSwipeRefreshLayout)FindViewById(Resource.Id.SuperSwipeRefreshLayout);
            superSwipeRefreshLayout.SetHeaderView(CreateHeaderView());
            superSwipeRefreshLayout.SetFooterView(CreateFooterView());
            superSwipeRefreshLayout.SetHeaderViewBackgroundColor(Color.BlueViolet);
            superSwipeRefreshLayout.SetOnPullRefreshListener(this);
            superSwipeRefreshLayout.SetOnPushLoadMoreListener(this);

            recyclerView = (RecyclerView)FindViewById(Resource.Id.SuperSwipeRefreshLayoutRecycler);
            recyclerView.SetLayoutManager(linearLayoutManager);

            recyclerAdapter = new SuperSwipeRefreshRecyclerAdapter<string>(this, data);

            recyclerView.SetAdapter(recyclerAdapter);

            //recyclerAdapter.NotifyDataSetChanged();
        }

        private View CreateFooterView()
        {
            var footerView = LayoutInflater.From(superSwipeRefreshLayout.Context)
                    .Inflate(Resource.Layout.MainSuperSwipeRefreshLayoutFooter, null);
            superSwipeLayoutFooterProgressBar = (ProgressBar)footerView
                    .FindViewById(Resource.Id.SuperSwipeRefreshLayoutProgressBarFooterView);
            superSwipeLayoutFooterImageView = (ImageView)footerView
                    .FindViewById(Resource.Id.SuperSwipeRefreshLayoutImageFooterView);
            superSwipeLayoutFooterTextView = (TextView)footerView
                    .FindViewById(Resource.Id.SuperSwipeRefreshLayoutFooterTextView);
            superSwipeLayoutFooterProgressBar.Visibility = ViewStates.Gone;
            superSwipeLayoutFooterImageView.Visibility = ViewStates.Visible;
            superSwipeLayoutFooterImageView.SetImageResource(Resource.Drawable.SuperSwipeRefreshLayoutDownArrow);
            superSwipeLayoutFooterTextView.Text = "上拉加载更多";
            return footerView;
        }

        private View CreateHeaderView()
        {
            var headerView = LayoutInflater.From(superSwipeRefreshLayout.Context)
                    .Inflate(Resource.Layout.MainSuperSwipeRefreshLayoutHeader, null);
            superSwipeLayoutHeaderProgressBar = (ProgressBar)headerView.FindViewById(Resource.Id.SuperSwipeRefreshLayoutProgressBarHeaderView);
            superSwipeLayoutHeaderTextView = (TextView)headerView.FindViewById(Resource.Id.SuperSwipeRefreshLayoutHeaderTextView);
            superSwipeLayoutHeaderTextView.Text = "下拉刷新";
            superSwipeLayoutHeaderImageView = (ImageView)headerView.FindViewById(Resource.Id.SuperSwipeRefreshLayoutHeaderImageView);
            superSwipeLayoutHeaderImageView.Visibility = ViewStates.Visible;
            superSwipeLayoutHeaderImageView.SetImageResource(Resource.Drawable.SuperSwipeRefreshLayoutDownArrow);
            superSwipeLayoutHeaderProgressBar.Visibility = ViewStates.Gone;
            return headerView;
        }
    }

    public class SuperSwipeRefreshRecyclerAdapter<T> : RecyclerView.Adapter where T : class
    {
        private readonly List<T> data;
        private readonly Context context;
        private SuperSwipeRefreshChildViewHolder viewHolder;

        public SuperSwipeRefreshRecyclerAdapter(Context context, List<T> data)
        {
            this.context = context;
            this.data = data;

        }


        public override int ItemCount => data.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            viewHolder = (SuperSwipeRefreshChildViewHolder)holder;
            viewHolder.BindView(this.data[position].ToString(), position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(this.context).Inflate(Resource.Layout.MainSuperSwipeRefreshLayoutRecyclerItemView, parent, false);
            return new SuperSwipeRefreshChildViewHolder(view);
        }
    }
    public class SuperSwipeRefreshChildViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView superSwipeRefreshChildText;
        private readonly ImageView superSwipeRefreshChildImage;

        public SuperSwipeRefreshChildViewHolder(View itemView)
            : base(itemView)
        {
            this.superSwipeRefreshChildText = (TextView)itemView.FindViewById(Resource.Id.SuperSwipeRefreshChildText);
            this.superSwipeRefreshChildImage = (ImageView)itemView.FindViewById(Resource.Id.SuperSwipeRefreshChildImage);
        }


        public void BindView(string str, int position)
        {
            this.superSwipeRefreshChildText.Text = str;
        }
    }
}

