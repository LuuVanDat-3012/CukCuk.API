using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web.Controllers
{
    public class CountryController:BaseEntitiesController<Country>
    {
        ICountryService _countryService;
        public CountryController(ICountryService countryService) : base(countryService)
        {
            _countryService = countryService;
        }
       
    }
}
