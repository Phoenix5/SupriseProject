using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

//Table Per Subclass Inheritance mapping by NHibernate Mapping-by-Code

namespace NHibernateSimpleDemoMVC
{
    public class PersonMapping : ClassMapping<Person>
    {
        public PersonMapping()
        {
            Table("Person");
            Id(x => x.Id, m => m.Generator(Generators.Native));
            Property(x => x.Name);
        }
    }

    public class JuridicalPersonMapping : JoinedSubclassMapping<JuridicalPerson>
    {
        public JuridicalPersonMapping()
        {
            Table("JuridicalPerson");
            Key(m => m.Column("Person"));
            Property(x => x.LegalName);
        }
    }

    public class PrivatePersonMapping : JoinedSubclassMapping<PrivatePerson>
    {
        public PrivatePersonMapping()
        {
            Table("PrivatePerson");
            Key(m => m.Column("Person"));
            Property(x => x.Gender);
        }
    }
}
