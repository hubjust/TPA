﻿using System.Text;

using Model;

namespace ViewModel.ViewModelMetadata
{
    class VMFieldMetadata : TreeViewItem
    {
        private FieldMetadata fieldMetadata;
        public override string Name => ToString();

        public VMFieldMetadata(FieldMetadata field)
        {
            fieldMetadata = field;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            Children.Add(new VMTypeMetadata(fieldMetadata.Type));
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Field: " + TransformModifiers() + fieldMetadata.Type.Name + " " + fieldMetadata.Name + TransformGenericArguments();
        }

        private string TransformModifiers()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(fieldMetadata.AccessLevel.ToString().Substring(2).ToLower() + " ");
            builder.Append(fieldMetadata.StaticEnum.Equals(StaticEnum.Static) ? "static " : "");

            return builder.ToString();
        }

        private string TransformGenericArguments()
        {
            if (fieldMetadata.Type.GenericArguments.IsNullOrEmpty())
                return "";

            StringBuilder builder = new StringBuilder();
            builder.Append("<");

            foreach (TypeMetadata arg in fieldMetadata.Type.GenericArguments)
                builder.Append(arg.Name + ",");

            builder.Remove(builder.Length - 1, 1);
            builder.Append(">");

            return builder.ToString();
        }
    }
}
