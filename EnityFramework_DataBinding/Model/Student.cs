namespace EnityFramework_DataBinding.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public int? Age { get; set; }

        [StringLength(50)]
        public string Major { get; set; }
    }
}
