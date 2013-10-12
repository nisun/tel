using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    [Serializable]
    public partial class PermissionRoleMapping : BaseEntity<int>
    {
        public virtual int RoleId { get; set; }
        public virtual int PermissionId { get; set; }

        public virtual TelehireRole Role { get; set; }
        public virtual PermissionRecord Permission { get; set; }

    }

    class PermissionRoleMappingMap : ClassMapping<PermissionRoleMapping>
    {
        public PermissionRoleMappingMap()
        {
            this.Table("SpecificPermissionRoleMappings"); this.Lazy(true);
            this.Id<int>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });
            this.ManyToOne<TelehireRole>(x => x.Role, mp => { mp.Column("RoleId"); mp.Lazy(LazyRelation.Proxy); mp.Update(false); mp.Insert(false); });
            this.Property<int>(x => x.RoleId, mp => { mp.Column("RoleId"); });

            this.ManyToOne<PermissionRecord>(x => x.Permission, mp => { mp.Column("PermissionId"); mp.Lazy(LazyRelation.Proxy); mp.Update(false); mp.Insert(false); });
            this.Property<int>(x => x.PermissionId, mp => { mp.Column("PermissionId"); });
        }
    }
}
