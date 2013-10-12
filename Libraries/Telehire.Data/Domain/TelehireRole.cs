using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    [Serializable]
    public partial class TelehireRole : BaseEntity<int>
    {
        private string roleName;

        public virtual string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        private string roleDescription;

        public virtual string RoleDescription
        {
            get { return roleDescription; }
            set { roleDescription = value; }
        }
        private bool autoAssign;

        public virtual bool AutoAssign
        {
            get { return autoAssign; }
            set { autoAssign = value; }
        }
    }

    class TelehireRoleMap : ClassMapping<TelehireRole>
    {
        public TelehireRoleMap()
        {
            this.Table("Roles");
            this.Lazy(true);
            this.Id<int>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });
            this.Property<string>(x => x.RoleName, mp => { mp.NotNullable(true); mp.Column("RoleName"); });
            this.Property<string>(x => x.RoleDescription, mp => { mp.NotNullable(true); mp.Column("RoleDescription"); });
            this.Property<bool>(x => x.AutoAssign, mp => { mp.Column("AutoAssign"); });
        }
    }
}
