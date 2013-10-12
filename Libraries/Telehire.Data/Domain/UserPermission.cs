using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    [Serializable]
    public partial class UserPermission : BaseEntity<int>
    {
        private PermissionRecord permission;

        public virtual PermissionRecord Permission
        {
            get { return permission; }
            set { permission = value; }
        }
        private Guid userId;

        public virtual Guid UserId
        {
            get { return userId; }
            set { userId = value; }
        }
    }

    partial class UserPermissionMap : ClassMapping<UserPermission>
    {
        public UserPermissionMap()
        {
            this.Table("UserPermissions");
            this.Lazy(true);
            this.Id<int>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });
            this.ManyToOne<PermissionRecord>(x => x.Permission, mp => { mp.Column("PermissionId"); mp.Lazy(LazyRelation.Proxy); });
            this.Property<Guid>(x => x.UserId, mp => { mp.NotNullable(true); mp.Column("UserId"); });
        }
    }
}
