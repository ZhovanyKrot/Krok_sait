namespace Krok_Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LiabilityStatistics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? GroupId { get; set; }

        public int? StudentId { get; set; }

        public int? DepartmentId { get; set; }

        public int? ResponsebleId { get; set; }

        public virtual Departments Departments { get; set; }

        public virtual Groups Groups { get; set; }

        public virtual Responsebles Responsebles { get; set; }

        public virtual Students Students { get; set; }
    }
}
