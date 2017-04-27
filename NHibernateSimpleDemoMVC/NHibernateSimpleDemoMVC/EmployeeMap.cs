using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateSimpleDemoMVC
{
    class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.Name, map => { map.NotNullable(true); map.Length(50); });
            Property(x => x.Phone, map => { map.NotNullable(true); map.Length(50); });
        }
    }
}
