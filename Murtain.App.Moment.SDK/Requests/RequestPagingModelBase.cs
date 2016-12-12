using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.SDK.Requests
{
    public abstract class RequestPagingModelBase : RequestModelBase
    {
        public int? limit { get; set; }
        public int? page { get; set; }
        public string sort { get; set; }
        public bool? order { get; set; }

    }

    public abstract class ResponsePageingModelBase<T> where T : class, IResponseModel
    {

    }
}
