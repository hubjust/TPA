﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Model;

namespace Serializers.Model
{
    [DataContract(Name = "ParameterModel", IsReference = true)]
    public class ParameterModel
    {
        public ParameterModel(FieldMetadata t) {   }

        public ParameterModel(ParameterMetadata parameterMetadata)
        {
            this.Name = parameterMetadata.Name;
            this.Type = TypeModel.GetOrAdd(parameterMetadata.Type);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }
    }
}