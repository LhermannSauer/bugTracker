using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.viewModels
{
    public class PrioritizeFormViewModel
    {
        [Required, MinLength(10), MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int? PriorityId { get; set; }
    }
}