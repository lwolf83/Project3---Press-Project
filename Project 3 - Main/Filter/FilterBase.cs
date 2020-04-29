using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Project_3___Press_Project.Filter
{
    public abstract class FilterBase<IEntity> : Dictionary<String, Object> where IEntity : class
    {
        public delegate bool FilterMatchCallback(IEntity entity);

        /// <summary>
        /// Callback to queue filtering results
        /// </summary>
        public virtual FilterMatchCallback MatchCallback 
        { 
            get => MatchObjectPropertiesEquality;
        }

        protected virtual ICollection<String> OverridenKeys { get => new List<String>(); }

        /// <summary>
        /// Enqueue any entity which concerned fields strictly matching filter values
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="matchingEntities"></param>
        /// <param name="property"></param>
        protected bool MatchObjectPropertiesEquality(IEntity entity)
        {
            IEnumerable<PropertyInfo> entityProperties = entity.GetType().GetProperties();
            foreach (String key in Keys)
            {
                if (OverridenKeys.Contains(key))
                {
                    continue;
                }
                // Must transform property name to PascalCase
                String propertyName = char.ToUpper(key[0]) + key.Substring(1);
                PropertyInfo property = entityProperties.SingleOrDefault(p => p.Name == propertyName);
                if (property != null)
                {
                    if (property.GetValue(entity) != this[key])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Create filter for a set of entities
        /// </summary>
        /// <param name="entities"></param>
        public FilterBase(ICollection<IEntity> entities)
        {
            Entities = entities;
        }

        /// <summary>
        /// Entities to be filtered
        /// </summary>
        public ICollection<IEntity> Entities { get; set; }

        /// <summary>
        /// Results from the filter content
        /// </summary>
        public IReadOnlyCollection<IEntity> Results
        {
            get
            {
                var matchingEntities = new Queue<IEntity>();
                foreach (IEntity entity in Entities)
                {
                    EnqueueMatchingEntity(entity, 
                                          ref matchingEntities);
                }
                return matchingEntities;
            }
        }

        protected virtual void EnqueueMatchingEntity(IEntity entity, 
                                                     ref Queue<IEntity> matchingEntities)
        {
            if (MatchCallback(entity) == true)
            {
                matchingEntities.Enqueue(entity);
            }
        }
    }
}
