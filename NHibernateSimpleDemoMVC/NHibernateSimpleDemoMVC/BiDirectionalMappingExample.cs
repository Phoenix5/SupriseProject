//Bi-directional NHibernate mapping by code

using System.Collections.Generic;

namespace NHibernateSimpleDemoMVC
{
    public class Child
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Parent Parent { get; set; }

        public Child()
        {
            
        }
        public Child(Parent parent)
        {
            Parent = parent;
        }
    }

    public class Parent
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Child> Children { get; set; }

        public Parent()
        {
            Children = new List<Child>();
        }

        public virtual Child AddChild()
        {
            Child newChild = new Child(this); //link parent to child via constructor
            Children.Add(newChild); //add child to parent's collection
            return newChild; //return child for direct usage
        }
    }
}
