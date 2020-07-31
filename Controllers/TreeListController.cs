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
    public class TreeListController : Controller
    {
        private EncostaContext _context;

        public TreeListController(EncostaContext context)
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
            var boms = from b in _context.TreeList
                       select new TreeList
                       {
                           TreeListId = b.TreeListId,
                           Id= b.Id,
                           Items = b.Items,
                           ParentId = b.ParentId,
                           HasChild = b.HasChild
                       };
            return DataSourceLoader.Load(_context.TreeList.Take(1000), loadOptions);
        }



        [HttpPost]
        public IActionResult Post(string values)
        {
            var newBomExpanded = new TreeList();
            JsonConvert.PopulateObject(values, newBomExpanded);

            if (!TryValidateModel(newBomExpanded))
                return BadRequest(); //(ModelState.GetFullErrorMessage());

            _context.TreeList.Add(newBomExpanded);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var treeList = _context.TreeList.First(e => e.TreeListId == key);

            JsonConvert.PopulateObject(values, treeList);

            if (!TryValidateModel(treeList))
                return BadRequest(); //(ModelState.GetFullErrorMessage());

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var treeList = _context.TreeList.First(e => e.TreeListId == key);
            _context.TreeList.Remove(treeList);
            _context.SaveChanges();
        }
    }
}