namespace Krok_Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkPlan")]
    public partial class WorkPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? SubjectId { get; set; }

        public int? StudentId { get; set; }

        public int? GroupId { get; set; }

        [StringLength(10)]
        public string Semester { get; set; }

        public int? TimeAll { get; set; }

        public int? LectionTime { get; set; }

        public int? LaboratoryTime { get; set; }

        public int? ConsultationTime { get; set; }

        public float? exam { get; set; }

        public float? Credit { get; set; }

        public int? CourseWork { get; set; }

        public int? Degree { get; set; }

        public int? Credits { get; set; }

        public virtual Groups Groups { get; set; }

        public virtual Students Students { get; set; }

        public virtual Subjects Subjects { get; set; }
    }
}
