using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Bigkoo.Pickerview;
using static Com.Bigkoo.Pickerview.TimePickerView;
using static Com.Bigkoo.Pickerview.OptionsPickerView;
using Com.Bigkoo.Pickerview.Model;
using System.Collections;

namespace Murtain.App.Bindings.Droid.PickerView.Demo
{
    [Activity(Label = "Murtain.App.Bindings.Droid.PickerView.Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
        , IOnTimeSelectListener
        , IOnOptionsSelectListener
    {
        int count = 1;
        private TimePickerView datePickerView;
        private OptionsPickerView optionsPickerView;

        private IList provinceCollection;
        private IList<IList> cityCollection;
        private IList<IList<IList>> streetCollection;

        public void OnOptionsSelect(int position1, int position2, int position3)
        {
            Toast.MakeText(this
                , $"{((Province)provinceCollection[position1])?.PickerViewText}-{((City)cityCollection[position1][position2])?.PickerViewText}-{((Street)streetCollection[position1][position2][position3])?.PickerViewText}"
                , ToastLength.Short).Show();
        }

        public void OnTimeSelect(Java.Util.Date date)
        {
            Toast.MakeText(this, $"select date is {date.Year}-{date.Month}-{date.Day}", ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            this.datePickerView = new TimePickerView(this, TimePickerView.Type.YearMonthDay);
            this.datePickerView.SetTime(new Java.Util.Date());
            this.datePickerView.SetCyclic(false);
            this.datePickerView.SetCancelable(true);

            this.datePickerView.SetOnTimeSelectListener(this);

            Button btnDatePicker = FindViewById<Button>(Resource.Id.btnDatePicker);
            btnDatePicker.Click += (sender, args) =>
            {
                this.datePickerView.Show();
            };

            this.optionsPickerView = new OptionsPickerView(this);


            provinceCollection = new List<Province>();
            provinceCollection.Add(new Province("北京", "010"));

            cityCollection = new JavaList<IList> {
                new JavaList() {
                    new City("东城区", "100010"),
                    new City("朝阳区", "100020"),
                    new City("西城区", "100032"),
                    new City("通州区", "101149"),
                }
            };


            streetCollection = new JavaList<IList<IList>> {
                new JavaList<IList> {
                    new JavaList {
                            new Street("朝阳门"),
                            new Street("北新桥"),
                            new Street("和平里"),
                        },
                    new JavaList {
                            new Street("朝阳区"),
                        },
                   new JavaList {
                            new Street("西城区"),
                        },
                    new JavaList {
                        }
                },
            };

            optionsPickerView.SetTitle("选择城市");

            this.optionsPickerView.SetPicker(provinceCollection, cityCollection, streetCollection, true);
            optionsPickerView.SetCyclic(false, false, false);
            optionsPickerView.SetSelectOptions(0, 0, 0);
            this.optionsPickerView.SetOnoptionsSelectListener(this);



            Button btnOptionsPickerView = FindViewById<Button>(Resource.Id.btnCascadingSelection);
            btnOptionsPickerView.Click += (sender, args) =>
            {
                this.optionsPickerView.Show();
            };
        }

        public class Province : Java.Lang.Object, IPickerViewData
        {
            public Province()
            {

            }

            public Province(string name, string code)
            {
                this.Name = name;
                this.Code = code;
            }

            public string Name { get; set; }
            public string Code { get; set; }

            public string PickerViewText
            {
                get
                {
                    return this.Name;
                }
            }
        }

        public class City : Java.Lang.Object, IPickerViewData
        {
            public City()
            {

            }

            public City(string name, string code)
            {
                this.Name = name;
                this.Code = code;
            }

            public string Name { get; set; }
            public string Code { get; set; }

            public string PickerViewText
            {
                get
                {
                    return this.Name;
                }
            }
        }

        public class Street : Java.Lang.Object, IPickerViewData
        {
            public Street()
            {

            }

            public Street(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }

            public string PickerViewText
            {
                get
                {
                    return this.Name;
                }
            }
        }
    }


}

