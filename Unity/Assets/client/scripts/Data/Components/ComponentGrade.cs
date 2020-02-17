using System;
using System.Collections.Generic;
using PandeaGames.Entities;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class ComponentGrade : UintValueContainer<ComponentGrade>, ComponentGrade.IGradedComponent
    {
        public interface IGradedComponent
        {
            ComponentGrade Grade { get; }
        }
        
        public ComponentGrade(uint value) : base(value)
        {
            
        }
        
        public override ComponentGrade Add(ComponentGrade other)
        {
            return new ComponentGrade(Value + other.Value);
        }

        public static ComponentGrade HighestGrade(IGradedComponent arg1, IGradedComponent arg2)
        {
            if (arg1 == null && arg2 == null)
                return null;
            if (arg1 == null)
                return arg2.Grade;
            if (arg2 == null)
                return arg1.Grade;
            
            return arg1.Grade > arg2.Grade ? arg1.Grade : arg2.Grade;
        }
        
        public static ComponentGrade HighestGrade(params IGradedComponent[] args)
        {
            return HighestGrade(args as IEnumerable<IGradedComponent>);
        }
        
        public static ComponentGrade HighestGrade<T>(IEnumerable<T> args) where T:IGradedComponent
        {
            ComponentGrade highest = null;

            foreach (T gradedComponent in args)
            {
                highest = HighestGrade(highest, gradedComponent);
            }
            
            return highest.Grade;
        }

        public ComponentGrade Grade
        {
            get { return this; }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}