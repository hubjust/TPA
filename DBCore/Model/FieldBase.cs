﻿using System.Collections.Generic;
using Core.Enum;

namespace Core.Model
{
    public class FieldBase
    {
        public string Name { get; set; }

        public TypeBase Type { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public StaticEnum StaticEnum { get; set; }
    }
}
