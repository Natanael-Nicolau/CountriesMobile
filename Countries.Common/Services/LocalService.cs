using Countries.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Common.Services
{
    public class LocalService : ILocalService
    {
        public List<CountryResponse> MyCountries { get; set; }
    }
}
