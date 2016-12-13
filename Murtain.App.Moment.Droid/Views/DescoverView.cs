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
using Android.Support.V7.Widget;
using Murtain.App.Moment.Droid.Adapters;
using Com.Tubb.Smrv;

namespace Murtain.App.Moment.Droid.Views
{
    [Activity(Label = "下拉刷新")]
    public class DescoverView : MvxActivity<DescoverViewModel>
    {
        //private RecyclerView recyclerView;
        private SwipeHorizontalMenuLayout swipeMenuView;
        private DescoverRecyclerAdapter recyclerAdapter;
        private LinearLayoutManager linearLayoutManager;
        private SuperSwipeRefreshLayout swipeRefreshLayout;

        private SuperSwipeRefreshLayout.IOnPullRefreshListener onPullRefreshListener;
        private SuperSwipeRefreshLayout.IOnPushLoadMoreListener onPushLoadMoreListener;

        private ProgressBar superSwipeLayoutHeaderProgressBar;
        private TextView superSwipeLayoutHeaderTextView;
        private ImageView superSwipeLayoutHeaderImageView;

        private ProgressBar superSwipeLayoutFooterProgressBar;
        private TextView superSwipeLayoutFooterTextView;
        private ImageView superSwipeLayoutFooterImageView;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Discover);

            //swipeMenuView = (SwipeHorizontalMenuLayout)FindViewById(Resource.Id.SwipeHorizontalMenuLayout);

            linearLayoutManager = new LinearLayoutManager(this);

            onPullRefreshListener = new SuperSwipeRefreshLayoutOnPullRefreshListener(this);
            onPushLoadMoreListener = new SuperSwipeRefreshLayoutOnPushLoadMoreListener(this);

            //recyclerView = (RecyclerView)FindViewById(Resource.Id.RecyclerView);
            //recyclerView.SetLayoutManager(linearLayoutManager);
            //recyclerAdapter = new DescoverRecyclerAdapter(this, ViewModel.Datas);
            //recyclerView.SetAdapter(recyclerAdapter);
            //recyclerAdapter.NotifyDataSetChanged();

            swipeRefreshLayout = FindViewById<Views.SuperSwipeRefreshLayout>(Resource.Id.SuperSwipeRefreshLayout);
            swipeRefreshLayout.SetHeaderView(DescoverRecyclerHeaderView());
            swipeRefreshLayout.SetFooterView(DescoverRecyclerFooterView());
            swipeRefreshLayout.SetTargetScrollWithLayout(true);
         


            swipeRefreshLayout.SetOnPullRefreshListener(onPullRefreshListener);
            swipeRefreshLayout.SetOnPushLoadMoreListener(onPushLoadMoreListener);

            BuildDatas();



