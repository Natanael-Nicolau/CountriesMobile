using Countries.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Common.Services
{
    public interface ILocalService
    {
        /// <summary>
        /// List with all the countries that were downloaded from the API
        /// </summary>
        List<CountryResponse> MyCountries { get; set; }
    }
}
