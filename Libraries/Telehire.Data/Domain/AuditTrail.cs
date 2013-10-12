using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    [Serializable]
    public class AuditTrail : BaseEntity<long>
    {
        public virtual Guid UserId { get; set; }
        public virtual long AuditActionId { get; set; }
        public virtual AuditAction AuditAction { get; set; }
        public virtual string Details { get; set; }

        public virtual string UserIP { get; set; }

        public virtual DateTime TimeStamp { get; set; }
        public virtual long? AirlineId { get; set; }
        //public virtual DateTime LastDateEditted { get; set; }


    }

    public class AuditTrailMap : ClassMapping<AuditTrail>
    {
        public AuditTrailMap()
        {
            this.Table("AuditTrails");
            this.Lazy(true);
            this.Id<long>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });
            this.Property<Guid>(x => x.UserId, mp => { mp.Column("UserId"); });
            this.Property<string>(x => x.UserIP, mp => { mp.Column("UserIP"); });
            this.Property<string>(x => x.Details, mp => { mp.Column("Details"); });
            //this.Property<DateTime>(x => x.LastDateEditted, mp => { mp.Column("LastEditDate"); });
            this.Property<DateTime>(x => x.TimeStamp, mp => { mp.Column("TimeStamp"); });
            this.Property<long>(x => x.AuditActionId, mp => { mp.Column("AuditActionId"); });
            this.Property<long?>(x => x.AirlineId, mp => { mp.Column("AirlineId"); });
            this.ManyToOne<AuditAction>(x => x.AuditAction, mp => { mp.Lazy(LazyRelation.Proxy); mp.Update(false); mp.Insert(false); mp.Column("AuditActionId"); });


        }
    }
}
