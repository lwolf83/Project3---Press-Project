using Project_3___Press_Project.Entity;
using System.Collections.Generic;
using System;

namespace Project_3___Press_Project.Filter
{
    public class ShopFilter : FilterBase<Shop>
    {
        public ShopFilter(ICollection<Shop> entities) : base(entities)
        { }

        protected override ICollection<String> OverridenKeys
        {
            get => new List<String>
            {
                "city"
            };
        }

        public override FilterMatchCallback MatchCallback
        {
            get => MatchShops;
        }

        private bool MatchShops(Shop shop)
        {
            bool isMatching = true;
            foreach (var key in Keys)
            {
                if (key == "city")
                {
                    if (shop.Address.City.Name != this[key] as String)
                    {
                        return false;
                    }
                }
                else if (key == "department")
                {
                    if (shop.Address.City.Department.Name != this[key] as String)
                    {
                        return false;
                    }
                }
                isMatching = MatchObjectPropertiesEquality(shop);
            }
            return isMatching;
        }
    }
}
