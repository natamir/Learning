using System.Collections.Generic;
using System.Web.Mvc;
using Sample.Extensibility.DataSource.Entities;
using Sample.Extensibility.Domain;

namespace Sample.Controllers
{
    public class CalculationController : Controller
    {
        private readonly ICalculationService calculationService;

        public CalculationController(ICalculationService calculationService)
        {
            this.calculationService = calculationService;
        }

        public IEnumerable<IAccount> Run()
        {
            return calculationService.Calculate();
        }
    }
}