            swipeRefreshLayout.Post(()=> {
                swipeRefreshLayout.SetRefreshing(true);
            });
            onPullRefreshListener.OnRefresh();

        }
        private View DescoverRecyclerFooterView()
        {
            var footerView = LayoutInflater.From(swipeRefreshLayout.Context)
                    .Inflate(Resource.Layout.DescoverSuperSwipeRefreshLayoutFooter, null);
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

        private View DescoverRecyclerHeaderView()
        {
            var headerView = LayoutInflater.From(swipeRefreshLayout.Context)
                    .Inflate(Resource.Layout.DescoverSuperSwipeRefreshLayoutHeader, null);
            superSwipeLayoutHeaderProgressBar = (ProgressBar)headerView.FindViewById(Resource.Id.SuperSwipeRefreshLayoutProgressBarHeaderView);
            superSwipeLayoutHeaderTextView = (TextView)headerView.FindViewById(Resource.Id.SuperSwipeRefreshLayoutHeaderTextView);
            superSwipeLayoutHeaderTextView.Text = "下拉刷新";
            superSwipeLayoutHeaderImageView = (ImageView)headerView.FindViewById(Resource.Id.SuperSwipeRefreshLayoutHeaderImageView);
            superSwipeLayoutHeaderImageView.Visibility = ViewStates.Visible;
            superSwipeLayoutHeaderImageView.SetImageResource(Resource.Drawable.SuperSwipeRefreshLayoutDownArrow);
            superSwipeLayoutHeaderProgressBar.Visibility = ViewStates.Gone;
            return headerView;
        }

        private void BuildDatas()
        {
            for (int i = 1; i <= 50; i++)
            {
                ViewModel.Datas.Add("SUPER SWIPE LAYOUT ITEM  " + (ViewModel.Datas.Count + 1));
            }
        }

        private class SuperSwipeRefreshLayoutOnPullRefreshListener : SuperSwipeRefreshLayout.IOnPullRefreshListener
        {
            private readonly DescoverView currentActivity;

            public SuperSwipeRefreshLayoutOnPullRefreshListener(DescoverView context)
            {
                currentActivity = context;
            }

            void SuperSwipeRefreshLayout.IOnPullRefreshListener.OnRefresh()
            {
                currentActivity.superSwipeLayoutHeaderTextView.Text = "正在刷新...";
                currentActivity.superSwipeLayoutHeaderImageView.Visibility = ViewStates.Gone;
                currentActivity.superSwipeLayoutHeaderProgressBar.Visibility = ViewStates.Visible;

                Action myAction = () =>
                {
                    currentActivity.swipeRefreshLayout.SetRefreshing(false);
                    currentActivity.superSwipeLayoutHeaderProgressBar.Visibility = ViewStates.Gone;

                    //currentActivity.recyclerAdapter.NotifyDataSetChanged();
                };

                new Handler().PostDelayed(myAction, 2000);

             
                currentActivity.BuildDatas();
            }

            void SuperSwipeRefreshLayout.IOnPullRefreshListener.OnPullDistance(int distance)
            {

            }

            void SuperSwipeRefreshLayout.IOnPullRefreshListener.OnPullEnable(bool enable)
            {
                currentActivity.superSwipeLayoutHeaderTextView.Text = enable ? "释放立即刷新..." : "下拉刷新";
                currentActivity.superSwipeLayoutHeaderImageView.Visibility = ViewStates.Visible;
                currentActivity.superSwipeLayoutHeaderImageView.Rotation = enable ? 180 : 0;
            }
        }
        private class SuperSwipeRefreshLayoutOnPushLoadMoreListener : SuperSwipeRefreshLayout.IOnPushLoadMoreListener
        {
            private readonly DescoverView currentActivity;

            public SuperSwipeRefreshLayoutOnPushLoadMoreListener(DescoverView context)
            {
                currentActivity = context;
            }

            void SuperSwipeRefreshLayout.IOnPushLoadMoreListener.OnLoadMore()
            {
                currentActivity.superSwipeLayoutFooterTextView.Text = "加载中...";
                currentActivity.superSwipeLayoutFooterImageView.Visibility = ViewStates.Gone;
                currentActivity.superSwipeLayoutFooterProgressBar.Visibility = ViewStates.Visible;

                Action myAction = () =>
                {

                    currentActivity.superSwipeLayoutFooterImageView.Visibility = ViewStates.Visible;
                    currentActivity.superSwipeLayoutFooterProgressBar.Visibility = ViewStates.Gone;
                    currentActivity.swipeRefreshLayout.SetLoadMore(false);

                    //currentActivity.recyclerAdapter.NotifyDataSetChanged();
                };

                new Handler().PostDelayed(myAction, 5000);

                currentActivity.BuildDatas();
            }

            void SuperSwipeRefreshLayout.IOnPushLoadMoreListener.OnPushDistance(int distance)
            {

            }

            void SuperSwipeRefreshLayout.IOnPushLoadMoreListener.OnPushEnable(bool enable)
            {
                currentActivity.superSwipeLayoutFooterTextView.Text = enable ? "释放立即加载..." : "上拉加载更多";
                currentActivity.superSwipeLayoutFooterImageView.Visibility = ViewStates.Visible;
                currentActivity.superSwipeLayoutFooterImageView.Rotation = enable ? 0 : 180;
            }
        }


    }
}