using System;
using UnityEngine;

namespace Infrastructure
{
    namespace PropertyAttributes
    {
        public class TypeFilterAttribute : PropertyAttribute
        {
            private Type _filterType;
            public Type FilterType { get { return _filterType; } }

            public TypeFilterAttribute(Type filterType)
            {
                _filterType = filterType;
            }
        }
    }
}
