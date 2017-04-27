//Bi-directional NHibernate mapping by code

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateSimpleDemoMVC
{
    public class ChildMap : ClassMapping<Child>
    {
        public ChildMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(p => p.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            Property(p => p.Name, m =>
            {
                m.Column("Name");
                m.NotNullable(true);
            });

            ManyToOne(x => x.Parent);
        }
    }


    public class ParentMap : ClassMapping<Parent>
    {
        public ParentMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(p => p.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            Property(p => p.Name, m =>
            {
                m.Column("Name");
                m.NotNullable(true);
            });

            Bag(x => x.Children, colmap => { colmap.Key(x => x.Column("Parent")); colmap.Cascade(Cascade.All | Cascade.DeleteOrphans); }, map => { map.OneToMany(); });
        }
    }

}
