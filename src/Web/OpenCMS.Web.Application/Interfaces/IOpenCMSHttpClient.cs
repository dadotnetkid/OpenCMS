﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Web.Application.Interfaces
{
    public interface IOpenCMSHttpClient
    {
        public HttpClient Create();
    }
}
