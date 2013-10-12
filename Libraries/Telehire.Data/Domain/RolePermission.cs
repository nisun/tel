using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    [Serializable]
    public partial class RolePermission : BaseEntity<int>
    {
        private PermissionRecord permission;

        public virtual PermissionRecord Permission
        {
            get { return permission; }
            set { permission = value; }
        }
        private TelehireRole telehireRole;

        public virtual TelehireRole TelehireRole
        {
            get { return telehireRole; }
            set { telehireRole = value; }
        }
    }

    class RolePermissionMap : ClassMapping<RolePermission>
    {
        public RolePermissionMap()
        {
            this.Table("RolePermissions");
            this.Lazy(true);
            this.Id<int>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });
            this.ManyToOne<PermissionRecord>(x => x.Permission, mp => { mp.Lazy(LazyRelation.Proxy); mp.Column("PermissionId"); });
            this.ManyToOne<TelehireRole>(x => x.TelehireRole, mp => { mp.Lazy(LazyRelation.Proxy); mp.Column("RoleId"); });
        }
    }
}
