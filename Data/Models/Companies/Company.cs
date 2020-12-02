using Data.Models.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models.Companies
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long ID { get; set; }
        public string Name { get; set; }
        public uint EstablishmentYear { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
