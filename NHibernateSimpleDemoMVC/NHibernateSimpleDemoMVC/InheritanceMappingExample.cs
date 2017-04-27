//Table Per Subclass Inheritance mapping by NHibernate Mapping-by-Code

namespace NHibernateSimpleDemoMVC
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public Person()
        {
            
        }
    }

    public class JuridicalPerson : Person
    {
        public virtual string LegalName { get; set; }

        public JuridicalPerson()
        {
            
        }
    }

    public class PrivatePerson : Person
    {
        public virtual string Gender { get; set; }

        public PrivatePerson()
        {
            
        }
    }
}
