using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MVC_Test.Data;
using MVC_Test.Models;
using Newtonsoft.Json;

namespace MVC_Test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillOfMaterialsExpandedController : Controller
    {
        private EncostaContext _context;

        public BillOfMaterialsExpandedController(EncostaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/BillOfMaterialsExpanded");
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_context.BillOfMaterialsExpanded, loadOptions);
        }



        [HttpPost]
        public IActionResult Post(string values)
        {
            var newBomExpanded = new BillOfMaterialsExpanded();
            JsonConvert.PopulateObject(values, newBomExpanded);

            if (!TryValidateModel(newBomExpanded))
                return BadRequest(); //(ModelState.GetFullErrorMessage());

            _context.BillOfMaterialsExpanded.Add(newBomExpanded);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var billOfMaterialsExpanded = _context.BillOfMaterialsExpanded.First(e => e.BillOfMaterialsExpandedId == key);

            JsonConvert.PopulateObject(values, billOfMaterialsExpanded);

            if (!TryValidateModel(billOfMaterialsExpanded))
                return BadRequest(); //(ModelState.GetFullErrorMessage());

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var billOfMaterialsExpanded = _context.BillOfMaterialsExpanded.First(e => e.BillOfMaterialsExpandedId == key);
            _context.BillOfMaterialsExpanded.Remove(billOfMaterialsExpanded);
            _context.SaveChanges();
        }
    }
}