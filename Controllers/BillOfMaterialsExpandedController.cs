using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MVC_Test.Data;
using MVC_Test.Models;

namespace MVC_Test.Controllers
{
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
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var boms = from b in _context.BillOfMaterialsExpanded
                       select new BillOfMaterialsExpanded
                       {
                           BillOfMaterialsExpandedId = b.BillOfMaterialsExpandedId,
                           BomLevel = b.BomLevel,
                           TopLevelItem = b.TopLevelItem,
                           TopLevelDescription = b.TopLevelDescription,
                           ParentItem = b.ParentItem,
                           ParentDescription = b.ParentDescription,
                           ComponentItem = b.ComponentItem,
                           ComponentDescription = b.ComponentDescription,
                           FullSequence = b.FullSequence,
                           ParentId = b.ParentId,
                           HasChild = b.HasChild,

                       };
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