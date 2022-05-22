namespace Krok_Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TimeTables
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? GroupId { get; set; }

        public int? SubjectId { get; set; }

        [StringLength(50)]
        public string TipeLesson { get; set; }

        public int? ProfessorId { get; set; }

        public int? LectureHall { get; set; }

        public TimeSpan? Time { get; set; }

        public DateTime? DateTime { get; set; }

        public byte? NomberPair { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Departments Departments { get; set; }

        public virtual Groups Groups { get; set; }

        public virtual Professors Professors { get; set; }

        public virtual Subjects Subjects { get; set; }
    }
}
