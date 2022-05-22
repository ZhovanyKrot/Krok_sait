namespace Krok_Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Responsebles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Responsebles()
        {
            LiabilityStatistics = new HashSet<LiabilityStatistics>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? SubjectId { get; set; }

        [StringLength(50)]
        public string Mounth { get; set; }

        public int? Passes { get; set; }

        public int? GoodReason { get; set; }

        public int? NotGoodReason { get; set; }

        public int? StudentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LiabilityStatistics> LiabilityStatistics { get; set; }

        public virtual Students Students { get; set; }

        public virtual Subjects Subjects { get; set; }
    }
}
