﻿using System;
using System.Collections.Generic;
using System.Linq;
using uFrame.Editor.Core;
using uFrame.Editor.Database.Data;
using uFrame.Editor.Graphs.Data.Types;
using uFrame.Json;

namespace uFrame.Editor.Graphs.Data
{

    public class GenericTypedChildItem : GenericNodeChildItem, IDataRecordRemoved, IMemberInfo, ITypedItem
    {
        protected string _type = string.Empty;

        public virtual Type Type
        {
            get
            {
                if (string.IsNullOrEmpty(RelatedType)) return null;

                return InvertApplication.FindTypeByName(RelatedType);
            }
        }

        public string NameAsChangedMethod
        {
            get { return string.Format("{0}Changed", Name); }
        }

        [JsonProperty]
        public string RelatedType
        {
            get
            {
                return _type;
            }
            set
            {
                this.Changed("RelatedType", ref _type, value);
            }
        }

        public virtual string DefaultTypeName
        {
            get { return typeof(string).FullName; }
        }

        public virtual string RelatedTypeName
        {
            get
            {
                var outputClass = RelatedTypeNode;
                if (outputClass != null)
                {
                    return outputClass.ClassName;
                }

                var relatedNode = this.RelatedNode();

                if (relatedNode != null)
                    return relatedNode.Name;

                return string.IsNullOrEmpty(RelatedType) ? DefaultTypeName : Type.Name;
            }
        }

        public virtual IClassTypeNode RelatedTypeNode
        {
            get
            {

                var result = this.OutputTo<IClassTypeNode>();
                if (result == null)
                {
                    return this.Repository.AllOf<IClassTypeNode>().FirstOrDefault(p => p.Identifier == RelatedType) as IClassTypeNode;
                }
                return result;
            }
        }

        public override void OnConnectedToInput(IConnectable input)
        {
            base.OnConnectedToInput(input);
            if(RelatedTypeNode != null)
            {
                InvertApplication.Log("OnConnectedToInput");
                //RelatedType = RelatedTypeNode.Identifier;
            }
        }

        public bool AllowEmptyRelatedType
        {
            get { return false; }
        }

        public string FieldName
        {
            get
            {
                return string.Format("_{0}Property", Name);
            }
        }

        public string ViewFieldName
        {
            get
            {
                return string.Format("_{0}", Name);
            }
        }


        public virtual void RemoveType()
        {
            this.RelatedType = typeof(string).FullName;
        }



        public IDiagramNode TypeNode()
        {
            return this.RelatedNode();
        }


        public override string FullLabel
        {
            get { return Name; }
        }

        public override string Label
        {
            get { return Name; }
        }

        public override void Remove(IDiagramNode diagramNode)
        {

        }

        public override void RecordRemoved(IDataRecord record)
        {
            if (RelatedType == record.Identifier)
            {
                RemoveType();
            }
        }

        public virtual string MemberName { get { return this.Name; } }
        public virtual ITypeInfo MemberType
        {
            get
            {
                var relatedNode = this.RelatedTypeNode as ITypeInfo;
                if (relatedNode != null)
                {
                    return relatedNode;
                }
                return new SystemTypeInfo(Type ?? InvertApplication.FindTypeByName(RelatedType));
            }

        }

        public virtual IEnumerable<Attribute> GetAttributes()
        {
            yield break;
        }
    }
}