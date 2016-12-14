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
using Android.Support.V7.Widget;

namespace Murtain.App.Moment.Droid.Adapters
{
    public class DescoverRecyclerAdapter : RecyclerView.Adapter
    {
        private readonly Context _mContext;
        private readonly List<string> _mDataSet;

        public DescoverRecyclerAdapter(Context context, List<string> dataList)
        {
            _mContext = context;
            _mDataSet = dataList;
        }

        public override int ItemCount => _mDataSet.Count;

        //Before Refactor
        //public override int ItemCount
        //{
        //    get { return mDataSet.Count; }
        //}

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            //var textViewHolder = (SuperChildViewHolder)holder;
            //textViewHolder.BindView(_mDataSet[position], position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(_mContext).Inflate(Resource.Layout.DescoverRecyclerViewItem, parent, false);
            return new SuperChildViewHolder(view);

        }

        protected void RemoveAll(int position, int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                _mDataSet.RemoveAt(position);
            }
            NotifyItemRangeRemoved(position, itemCount);
        }


        public override int GetItemViewType(int position)
        {
            return 0;
        }

        //public void Add(String text, int position)
        //{
        //    mDataSet.Insert(position, text);
        //    NotifyItemInserted(position);
        //}

        //public void AddAll(List<String> list, int position)
        //{
        //    mDataSet.InsertRange(position, list);
        //    NotifyItemRangeInserted(position, list.Count);
        //}

    }
}