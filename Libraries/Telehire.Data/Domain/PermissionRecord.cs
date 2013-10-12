using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    public partial class PermissionRecord : BaseEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsSpecific { get; set; }

    }

    class PermissionRecordMap : ClassMapping<PermissionRecord>
    {
        public PermissionRecordMap()
        {
            this.Lazy(true);
            this.Table("PermissionRecords");
            this.Property<string>(x => x.Description);
            this.Property<string>(x => x.Name);
            this.Property<bool>(x => x.IsSpecific);
            this.Id<int>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });


        }
    }
}
