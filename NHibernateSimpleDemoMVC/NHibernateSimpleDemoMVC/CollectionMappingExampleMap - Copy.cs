using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateSimpleDemoMVC
{
    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Id(p => p.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            Property(x => x.Name, map => { map.Length(150); map.NotNullable(true); });

            Component(x => x.HomeAddress, c =>
            {
                // mappings for component's parts
                c.Property(x => x.Street);
                c.Property(x => x.City);
                c.Property(x => x.State);
                c.Property(x => x.Zip);
            });

            Bag(x => x.Addresses, cp => { cp.Table("Address"); cp.Cascade(Cascade.All); cp.Key(k => k.Column("Customer")); }, cr => cr.Component(ce =>
            {
                ce.ManyToOne(x => x.Owner);
                ce.Property(x => x.Street);
                ce.Component(x => x.Number, y =>
                {
                    y.Component(x => x.OwnerAddress, map => { });
                    y.Property(x => x.Block);
                });
                ce.Property(x => x.City);
                ce.Property(x => x.State);
                ce.Property(x => x.Zip);
            }));
        }
    }

    class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.Name, map => { map.NotNullable(true); map.Length(50); });

            Bag<Order>("_Orders",
                colmap => { colmap.Table("OrderItems"); colmap.Key(x => x.Column("[Product]")); },
                map => map.ManyToMany(x => x.Column("[Order]")));
        }
    }

    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Id(p => p.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            ManyToOne(x => x.Customer);
            Property(x => x.OrderDate);

            Bag<Product>("_OrderItems",
                colmap => { colmap.Table("OrderItems"); colmap.Key(x => x.Column("[Order]")); colmap.Inverse(true); },
                map => map.ManyToMany(x => x.Column("[Product]")));
        }
    }
}
