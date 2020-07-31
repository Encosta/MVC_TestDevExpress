using System;
using System.Collections.Generic;

namespace MVC_Test.Models
{
    public partial class TreeList
    {
        public Guid TreeListId { get; set; }
        public string Id { get; set; }
        public string Items { get; set; }
        public string ParentId { get; set; }
        public bool? HasChild { get; set; }
    }
}
