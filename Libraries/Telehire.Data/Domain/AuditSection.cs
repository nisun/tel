﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Telehire.Data.Domain
{
    [Serializable]
    public class AuditSection : BaseEntity<int>
    {
        public virtual string Name { get; set; }
    }

    public class AuditSectionMap : ClassMapping<AuditSection>
    {
        public AuditSectionMap()
        {
            this.Table("AuditSections");
            this.Lazy(true);
            this.Id<int>(x => x.Id, mp => { mp.Column("Id"); mp.Generator(Generators.Native); });
            this.Property<string>(x => x.Name, mp => { mp.Column("Name"); });

        }
    }
}
