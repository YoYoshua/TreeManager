using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManager.Domain.Entities
{
    public class Node
    {
        public int NodeID { get; set; }
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public virtual Node Parent { get; set; }
        public List<Node> ChildNodes { get; set; }
    }
}
