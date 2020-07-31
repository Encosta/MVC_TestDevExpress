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
    public class BillOfMaterialsTestController : Controller
    {
        private EncostaContext _context;

        public BillOfMaterialsTestController(EncostaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DataGrid()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var boms = from b in _context.BillOfMaterialsTest
                       select new BillOfMaterialsTest
                       {
                           BillOfMaterialsExpandedId = b.BillOfMaterialsExpandedId,
                           Bomlevel = b.Bomlevel,
                           TopLevelItem = b.TopLevelItem,
                           TopLevelDescription = b.TopLevelDescription,
                           ParentItem = b.ParentItem,
                           ParentDescription = b.ParentDescription,
                           ComponentItem = b.ComponentItem,
                           ComponentDescription = b.ComponentDescription,
                           Id= b.Id,
                           ParentId = b.ParentId,
                           HasChild = b.HasChild
                       };
            return DataSourceLoader.Load(_context.BillOfMaterialsTest.Take(1000), loadOptions);
        }



        [HttpPost]
        public IActionResult Post(string values)
        {
            var newBomExpanded = new BillOfMaterialsTest();
            JsonConvert.PopulateObject(values, newBomExpanded);

            if (!TryValidateModel(newBomExpanded))
                return BadRequest(); //(ModelState.GetFullErrorMessage());

            _context.BillOfMaterialsTest.Add(newBomExpanded);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var billOfMaterialsTest = _context.BillOfMaterialsTest.First(e => e.BillOfMaterialsExpandedId == key);

            JsonConvert.PopulateObject(values, billOfMaterialsTest);

            if (!TryValidateModel(billOfMaterialsTest))
                return BadRequest(); //(ModelState.GetFullErrorMessage());

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var billOfMaterialsTest = _context.BillOfMaterialsTest.First(e => e.BillOfMaterialsExpandedId == key);
            _context.BillOfMaterialsTest.Remove(billOfMaterialsTest);
            _context.SaveChanges();
        }
    }
}